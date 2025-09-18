using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    private RouletteController _roulette;
    private ChipController _chip;
    private CardController _card;
    private TurnController _turn;

    private void Awake()
    {
        Manager.SetGameFlowManager(this);

        _roulette = FindAnyObjectByType<RouletteController>();
        _chip = FindAnyObjectByType<ChipController>();
        _card = FindAnyObjectByType<CardController>();
        _turn = FindAnyObjectByType<TurnController>();
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

        Manager.Event.RoundStartInvoke();
    }
    private void OnDestroy()
    {
        Manager.SetGameFlowManager(null);
    }

    private void StartTurnFlow()
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
    }
    private void SpinFlow()
    {
        // 카드 적용
        _card.OnSpin();
        // 룰렛 스핀
        _roulette.Spin();
    }

    private void WinFlow()
    {
        // 카드 적용
        _card.OnWin();
        // 칩 수집
        _chip.CollectChip(_roulette.BetMultiplier);
    }
    private void LoseFlow()
    {
        // 카드 적용
        _card.OnLose();
        // 칩 손실
        _chip.LoseChip();
    }
    private void EndTurnFlow()
    {
        // 카드 적용
        _card.OnTurnEnd();
        // 정리
        _roulette.ClearBet();

        //TODO: 일단 바로 시작, 나중에 UI 대기시간 줄 것
        Manager.Event.StartTurnInvoke();
    }

    private void StartRoundFlow()
    {
        // 라운드 시작 UI 띄우기
        UIManager.ChangePanel(InGameCanvas.Panel.RoundStart);
        // 초기 칩 지급

        // 턴 횟수 초기화
        _turn.CurrentTurn = 0;
    }
    private void EndRoundFlow()
    {
        // 라운드 종료 UI 띄우기
        UIManager.ChangePanel(InGameCanvas.Panel.RoundEnd);
    }
    private void StartShopFlow()
    {
        // 상점 UI 띄우기
        UIManager.ChangePanel(InGameCanvas.Panel.Shop);
    }
    private void EndShopFlow()
    {

    }
}
