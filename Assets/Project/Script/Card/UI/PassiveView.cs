using NSJ_MVVM;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PassiveView : BaseView<PassiveViewModel>
{
    private List<PassiveCardView> _cardViews = new List<PassiveCardView>();

    [SerializeField] private PassiveCardView _cardViewPrefab;

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
        Model.OnPassiveCardsUpdated += UpdatePassiveCards;
    }

    protected override void SubscribeEvents()
    {

    }

    private void UpdatePassiveCards(int index)
    {
        // ���� ī�� ������ �� ī�� ���� ��
        if (Model.PassiveCards.Count > _cardViews.Count)
        {
            AddCard();
        }
        else
        {
            RemoveCard(index);
        }
    }

    private void AddCard()
    {
        PassiveCardView newCard = Instantiate(_cardViewPrefab, _layout);

        // ����
        PassiveCardData passiveCardData = Model.PassiveCards[Model.PassiveCards.Count - 1].Data;
        newCard.SetCard(passiveCardData);

        newCard.OnPointEnterEvent += ShowDescription;
        newCard.OnPointExitEvent += HideDescription;
        // ����Ʈ�� �߰�
        _cardViews.Add(newCard);
    }
    private void RemoveCard(int index)
    {
        PassiveCardView removeCard = _cardViews[index];
        // ����
        removeCard.OnPointEnterEvent -= ShowDescription;
        removeCard.OnPointExitEvent -= HideDescription;
        // ����
        _cardViews.RemoveAt(index);
        Destroy(removeCard.gameObject);
    }

    private void ShowDescription(PassiveCardData data)
    {
        _descriptionBox.SetActive(true);
        _name.text = data.Name;
        _description.text = data.Description;
    }
    private void HideDescription(PassiveCardData data)
    {
        _descriptionBox.SetActive(false);
    }
}

public class PassiveViewModel : BaseViewModel<CardControllerModel>
{
    public List<PassiveCardStruct> PassiveCards;

    public event UnityAction<int> OnPassiveCardsUpdated;
    protected override void OnModelRemove()
    {
        Model.OnPassiveCardsChanged -= UpdatePassiveCards;
    }

    protected override void OnModelSet()
    {
        PassiveCards = Model.PassiveCards;

        Model.OnPassiveCardsChanged += UpdatePassiveCards;
    }

    private void UpdatePassiveCards(int index)
    {
        OnPassiveCardsUpdated?.Invoke(index);
    }
}
