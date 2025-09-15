using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class RouletteInputController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] protected List<RouletteSlot> _slots;

    private void Awake()
    {
        InitAwake();
        RouletteController roulette = GetComponentInParent<RouletteController>();
        if (roulette != null)
            SetSlots(roulette.Slots);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        string debugLog = $"{gameObject.name} Clicked. Slots: ";
        foreach (var slot in _slots)
        {
            debugLog += $"{slot.Number} ";
        }
        Debug.Log(debugLog);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
    }
    protected virtual void InitAwake() { }
    protected abstract void SetSlots(RouletteSlot[] allSlots);
}
