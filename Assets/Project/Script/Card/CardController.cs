using System.Collections.Generic;
using System.ComponentModel;
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
    [SerializeField] private List<ActiveCardStruct> _activeCards;
    [SerializeField] private List<PassiveCardStruct> _passiveCards;


    [SerializeField] private PassiveCardData _test;

    private RouletteController _roulette;
    private ChipController _chip;

    private void Awake()
    {
        _roulette = FindAnyObjectByType<RouletteController>();
        _chip = FindAnyObjectByType<ChipController>();
    }

    private void Start()
    {
        SubscribeEvent();
    }
    public void SubscribeEvent()
    {
        _roulette.OnSpinEvent += OnSpin;
        _roulette.OnWinEvent += (float winAmount) => OnWin();
        _roulette.OnLoseEvent += OnLose;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddPassiveCard(_test);
        }
    }

    // method

    // 액티브 카드 추가
    public void AddActiveCard(ActiveCardData activeCard)
    {
        ActiveCardStruct newActive = new ActiveCardStruct
        {
            Data = activeCard,
            Card = activeCard.GetCard()
        };

        newActive.Card.SetRoulette(_roulette);
        newActive.Card.SetChip(_chip);

        _activeCards.Add(newActive);
    }
    // 액티브 카드 사용
    public void UseActiveCard(int index)
    {
        //_activeCards[index].Card.Use();
        RemoveActiveCard(index);
    }
    // 액티브 카드 제거
    public void RemoveActiveCard(int index)
    {
        _activeCards.RemoveAt(index);
    }

    // 패시브 카드 추가
    public void AddPassiveCard(PassiveCardData passiveCard)
    {
        PassiveCardStruct newPassive = new PassiveCardStruct
        {
            Data = passiveCard,
            Card = passiveCard.GetCard()
        };

        newPassive.Card.SetRoulette(_roulette);
        newPassive.Card.SetChip(_chip);

        _passiveCards.Add(newPassive);
    }
    // 패시브 카드 제거
    public void RemovePassiveCard(int index)
    {
        _passiveCards.RemoveAt(index);
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
}
    