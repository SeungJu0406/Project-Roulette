using System.Collections;
using UnityEngine;
using Utility;

public class ChipController : MonoBehaviour
{
    [SerializeField] private ChipControllerModel _model;

    public ChipControllerModel Model => _model;
    public int StartChip { get => _model.StartChip; set => _model.StartChip = value; }
    public int HoldChip { get => _model.HoldChip; set => _model.HoldChip = value; }
    public int BettingChip { get => _model.BetChip; set => _model.BetChip = value; }

    private void Awake()
    {

        _model.InitModel(this);

        _model.OnBetReceiver += BetChip;
    }

    private void Start()
    {
        RouletteController rouletteController = FindAnyObjectByType<RouletteController>();
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

    public void CollectChip(float betMultiplier)
    {
        int winAmount = Mathf.RoundToInt(BettingChip * betMultiplier);
        StartCoroutine(CollectChipRoutine(winAmount));
    }
    public void LoseChip()
    {
        StartCoroutine(LoseChipRoutine());
    }

    IEnumerator CollectChipRoutine(int winAmount)
    {
        float delay = 1f / winAmount;
        while (winAmount > 0)
        {
            HoldChip += 1;
            winAmount -= 1;

            if(BettingChip > 0)
                BettingChip -= 1;
            yield return delay.Second();
        }

        // TODO : 임시 턴 종료 타이밍
        yield return 1f.Second();
        Manager.Event.EndTurnInvoke();
    }
    IEnumerator LoseChipRoutine()
    {
        float delay = 1f / BettingChip;
        while (BettingChip > 0)
        {
            BettingChip -= 1;
            yield return delay.Second();
        }


        // TODO : 임시 턴 종료 타이밍
        yield return 1f.Second();
        Manager.Event.EndTurnInvoke();
    }
}
