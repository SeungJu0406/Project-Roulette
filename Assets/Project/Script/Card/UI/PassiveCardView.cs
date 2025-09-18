using NSJ_MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PassiveCardView : BaseView
{
    public PassiveCardData CardData;

    public event UnityAction<PassiveCardData> OnPointClickEvent;
    public event UnityAction<PassiveCardData> OnPointEnterEvent;
    public event UnityAction<PassiveCardData> OnPointExitEvent;

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



    protected override void OnPointEnter(PointerEventData eventData)
    {
       OnPointEnterEvent?.Invoke(CardData);
    }
    protected override void OnPointExit(PointerEventData eventData)
    {
        OnPointExitEvent?.Invoke(CardData);
    }

    public void SetCard(PassiveCardData data)
    {
        CardData = data;
    }
}
