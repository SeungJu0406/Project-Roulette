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
        // �ʱ�ȭ
        _passiveChoices.Clear();

        // ���� 3�� ȹ��
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

        // ī�� ����
        _cardController.AddPassiveCard(card);


        // �������� ���� ī�� ��ȯ
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

        // �ʱ�ȭ
        _passiveChoices.Clear();
    }
}
