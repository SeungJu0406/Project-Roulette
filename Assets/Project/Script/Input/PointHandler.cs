using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PointHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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

    protected abstract void OnPointEnter(PointerEventData eventData);
    protected abstract void OnPointExit(PointerEventData eventData);
    protected abstract void OnPointClick(PointerEventData eventData);
}
