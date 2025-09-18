using NSJ_MVVM;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ChipControllerModel : BaseModel
{
    public int StartChip { get => _startChip; set { _startChip = value; } }
    public int HoldChip { get => _holdChip; set { _holdChip = value; OnHoldChipChanged?.Invoke(HoldChip); } }
    public int BetChip { get => _betChip; set { _betChip = value; OnBetChipChanged?.Invoke(BetChip); } }

    public event UnityAction<int> OnHoldChipChanged;
    public event UnityAction<int> OnBetChipChanged;

    [SerializeField] private int _startChip = 5;
    [SerializeField] private int _holdChip;
    [SerializeField] private int _betChip;

    public event UnityAction<int> OnBetReceiver;

    protected override void Awake()
    {
        BindingSystem<ChipViewModel>.Bind(this);
    }

    protected override void Destroy()
    {
        BindingSystem<ChipViewModel>.UnBind(this);
    }

    protected override void Start()
    {
     
    }
    public void OnBetReceiverInvoke(int count) => OnBetReceiver?.Invoke(count);
}
