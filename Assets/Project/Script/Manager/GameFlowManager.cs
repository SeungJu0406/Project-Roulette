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
        // ī�� ����
        _card.OnTurnStart();
        // �귿 �� ����
        _roulette.StartTurn();
    }
    private void SpinFlow()
    {
        // ī�� ����
        _card.OnSpin();
        // �귿 ����
        _roulette.Spin();
    }

    private void WinFlow()
    {
        // ī�� ����
        _card.OnWin();
        // Ĩ ����
        _chip.CollectChip(_roulette.BetMultiplier);
    }
    private void LoseFlow()
    {
        // ī�� ����
        _card.OnLose();
        // Ĩ �ս�
        _chip.LoseChip();
    }
    private void EndTurnFlow()
    {
        // ī�� ����
        _card.OnTurnEnd();
        // ����
        _roulette.ClearBet();

        //TODO: �ϴ� �ٷ� ����, ���߿� UI ���ð� �� ��
        Manager.Turn.StartTurnInvoke();
    }

}
