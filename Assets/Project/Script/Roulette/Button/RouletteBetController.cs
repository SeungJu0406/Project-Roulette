using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class RouletteBetController : PointHandler
{
    [SerializeField] private float _betMultiplier = 1f;
    [SerializeField] protected List<RouletteSlot> _slots;

    private RouletteController _rouletteController;
    private void Awake()
    {
        InitAwake();
    }
    protected override void OnPointClick(PointerEventData eventData)
    {
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

}
