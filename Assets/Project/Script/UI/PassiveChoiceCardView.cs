using NSJ_MVVM;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PassiveChoiceCardView : BaseView
{
    public PassiveCardData CardData;

    public event UnityAction<PassiveCardData> OnPointClickEvent;
    public event UnityAction<PassiveCardData> OnPointEnterEvent;
    public event UnityAction<PassiveCardData> OnPointExitEvent;

    public event UnityAction<int> OnChoiceEvent;

    private Button _choice;
    private int _index;
    protected override void InitAwake()
    {

    }

    protected override void InitGetUI()
    {
        _choice = GetUI<Button>("Choice");
    }

    protected override void InitStart()
    {

    }

    protected override void SubscribeEvents()
    {
        _choice.onClick.AddListener(OnChoice);
    }


    protected override void OnPointEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject == _choice.gameObject)
            return;

        OnPointEnterEvent?.Invoke(CardData);
    }
    protected override void OnPointExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject == _choice.gameObject)
            return;

        OnPointExitEvent?.Invoke(CardData);
    }

    public void SetCard(PassiveCardData data)
    {
        CardData = data;
    }
    public void SetIndex(int index)
    {
        _index = index;
    }

    private void OnChoice()
    {
        OnChoiceEvent?.Invoke(_index);
    }
}
