using NSJ_MVVM;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Utility;

public class PassiveCardView : BaseView
{
    public PassiveCardStruct CardData;

    public event UnityAction<PassiveCardStruct> OnPointClickEvent;
    public event UnityAction<PassiveCardStruct> OnPointEnterEvent;
    public event UnityAction<PassiveCardStruct> OnPointExitEvent;

    GameObject _applyImage;

    protected override void InitAwake()
    {
       
    }

    protected override void InitGetUI()
    {
        _applyImage = GetUI("ApplyImage");
    }

    protected override void InitStart()
    {
         _applyImage.SetActive(false);
    }

    protected override void SubscribeEvents()
    {
        CardData.Card.OnApplyEvent += ShowApplyDisplay;
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();
        CardData.Card.OnApplyEvent -= ShowApplyDisplay;
    }


    protected override void OnPointEnter(PointerEventData eventData)
    {
       OnPointEnterEvent?.Invoke(CardData);
    }
    protected override void OnPointExit(PointerEventData eventData)
    {
        OnPointExitEvent?.Invoke(CardData);
    }

    public void SetCard(PassiveCardStruct data)
    {
        CardData = data;
    }

    public void ShowApplyDisplay()
    {
        StartCoroutine(DisplayApplyRoutine());
    }

    IEnumerator DisplayApplyRoutine()
    {
        _applyImage.SetActive(true);
        yield return 0.5f.Second();
        _applyImage.SetActive(false);
    }
}
