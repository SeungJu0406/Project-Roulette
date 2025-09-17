using NSJ_MVVM;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class RouletteModel : BaseModel
{
    public RouletteSlot[] Slots;
    public float BetMultiplier { get => _betMultiplier; set { _betMultiplier = value; OnBetMultiplierChanged?.Invoke(BetMultiplier); } }

    public event UnityAction<float> OnBetMultiplierChanged;

    [SerializeField] private float _betMultiplier = 1f;

    protected override void Awake()
    {
        BindingSystem<BetMultiplierViewModel>.Bind(this);
    }

    protected override void Destroy()
    {
        BindingSystem<BetMultiplierViewModel>.UnBind(this);
    }

    protected override void Start()
    {

    }
}
