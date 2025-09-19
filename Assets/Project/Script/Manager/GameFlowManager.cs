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
        // ���� ���� UI ����
        UIManager.ChangePanel(InGameCanvas.Panel.RoundStart);
        // �ʱ� Ĩ ����
        _chip.HoldChip = _chip.StartChip;
        // �ʱ� ��Ƽ�� ī�� ����
        for (int i = 0; i < _startCardCount; i++)
        {
            _card.AddRandomActiveCard();
        }

        // �� Ƚ�� �ʱ�ȭ
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
        // ī�� ����
        _card.OnTurnStart();
        // �귿 �� ����
        _roulette.StartTurn();
        _roulette.IsAlwaysWin = false;
    }
    protected virtual void SpinFlow()
    {
        // ī�� ����
        _card.OnSpin();
        // �귿 ����
        _roulette.Spin();
    }

    protected virtual void WinFlow()
    {
        // ī�� ����
        _card.OnWin();
        // Ĩ ����
        _chip.CollectChip(_roulette.BetMultiplier);
    }
    protected virtual void LoseFlow()
    {
        // ī�� ����
        _card.OnLose();

        if(_roulette.IsAlwaysWin == true)
        {
            _chip.CollectChip(_roulette.BetMultiplier * _roulette.AlwaysWinMultiplier);
        }
        else
        {
            // Ĩ �ս�
            _chip.LoseChip();
        }

    }
    protected virtual void EndTurnFlow()
    {
        // ī�� ����
        _card.OnTurnEnd();
        // ����
        _roulette.ClearBet();

        //TODO: �ϴ� �ٷ� ����, ���߿� UI ���ð� �� ��
        Manager.Event.StartTurnInvoke();
    }


    protected virtual void EndRoundFlow()
    {
        // ���� ���� UI ����
        UIManager.ChangePanel(InGameCanvas.Panel.RoundEnd);
    }
    protected virtual void StartShopFlow()
    {
        // ���� UI ����
        UIManager.ChangePanel(InGameCanvas.Panel.Shop);

        // �нú� ī�� ������ ����
        _shop.SetPassiveChoice();
    }
    protected virtual void EndShopFlow()
    {

    }
}
