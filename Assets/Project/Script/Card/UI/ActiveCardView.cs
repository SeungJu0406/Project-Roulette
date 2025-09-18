using NSJ_MVVM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ActiveCardView : BaseView
{
    public ActiveCardStruct CardData;

    private int _index;

    public event UnityAction<ActiveCardStruct> OnPointClickEvent;
    public event UnityAction<ActiveCardStruct> OnPointEnterEvent;
    public event UnityAction<ActiveCardStruct> OnPointExitEvent;

    public event UnityAction<int> OnCardUsedEvent;

    protected override void InitAwake()
    {

    }

    protected override void InitGetUI()
    {

    }

    protected override void InitStart()
    {

    }

    protected override void SubscribeEvents()
    {

    }

    protected override void OnPointUp(PointerEventData eventData)
    {
        OnCardUsedEvent?.Invoke(_index);
    }


    protected override void OnPointEnter(PointerEventData eventData)
    {
        OnPointEnterEvent?.Invoke(CardData);
    }
    protected override void OnPointExit(PointerEventData eventData)
    {
        OnPointExitEvent?.Invoke(CardData);
    }

    public void SetCard(ActiveCardStruct data)
    {
        CardData = data;
    }
    public void SetIndex(int index)
    {
        _index = index;
    }
}
