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
        Manager.Event.StartTurnInvoke();
    }

    private void StartRoundFlow()
    {
        // ���� ���� UI ����
        UIManager.ChangePanel(InGameCanvas.Panel.RoundStart);
        // �ʱ� Ĩ ����

        // �� Ƚ�� �ʱ�ȭ
        _turn.CurrentTurn = 0;
    }
    private void EndRoundFlow()
    {
        // ���� ���� UI ����
        UIManager.ChangePanel(InGameCanvas.Panel.RoundEnd);
    }
    private void StartShopFlow()
    {
        // ���� UI ����
        UIManager.ChangePanel(InGameCanvas.Panel.Shop);
    }
    private void EndShopFlow()
    {

    }
}
