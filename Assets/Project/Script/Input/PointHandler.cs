using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class PointHandler : MonoBehaviour, 
    IPointerEnterHandler, 
    IPointerExitHandler, 
    IPointerClickHandler, 
    IPointerDownHandler, 
    IPointerUpHandler,
    IDragHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointClick(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointExit(eventData);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointDown(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointUp(eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {
        OnPointDrag(eventData);
    }
    protected virtual void OnPointEnter(PointerEventData eventData) { }
    protected virtual void OnPointExit(PointerEventData eventData) { }
    protected virtual void OnPointClick(PointerEventData eventData) { }

    protected virtual void OnPointDown(PointerEventData eventData) { }
    protected virtual void OnPointUp(PointerEventData eventData) { }
    protected virtual void OnPointDrag(PointerEventData eventData) { }


}
