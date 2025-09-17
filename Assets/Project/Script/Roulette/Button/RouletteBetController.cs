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
    private void Awake()
    {
        _choiceObject = GetComponentInChildren<ChoiceObject>();
        _choiceObject?.gameObject.SetActive(false);
        InitAwake();
    }
    private void Start()
    {
        TurnManager.Instance.OnTurnEndEvent += () => Choice(false);
    }

    protected override void OnPointClick(PointerEventData eventData)
    {
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

}
