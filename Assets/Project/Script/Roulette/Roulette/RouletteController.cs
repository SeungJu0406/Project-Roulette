using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using WeightUtility;

public class RouletteController : MonoBehaviour
{

    public RouletteSlot[] Slots;
    [SerializeField] private List<RouletteSlot> _betSlots;

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
                Debug.Log("Win! Number: " + slot.Number);              
                return;
            }
        }
        Debug.Log("Lose! Winning Number: " + _resultSlot.Number);
        _betSlots.Clear();
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
        // ½½·Ô È®·ü Å×ÀÌºí ÃÊ±âÈ­
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
    /// ·ê·¿ ÃÖÃÊ »ý¼º
    /// </summary>
    private void CreateRoulette() => _createHandler.CreateRoulette();

    [ContextMenu("CreateRoulette")]
    private void CreateRouletteInspector()
    {
        _createHandler.Initialize(this);
        CreateRoulette();
    }
}
