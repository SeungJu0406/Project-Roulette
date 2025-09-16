using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;
using WeightUtility;

public class RouletteController : MonoBehaviour
{
    public RouletteSlot[] Slots;

    public event UnityAction<float> OnWinEvent;
    public event UnityAction OnLoseEvent;

    [SerializeField] private List<RouletteSlot> _betSlots;
    private RouletteBetController _currentBetHandler;
    [SerializeField] private RouletteCreateHandler _createHandler;



    private RouletteBetController[] _betHandlers;
    private WeightTable<RouletteSlot> _weightTable;

    private RouletteSlot _resultSlot;
    private void Awake()
    {
        _betHandlers = GetComponentsInChildren<RouletteBetController>(true);
    }

    private void Start()
    {
        InitRoulette();
        InitBetHandler();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Spin();
            CheckBetResult();
        }
    }
    public void SetCurBetHandelr(RouletteBetController handler)
    {
        _currentBetHandler = handler;
    }
    public void SetBetSlots(List<RouletteSlot> betSlots)
    {
        _betSlots = new List<RouletteSlot>(betSlots);
    }

    public void Spin()
    {
        _resultSlot?.SetOutline(false);
        _resultSlot = _weightTable.Pick();
        _resultSlot?.RevouleSlot();
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
        float betMultiplier = _currentBetHandler == null ? 0 : _currentBetHandler.BetMultiplier;

        // TODO : 추가 배율 계산

        OnWinEvent?.Invoke(betMultiplier);
        ClearBets();
    }

    private void OnLose()
    {
        OnLoseEvent?.Invoke();
        ClearBets();
    }

    private void ClearBets()
    {
        _betSlots.Clear();
        _currentBetHandler = null;
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

    [ContextMenu("CreateRoulette")]
    private void CreateRouletteInspector()
    {
        _createHandler.Initialize(this);
        CreateRoulette();
    }
}
