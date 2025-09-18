using NSJ_MVVM;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ActiveView : BaseView<ActiveViewModel>
{
    private List<ActiveCardView> _cardViews = new List<ActiveCardView>();

    [SerializeField] private ActiveCardView _cardViewPrefab;

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
        Model.OnActiveCardsUpdated += UpdatePassiveCards;
    }

    protected override void SubscribeEvents()
    {

    }

    private void UpdatePassiveCards(int index)
    {
        // 실제 카드 개수와 뷰 카드 개수 비교
        if (Model.ActiveCards.Count > _cardViews.Count)
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
        // 설정
        ActiveCardStruct passiveCardData = Model.ActiveCards[Model.ActiveCards.Count - 1];

        ActiveCardView newCard = Instantiate(_cardViewPrefab, _layout);
        newCard.SetCard(passiveCardData);
        newCard.SetIndex(_cardViews.Count);
        newCard.OnPointEnterEvent += ShowDescription;
        newCard.OnPointExitEvent += HideDescription;
        newCard.OnCardUsedEvent += UseCard;
        // 리스트에 추가
        _cardViews.Add(newCard);
    }

    private void RemoveCard(int index)
    {
        ActiveCardView removeCard = _cardViews[index];
        // 해제
        removeCard.OnPointEnterEvent -= ShowDescription;
        removeCard.OnPointExitEvent -= HideDescription;
        removeCard.OnCardUsedEvent -= UseCard;
        // 삭제
        _cardViews.RemoveAt(index);
        Destroy(removeCard.gameObject);

        // 인덱스 재설정
        for (int i = index; i < _cardViews.Count; i++)
        {
            _cardViews[i].SetIndex(i);
        }
    }

    private void ShowDescription(ActiveCardStruct data)
    {
        ActiveCardData activeCardData = data.Data;

        _descriptionBox.SetActive(true);
        _name.text = activeCardData.Name;
        _description.text = activeCardData.Description;
    }
    private void HideDescription(ActiveCardStruct data)
    {
        _descriptionBox.SetActive(false);
    }

    private void UseCard(int index)
    {
        Model.UseCard(index);
    }

}
public class ActiveViewModel : BaseViewModel<CardControllerModel>
{
    public List<ActiveCardStruct> ActiveCards;

    public event UnityAction<int> OnActiveCardsUpdated;

    public void UseCard(int index)
    {
        Model.ReceiveUseCardEvent(index);
    }

    protected override void OnModelRemove()
    {
        Model.OnActiveCardsChanged -= UpdatePassiveCards;
    }

    protected override void OnModelSet()
    {
        ActiveCards = Model.ActiveCards;

        Model.OnActiveCardsChanged += UpdatePassiveCards;
    }

    private void UpdatePassiveCards(int index)
    {
        OnActiveCardsUpdated?.Invoke(index);
    }
}
