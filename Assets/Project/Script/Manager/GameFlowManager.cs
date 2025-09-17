using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    private RouletteController _roulette;
    private ChipController _chip;
    private CardController _card;


    // TODO: 테스트용 턴 횟수, 구조분리 필요

    private int _turnCount = 0;
    private void Awake()
    {
        Manager.SetGameFlowManager(this);

        _roulette = FindAnyObjectByType<RouletteController>();
        _chip = FindAnyObjectByType<ChipController>();
        _card = FindAnyObjectByType<CardController>();
    }

    private void Start()
    {
        Manager.Turn.OnTurnStartEvent += StartTurnFlow;
        Manager.Turn.OnTurnEndEvent += EndTurnFlow;
        Manager.Turn.OnSpinEvent += SpinFlow;
        Manager.Turn.OnWinEvent += WinFlow;
        Manager.Turn.OnLoseEvent += LoseFlow;
        Manager.Turn.OnRoundStartEvent += StartRoundFlow;
        Manager.Turn.OnRoundEndEvent += EndRoundFlow;
        Manager.Turn.OnShopStartEvent += StartShopFlow;
        Manager.Turn.OnShopEndEvent += EndShopFlow;

        Manager.Turn.RoundStartInvoke();
    }
    private void OnDestroy()
    {
        Manager.SetGameFlowManager(null);
    }

    private void StartTurnFlow()
    {
        if (_turnCount >= 3)
        {
            Manager.Turn.RoundEndInvoke();
            return;
        }

        _turnCount++;
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
        Manager.Turn.StartTurnInvoke();
    }

    private void StartRoundFlow()
    {
        // 라운드 시작 UI 띄우기
        UIManager.ChangePanel(InGameCanvas.Panel.RoundStart);
        // 초기 칩 지급

        // 턴 횟수 초기화
        _turnCount = 0;
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
