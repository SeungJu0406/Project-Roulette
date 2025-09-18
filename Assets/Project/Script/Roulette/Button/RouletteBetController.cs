using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class RouletteBetController : PointHandler
{
    public float BetMultiplier => _betMultiplier;
    [SerializeField] private float _betMultiplier = 1f;
    [SerializeField] protected List<RouletteSlot> _slots;

    private ChoiceObject _choiceObject;
    private RouletteController _rouletteController;

    private bool _canClick = true;
    private void Awake()
    {

        _choiceObject = GetComponentInChildren<ChoiceObject>();
        _choiceObject?.gameObject.SetActive(false);
        InitAwake();
    }
    private void Start()
    {
        Manager.Event.OnTurnEndEvent += EndTurn;
        Manager.Event.OnSpinEvent += OnSpin;
        Manager.Event.OnTurnStartEvent += StartTurn;
    }

    protected override void OnPointClick(PointerEventData eventData)
    {
        if (_canClick == false) return;

        _rouletteController.SetCurBetHandelr(this);
        _rouletteController.SetBetSlots(_slots);
    }
    protected override void OnPointEnter(PointerEventData eventData)
    {

    }
    protected override void OnPointExit(PointerEventData eventData)
    {

    }
    public abstract void SetSlots(RouletteSlot[] allSlots);
    protected virtual void InitAwake() { }

    public void SetRouletteController(RouletteController controller) => _rouletteController = controller;

    public void Choice(bool isChoice)
    {
        _choiceObject.gameObject.SetActive(isChoice);
    }

    private void EndTurn()
    {
         Choice(false);
    }
    private void StartTurn()
    {
        _canClick = true;
    }
    private void OnSpin()
    {
        _canClick = false;
    }

}
