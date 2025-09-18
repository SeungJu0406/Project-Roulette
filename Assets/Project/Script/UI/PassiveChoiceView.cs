using NSJ_MVVM;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PassiveChoiceView : BaseView<PassiveChoiceViewModel>
{
    private List<PassiveChoiceCardView> _cardViews = new List<PassiveChoiceCardView>();

    [SerializeField] private PassiveChoiceCardView _cardViewPrefab;

    [SerializeField] private Vector2 _descriptionBoxOffset;

    private Transform _layout;

    private GameObject _descriptionBox;

    private TMP_Text _name;
    private TMP_Text _description;
    protected override void ClearView()
    {

    }

    protected override void InitAwake()
    {

    }

    protected override void InitGetUI()
    {
        _layout = GetUI<Transform>("Layout");
        _descriptionBox = GetUI("DescriptionBox");
        _name = GetUI<TMP_Text>("Name");
        _description = GetUI<TMP_Text>("Description");
    }

    protected override void InitStart()
    {
        _descriptionBox.SetActive(false);
    }

    private void Update()
    {
        if (_descriptionBox.activeSelf)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform.parent, Input.mousePosition, null, out pos);
            _descriptionBox.transform.localPosition = pos + _descriptionBoxOffset;
        }
    }

    protected override void OnViewModelSet()
    {
        Model.OnAddPassiveEvent += AddCardView;
        Model.OnRemovePassiveEvent += RemoveCardView;
    }

    protected override void SubscribeEvents()
    {

    }

    private void AddCardView(int index)
    {
        PassiveChoiceCardView newCard = Instantiate(_cardViewPrefab, _layout);

        // 설정
        PassiveCardData passiveCardData = Model.PassiveChoices[Model.PassiveChoices.Count - 1];
        newCard.SetCard(passiveCardData);
        newCard.SetIndex(index);

        newCard.OnPointEnterEvent += ShowDescription;
        newCard.OnPointExitEvent += HideDescription;
        newCard.OnChoiceEvent += Model.ReceiveOnChoicePassiveInvoke;

        // 리스트에 추가
        _cardViews.Add(newCard);
    }

    private void RemoveCardView(int index)
    {
        PassiveChoiceCardView removeCard = _cardViews[index];
        // 해제
        removeCard.OnPointEnterEvent -= ShowDescription;
        removeCard.OnPointExitEvent -= HideDescription;
        removeCard.OnChoiceEvent -= Model.ReceiveOnChoicePassiveInvoke;
        // 삭제
        _cardViews.RemoveAt(index);
        Destroy(removeCard.gameObject);
    }

    private void ShowDescription(PassiveCardData data)
    {
        PassiveCardData passiveCardData = data;

        _descriptionBox.SetActive(true);
        _name.text = passiveCardData.Name;
        _description.text = passiveCardData.Description;
    }
    private void HideDescription(PassiveCardData data)
    {
        _descriptionBox.SetActive(false);
    }
}

public class PassiveChoiceViewModel : BaseViewModel<ShopModel>
{
    public List<PassiveCardData> PassiveChoices => Model.PassiveCards;

    public event UnityAction<int> OnAddPassiveEvent;
    public event UnityAction<int> OnRemovePassiveEvent;

    protected override void OnModelRemove()
    {
        Model.OnAddPassiveEvent -= AddPassive;
        Model.OnRemovePassiveEvent -= RemovePassive;
    }

    protected override void OnModelSet()
    {
        Model.OnAddPassiveEvent += AddPassive;
        Model.OnRemovePassiveEvent += RemovePassive;
    }

    public void ReceiveOnChoicePassiveInvoke(int index)
    {
        Model.ReceiveOnChoicePassiveInvoke(index);
    }

    private void AddPassive(int card)
    {
        OnAddPassiveEvent?.Invoke(card);
    }
    private void RemovePassive(int card)
    {
        OnRemovePassiveEvent?.Invoke(card);
    }
}
