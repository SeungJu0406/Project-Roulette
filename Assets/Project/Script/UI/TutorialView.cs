using NSJ_MVVM;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialView : BaseView
{
    [SerializeField] private NumberChangerData _tutorialCard;
    List<GameObject> tutorialList = new List<GameObject>();

    Button _startTutorial;
    Button _startGame;
    Button _nextTutorial7Button;
    Button _nextTutorial9Button;

    int currentIndex = 0;

    ChipController _chip;
    RouletteController _roullette;
    CardController _card;
    protected override void InitAwake()
    {
       _chip = FindAnyObjectByType<ChipController>();
        _roullette = FindAnyObjectByType<RouletteController>();
         _card = FindAnyObjectByType<CardController>();
    }

    protected override void InitGetUI()
    {
        for(int i = 0; i < 10 ; i++)
        {
            tutorialList.Add(GetUI($"{i}"));
            tutorialList[i].SetActive(false);
        }


        _startTutorial = GetUI<Button>("StartTutorial");
        _startGame = GetUI<Button>("StartGame");
        _nextTutorial7Button = GetUI<Button>("NextTutorial7Button");   
        _nextTutorial9Button = GetUI<Button>("NextTutorial9Button");
    }

    protected override void InitStart()
    {
        _chip.Model.OnBetChipChanged += OnBetChipChanged;
        _roullette.OnChangeCurrentBetHandler += OnChangeCurrentBetHandler;
        Manager.Event.OnSpinEvent += OnSpin;
        Manager.Event.OnWinEvent += OnSpinResult;
        Manager.Event.OnLoseEvent += OnSpinResult;
        Manager.Event.OnTurnEndEvent += OnTurnEnd;
        _card.OnUseActiveCardEvent += OnUseActiveCard;

        ChangeTutorial(currentIndex);
        
    }



    protected override void SubscribeEvents()
    {
        _startTutorial.onClick.AddListener(NextTutorial);
        _nextTutorial7Button.onClick.AddListener(NextTutorial);
        _nextTutorial9Button.onClick.AddListener(NextTutorial);
        _startGame.onClick.AddListener(StartGame);

    }

    private void NextTutorial()
    {
        currentIndex++;
        ChangeTutorial(currentIndex);
    }

    private void ChangeTutorial(int index)
    {
        for(int i = 0; i < tutorialList.Count; i++)
        {
            tutorialList[i].SetActive(i == index);
        }
    }

    private void OnBetChipChanged(int chip)
    {
        if (currentIndex != 1)
            return;
        NextTutorial();
    }
    private void OnChangeCurrentBetHandler(RouletteBetController betSlot)
    {
        if (currentIndex != 2)
            return;

        NextTutorial();
    }
    private void OnSpin()
    {
        if (currentIndex != 3)
            return;
        NextTutorial();
    }

    private void OnSpinResult()
    {
        if (currentIndex != 4)
            return;
        NextTutorial();
    }
    private void OnTurnEnd()
    {
        if (currentIndex != 5)
            return;

        CardController cardController = FindAnyObjectByType<CardController>();
        cardController.AddActiveCard(_tutorialCard);

        NextTutorial();
    }
    private void OnUseActiveCard(ActiveCardStruct card)
    {
        if (currentIndex != 7)
            return;

        NextTutorial();
    }
    private void StartGame()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }
}
