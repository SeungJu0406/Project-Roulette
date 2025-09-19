using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utility;
using WeightUtility;

public class RouletteController : MonoBehaviour
{
    public RouletteSlot[] Slots { get => _model.Slots; set { _model.Slots = value; } }

    [SerializeField] private RouletteModel _model;


    [SerializeField] private RouletteCreateHandler _createHandler;
    [SerializeField] private float _spinDuration = 3f;
    [SerializeField] private float _spenSpeedPerSecond = 5f;

    public float BetMultiplier { get => _model.BetMultiplier; set { _model.BetMultiplier = value; } }

    // 당첨 안되도 당첨 처리 옵션(배당금 절반)
    [HideInInspector] public bool IsAlwaysWin = false;
    [HideInInspector] public float AlwaysWinMultiplier = 0.5f;

    public event UnityAction<RouletteBetController> OnChangeCurrentBetHandler;

    private List<RouletteSlot> _betSlots;
    private RouletteBetController _currentBetHandler;
    private RouletteBetController[] _betHandlers;
    private WeightTable<RouletteSlot> _weightTable;
    private RouletteSlot _resultSlot;
    private bool _canSpin = true;

    private void Awake()
    {
        IsAlwaysWin = false;

        _betHandlers = GetComponentsInChildren<RouletteBetController>(true);
        _model.InitModel(this);
    }

    private void Start()
    {
        InitRouletteWeight();
        InitBetHandler();
        SubscribeEvent();
    }

    public void SetCurBetHandelr(RouletteBetController handler)
    {
        BetMultiplier /= _currentBetHandler == null ? 1f : _currentBetHandler.BetMultiplier;
        _currentBetHandler?.Choice(false);

        _currentBetHandler = handler;

        BetMultiplier *= _currentBetHandler == null ? 1f : _currentBetHandler.BetMultiplier;
        _currentBetHandler?.Choice(true);

        OnChangeCurrentBetHandler?.Invoke(_currentBetHandler);  
    }
    public void SetBetSlots(List<RouletteSlot> betSlots)
    {
        _betSlots = new List<RouletteSlot>(betSlots);
    }

    public void AddBetMultiplier(float multiplier) => BetMultiplier *= multiplier;
    public void SubtractBetMultiplier(float multiplier) => BetMultiplier /= multiplier;

    private void CheckCanSpin()
    {
        if (_canSpin == false)
            return;
        if (_currentBetHandler == null)
            return;
        if (_betSlots.Count == 0)
            return;

        Manager.Event.SpinInvoke();
    }

    public void Spin()
    {
        _canSpin = false;


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
        if (_betSlots.Count == 0)
        {
            Debug.Log("No Bet Placed");
            return;
        }

        foreach (var slot in _betSlots)
        {
            if (slot == _resultSlot)
            {
                OnWin();
                return;
            }
        }
        OnLose();
    }

    private void OnWin()
    {
        Manager.Event.WinInvoke();
    }

    private void OnLose()
    {
        Manager.Event.LoseInvoke();
    }

    public void StartTurn()
    {
        _canSpin = true;
    }
    public void ClearBet()
    {
        _betSlots.Clear();
        SetCurBetHandelr(null);
        BetMultiplier = 1f;
    }

    private void InitBetHandler()
    {
        foreach (var handler in _betHandlers)
        {
            handler.SetRouletteController(this);
            handler.SetSlots(Slots);
        }
        foreach (var slot in Slots)
        {
            slot.OnSlotInfoChanged += UpdateBetHandler;
        }
    }
    private void UpdateBetHandler()
    {
        foreach (var handler in _betHandlers)
        {
            handler.ResetSlots();
            handler.SetSlots(Slots);
        }
    }

    private void InitRouletteWeight()
    {
        // 슬롯 확률 테이블 초기화
        _weightTable = new WeightTable<RouletteSlot>();
        foreach (var slot in Slots)
        {
            _weightTable.AddElement(slot, slot.Probability);
            slot.OnProbabilityChanged += EditSlotProbability;
        }
    }

    /// <summary>
    /// 확률 변경 
    /// </summary>
    private void EditSlotProbability()
    {
        // 확률 변경시 100% 맞추기
        int changedCount = 0;
        float totalProbability = 0;
        foreach (var s in Slots)
        {
            if (s.IsProbabilityChanged)
            {
                changedCount++;
            }
            totalProbability += s.Probability;
        }
        float extraValue = totalProbability - 100f;
        float decreaseProbability = extraValue / ((Slots.Length - 1) - changedCount);
        // 100% 미만시 변경 안된 슬롯 기준으로 나머지 슬롯 확률 감소
        foreach (var s in Slots)
        {
            if (s.IsProbabilityChanged)
                continue;
            s.Probability -= decreaseProbability;
            if (s.Probability < 0)
                s.Probability = 0;
        }


        // 100% 초과시 변경된 슬롯 기준으로 나머지 슬롯 확률 감소
        float newTotal = 0;
        foreach (var s in Slots)
        {
            newTotal += s.Probability;
        }
        if (newTotal > 100f)
        {
            float diff = newTotal - 100f;
            float reducePerSlot = diff / changedCount;
            foreach (var s in Slots)
            {
                if (s.IsProbabilityChanged == false)
                    continue;
                s.Probability -= reducePerSlot;
                if (s.Probability < 0)
                    s.Probability = 0;
            }
        }

        // 확률 테이블 갱신
        _weightTable = new WeightTable<RouletteSlot>();
        foreach (var s in Slots)
        {
            _weightTable.AddElement(s, s.Probability);
            s.IsProbabilityChanged = false;
        }
    }
    /// <summary>
    /// 룰렛 최초 생성
    /// </summary>
    private void CreateRoulette() => _createHandler.CreateRoulette();

    private void SubscribeEvent()
    {
        Manager.Event.OnCommandSpinEvent += CheckCanSpin;
    }

    [ContextMenu("CreateRoulette")]
    private void CreateRouletteInspector()
    {
        _createHandler.Initialize(this);
        CreateRoulette();
    }
}
