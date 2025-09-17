using System.Collections;
using UnityEngine;
using Utility;

public class ChipController : MonoBehaviour
{
    [SerializeField] private ChipControllerModel _model;
    [SerializeField] private float _collectChipDelay = 0.02f;
    public int HoldChip { get => _model.HoldChip; set => _model.HoldChip = value; }
    public int BettingChip { get => _model.BetChip; set => _model.BetChip = value; }

    private void Awake()
    {
        _model.InitModel();

        _model.OnBetReceiver += BetChip;
    }

    private void Start()
    {
        RouletteController rouletteController = FindAnyObjectByType<RouletteController>();
        rouletteController.OnWinEvent += CollectChip;
        rouletteController.OnLoseEvent += LoseChip;
    }

    private void BetChip(int chipCount)
    {
        if (chipCount >= 0)
        {
            int betChip = Mathf.Min(HoldChip, chipCount);
            BettingChip += betChip;
            HoldChip -= betChip;
        }
        else
        {
            int returnChip = Mathf.Min(BettingChip, -chipCount);
            BettingChip -= returnChip;
            HoldChip += returnChip;
        }
    }

    private void CollectChip(float betMultiplier)
    {
        int winAmount = Mathf.RoundToInt(BettingChip * betMultiplier);
        StartCoroutine(CollectChipRoutine(winAmount));
    }
    private void LoseChip()
    {
        StartCoroutine(LoseChipRoutine());
    }

    IEnumerator CollectChipRoutine(int winAmount)
    {
        while (winAmount > 0)
        {
            HoldChip += 1;
            winAmount -= 1;

            if(BettingChip > 0)
                BettingChip -= 1;
            yield return _collectChipDelay.Second();
        }

        // TODO : 임시 턴 종료 타이밍
        yield return 1f.Second();
        TurnManager.EndTurnInvoke();
    }
    IEnumerator LoseChipRoutine()
    {
        while (BettingChip > 0)
        {
            BettingChip -= 1;
            yield return _collectChipDelay.Second();
        }


        // TODO : 임시 턴 종료 타이밍
        yield return 1f.Second();
        TurnManager.EndTurnInvoke();
    }
}
