using UnityEngine;

public class ChipController : MonoBehaviour
{
    [SerializeField] private ChipControllerModel _model;
    private int _chipCount { get => _model.HoldChip; set => _model.HoldChip = value; }
    private int _betCount { get => _model.BetChip; set => _model.BetChip = value; }

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            BetChip(12);
        if (Input.GetKeyDown(KeyCode.W))
            BetChip(-5);
    }

    private void BetChip(int chipCount)
    {
        if (chipCount >= 0)
        {
            int betChip = Mathf.Min(_chipCount, chipCount);
            _betCount += betChip;
            _chipCount -= betChip;
        }
        else
        {
            int returnChip = Mathf.Min(_betCount, -chipCount);
            _betCount -= returnChip;
            _chipCount += returnChip;
        }
    }

    private void CollectChip(float betMultiplier)
    {
        int winAmount = Mathf.RoundToInt(_betCount * betMultiplier);
        _chipCount += winAmount;
        _betCount = 0;
    }
    private void LoseChip()
    {
        _betCount = 0;
    }
}
