using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private ShopModel _model;
    [SerializeField] private int _passiveChoiceCount = 3;
    private List<PassiveCardData> _passiveChoices => _model.PassiveCards;

    private CardController _cardController;
    private void Awake()
    {
        _model.InitModel(this);
        _cardController = FindAnyObjectByType<CardController>();

        _model.OnChoicePassiveReceiver += ChoicePassive;
    }

    public void SetPassiveChoice()
    {
        // 초기화
        _passiveChoices.Clear();

        // 랜덤 3개 획득
        for (int i = 0; i < _passiveChoiceCount; i++)
        {
            PassiveCardData randomCard = CardDatabase.GetRandomPassive();
            if (randomCard == null)
            {
                break;
            }
            CardDatabase.RemovePassive(randomCard);

            _passiveChoices.Add(randomCard);
            _model.OnAddPassiveEventInvoke(i);
        }
    }

    public void ChoicePassive(int index)
    {
        PassiveCardData card = _passiveChoices[index];

        // 카드 적용
        _cardController.AddPassiveCard(card);


        // 선택하지 않은 카드 반환
        foreach (var passive in _passiveChoices)
        {
            if(passive != card)
            {
                CardDatabase.ReturnPassive(passive);
            }
        }

        for(int i = _passiveChoices.Count - 1; i >= 0; i--)
        {
            _model.OnRemovePassiveEventInvoke(i);
        }

        // 초기화
        _passiveChoices.Clear();
    }
}
