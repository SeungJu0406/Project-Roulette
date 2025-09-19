using System.ComponentModel;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    [SerializeField] private int _startCardCount = 3;

    private RouletteController _roulette;
    private ChipController _chip;
    private CardController _card;
    private TurnController _turn;
    private ShopController _shop;

    private void Awake()
    {

        _roulette = FindAnyObjectByType<RouletteController>();
        _chip = FindAnyObjectByType<ChipController>();
        _card = FindAnyObjectByType<CardController>();
        _turn = FindAnyObjectByType<TurnController>();
        _shop = FindAnyObjectByType<ShopController>();
    }

    private void Start()
    {
        Manager.Event.OnTurnStartEvent += StartTurnFlow;
        Manager.Event.OnTurnEndEvent += EndTurnFlow;
        Manager.Event.OnSpinEvent += SpinFlow;
        Manager.Event.OnWinEvent += WinFlow;
        Manager.Event.OnLoseEvent += LoseFlow;
        Manager.Event.OnRoundStartEvent += StartRoundFlow;
        Manager.Event.OnRoundEndEvent += EndRoundFlow;
        Manager.Event.OnShopStartEvent += StartShopFlow;
        Manager.Event.OnShopEndEvent += EndShopFlow;

        StartGame();
    }
    protected virtual  void StartGame()
    {
        Manager.Event.RoundStartInvoke();
    }

    protected virtual void StartRoundFlow()
    {
        // 라운드 시작 UI 띄우기
        UIManager.ChangePanel(InGameCanvas.Panel.RoundStart);
        // 초기 칩 지급
        _chip.HoldChip = _chip.StartChip;
        // 초기 액티브 카드 지급
        for (int i = 0; i < _startCardCount; i++)
        {
            _card.AddRandomActiveCard();
        }

        // 턴 횟수 초기화
        _turn.CurrentTurn = 0;
    }

    protected virtual void StartTurnFlow()
    {
        if (_turn.CurrentTurn >= _turn.MaxTurn)
        {
            Manager.Event.RoundEndInvoke();
            return;
        }

        _turn.CurrentTurn++;
        // 카드 적용
        _card.OnTurnStart();
        // 룰렛 재 세팅
        _roulette.StartTurn();
        _roulette.IsAlwaysWin = false;
    }
    protected virtual void SpinFlow()
    {
        // 카드 적용
        _card.OnSpin();
        // 룰렛 스핀
        _roulette.Spin();
    }

    protected virtual void WinFlow()
    {
        // 카드 적용
        _card.OnWin();
        // 칩 수집
        _chip.CollectChip(_roulette.BetMultiplier);
    }
    protected virtual void LoseFlow()
    {
        // 카드 적용
        _card.OnLose();

        if(_roulette.IsAlwaysWin == true)
        {
            _chip.CollectChip(_roulette.BetMultiplier * _roulette.AlwaysWinMultiplier);
        }
        else
        {
            // 칩 손실
            _chip.LoseChip();
        }

    }
    protected virtual void EndTurnFlow()
    {
        // 카드 적용
        _card.OnTurnEnd();
        // 정리
        _roulette.ClearBet();

        //TODO: 일단 바로 시작, 나중에 UI 대기시간 줄 것
        Manager.Event.StartTurnInvoke();
    }


    protected virtual void EndRoundFlow()
    {
        // 라운드 종료 UI 띄우기
        UIManager.ChangePanel(InGameCanvas.Panel.RoundEnd);
    }
    protected virtual void StartShopFlow()
    {
        // 상점 UI 띄우기
        UIManager.ChangePanel(InGameCanvas.Panel.Shop);

        // 패시브 카드 선택존 세팅
        _shop.SetPassiveChoice();
    }
    protected virtual void EndShopFlow()
    {

    }
}
