using System.Collections.Generic;
using UnityEngine;

public class RouletteController : MonoBehaviour
{

    public RouletteSlot[] Slots;
    [SerializeField] private List<RouletteSlot> _betSlots;

    // TODO : Å×½ºÆ®¿ë
    [SerializeField] private int _randomIndex;

    [SerializeField] private RouletteCreateHandler _createHandler;

    private RouletteBetController[] _betHandlers;
    private void Awake()
    {
        _betHandlers = GetComponentsInChildren<RouletteBetController>(true);
    }

    private void Start()
    {
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
        _randomIndex = Random.Range(0, Slots.Length);
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
            if(slot == Slots[_randomIndex])
            {
                Debug.Log("Win! Number: " + slot.Number);
                return;
            }
        }
        Debug.Log("Lose! Winning Number: " + Slots[_randomIndex].Number);

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
