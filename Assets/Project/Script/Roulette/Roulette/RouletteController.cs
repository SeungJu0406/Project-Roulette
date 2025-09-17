using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;
using Utility;
using WeightUtility;

public class RouletteController : MonoBehaviour
{
    public RouletteSlot[] Slots { get => _model.Slots; set { _model.Slots = value; } }

    [SerializeField] private RouletteModel _model;

    public event UnityAction<float> OnWinEvent;
    public event UnityAction OnLoseEvent;


    [SerializeField] private RouletteCreateHandler _createHandler;
    [SerializeField] private float _spinDuration = 3f;
    [SerializeField] private float _spenSpeedPerSecond = 5f;

    private float _betMultiplier { get => _model.BetMultiplier; set { _model.BetMultiplier = value; } }

    private List<RouletteSlot> _betSlots;
    private RouletteBetController _currentBetHandler;
    private RouletteBetController[] _betHandlers;
    private WeightTable<RouletteSlot> _weightTable;
    private RouletteSlot _resultSlot;
    private void Awake()
    {
        _betHandlers = GetComponentsInChildren<RouletteBetController>(true);
        _model.InitModel();
    }

    private void Start()
    {
        InitRoulette();
        InitBetHandler();
        SubscribeEvent();
    }

    public void SetCurBetHandelr(RouletteBetController handler)
    {
        _betMultiplier /= _currentBetHandler == null ? 1f : _currentBetHandler.BetMultiplier;
        _currentBetHandler?.Choice(false);

        _currentBetHandler = handler;

        _betMultiplier *= _currentBetHandler == null ? 1f : _currentBetHandler.BetMultiplier;
        _currentBetHandler?.Choice(true);
    }
    public void SetBetSlots(List<RouletteSlot> betSlots)
    {
        _betSlots = new List<RouletteSlot>(betSlots);
    }

    public void Spin()
    {
        StartCoroutine(SpinRoutine());
    }

    private void SpinReal()
    {
        _resultSlot = _weightTable.Pick();
        _resultSlot?.RevouleSlot();
        CheckBetResult();
    }

    IEnumerator SpinRoutine()
    {
        float delay = 1f / _spenSpeedPerSecond;
        float timer = 0f;
        RouletteSlot slot = null;
        while (timer < _spinDuration)
        {
            timer += delay;
            slot = PickSlot(slot);
            yield return delay.Second();
        }
        slot?.SetOutline(false);
        SpinReal();
    }

    private RouletteSlot PickSlot(RouletteSlot curSlot)
    {
        curSlot?.SetOutline(false);
        curSlot = _weightTable.Pick();
        curSlot?.SetOutline(true);
        return curSlot;
    }

    private void CheckBetResult()
    {
        if(_betSlots.Count == 0)
        {
            Debug.Log("No Bet Placed");
            return;
        }

        foreach (var slot in _betSlots)
        {
            if(slot == _resultSlot)
            {
                OnWin();
                return;
            }
        }
        OnLose();
    }

    private void OnWin()
    {
        // TODO : 추가 배율 계산
        OnWinEvent?.Invoke(_betMultiplier);

        _betMultiplier = 1f;
    }

    private void OnLose()
    {
        OnLoseEvent?.Invoke();

        _betMultiplier = 1f;
    }

    private void ClearBets()
    {
        _betSlots.Clear();
        SetCurBetHandelr(null);
        _betMultiplier = 1f;
    }

    private void InitBetHandler()
    {
        foreach (var handler in _betHandlers)
        {
            handler.SetRouletteController(this);
            handler.SetSlots(Slots);
        }
    }

    private void InitRoulette()
    {
        // 슬롯 확률 테이블 초기화
        _weightTable = new WeightTable<RouletteSlot>();
        foreach (var slot in Slots)
        {
            _weightTable.AddElement(slot, slot.Probability);
            slot.OnProbabilityChanged += EditSlotProbability;
        }
    }

    private void EditSlotProbability(RouletteSlot slot)
    {
        _weightTable.EditWeight(slot, slot.Probability);
    }
    /// <summary>
    /// 룰렛 최초 생성
    /// </summary>
    private void CreateRoulette() => _createHandler.CreateRoulette();

    private void SubscribeEvent()
    {
        TurnManager.Instance.OnTurnEndEvent += ClearBets;
        TurnManager.Instance.OnSpinEvent += Spin;
    }

    [ContextMenu("CreateRoulette")]
    private void CreateRouletteInspector()
    {
        _createHandler.Initialize(this);
        CreateRoulette();
    }
}
