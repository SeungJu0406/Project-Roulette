using System.Collections;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    private RouletteController _roulette;
    private ChipController _chip;
    private CardController _card;
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
    }
    private void OnDestroy()
    {
        Manager.SetGameFlowManager(null);
    }

    private void StartTurnFlow()
    {
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

}
