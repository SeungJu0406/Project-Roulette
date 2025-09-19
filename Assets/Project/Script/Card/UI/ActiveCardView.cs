using NSJ_MVVM;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ActiveCardView : BaseView
{
    public ActiveCardStruct CardData;

    private int _index;

    //public event UnityAction<ActiveCardStruct> OnPointClickEvent;
    public event UnityAction<ActiveCardStruct> OnPointEnterEvent;
    public event UnityAction<ActiveCardStruct> OnPointExitEvent;

    public event UnityAction<int> OnCardUsedEvent;

    private GameObject _multiplePointer;
    TMP_Text _nameText;
    protected override void InitAwake()
    {

    }

    protected override void InitGetUI()
    {
        _multiplePointer = GetUI("MultiplePointer");
        _nameText = GetUI<TMP_Text>("Name");
    }

    protected override void InitStart()
    {
        _multiplePointer.SetActive(false);
    }

    private void Update()
    {
        // 마우스 위치 따라오기
        if(CardData.Card.CanMultipleChoice)
            _multiplePointer.transform.position = Input.mousePosition;
    }

    protected override void SubscribeEvents()
    {

    }

    protected override void OnPointUp(PointerEventData eventData)
    {
        StartCoroutine(PointUpRoutine());
    }

    IEnumerator PointUpRoutine()
    {
        _multiplePointer.SetActive(false);
        yield return null;
        OnCardUsedEvent?.Invoke(_index);
        _multiplePointer.SetActive(CardData.Card.CanMultipleChoice == true);
    }
    protected override void OnPointEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject == _multiplePointer)
            return;

        OnPointEnterEvent?.Invoke(CardData);
    }
    protected override void OnPointExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject == _multiplePointer)
            return;

        OnPointExitEvent?.Invoke(CardData);
    }

    public void SetCard(ActiveCardStruct data)
    {
        CardData = data;
        SetName(data);
    }
    public void SetIndex(int index)
    {
        _index = index;
    }

    private void SetName(ActiveCardStruct data)
    {
        _nameText.text = data.Data.Name;
    }
}
