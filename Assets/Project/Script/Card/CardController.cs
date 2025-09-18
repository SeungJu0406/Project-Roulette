using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ActiveCardStruct
{
    public ActiveCardData Data;
    public ActiveCard Card;
}
[System.Serializable]
public struct PassiveCardStruct
{
    public PassiveCardData Data;
    public PassiveCard Card;
}
public class CardController : MonoBehaviour
{
    // field
    [SerializeField] private CardControllerModel _model;

    private List<ActiveCardStruct> _activeCards => _model.ActiveCards;
    private List<PassiveCardStruct> _passiveCards => _model.PassiveCards;
    private int _maxActiveCardCount { get => _model.MaxActiveCardCount; set => _model.MaxActiveCardCount = value; }

    private RouletteController _roulette;
    private ChipController _chip;

    [SerializeField] private PassiveCardData _test;

    private void Awake()
    {
        _model.InitModel(this);

        _roulette = FindAnyObjectByType<RouletteController>();
        _chip = FindAnyObjectByType<ChipController>();

        _model.OnUseCardEventReciever += UseActiveCard;
    }

    private void Start()
    {
        SubscribeEvent();
    }
    public void SubscribeEvent()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            AddPassiveCard(_test);
        }
    }

    // methods

    public void AddRandomActiveCard()
    {
        ActiveCardData active = CardDatabase.GetRandomActive();
        AddActiveCard(active);
    }

    // 액티브 카드 추가
    public void AddActiveCard(ActiveCardData activeCard)
    {
        if (_activeCards.Count >= _maxActiveCardCount)
            return;

        ActiveCardStruct newActive = new ActiveCardStruct
        {
            Data = activeCard,
            Card = activeCard.GetCard()
        };

        newActive.Card.SetRoulette(_roulette);
        newActive.Card.SetChip(_chip);
        newActive.Card.SetCardController(this);

        _activeCards.Add(newActive);
        _model.OnActiveCardChangedInvoke(_activeCards.Count - 1);
    }
    // 액티브 카드 사용
    public void UseActiveCard(int index)
    {
        bool applySuccess = _activeCards[index].Card.Apply();
        if (applySuccess == true)
        {
            RemoveActiveCard(index);
        }
    }
    // 액티브 카드 제거
    public void RemoveActiveCard(int index)
    {
        _activeCards.RemoveAt(index);
        _model.OnActiveCardChangedInvoke(index);
    }

    // 패시브 카드 추가
    public void AddPassiveCard(PassiveCardData passiveCard)
    {
        CardDatabase.RemovePassive(passiveCard);

        PassiveCardStruct newPassive = new PassiveCardStruct
        {
            Data = passiveCard,
            Card = passiveCard.GetCard()
        };

        newPassive.Card.SetRoulette(_roulette);
        newPassive.Card.SetChip(_chip);
        newPassive.Card.SetCardController(this);

        _passiveCards.Add(newPassive);       

        _model.OnPassiveCardChangedInvoke(_passiveCards.Count - 1);
    }
    // 패시브 카드 제거
    public void RemovePassiveCard(int index)
    {
        _passiveCards.RemoveAt(index);
        _model.OnPassiveCardChangedInvoke(index);
    }

    public void OnSpin()
    {
        foreach (var passive in _passiveCards)
        {
            passive.Card.OnSpin();
        }
    }
    public void OnWin()
    {
        foreach (var passive in _passiveCards)
        {
            passive.Card.OnWin();
        }
    }
    public void OnLose()
    {
        foreach (var passive in _passiveCards)
        {
            passive.Card.OnLose();
        }
    }
    public void OnTurnStart()
    {
        foreach (var passive in _passiveCards)
        {
            passive.Card.OnTurnStart();
        }
    }
    public void OnTurnEnd()
    {
        foreach (var passive in _passiveCards)
        {
            passive.Card.OnTurnEnd();
        }
    }
}
