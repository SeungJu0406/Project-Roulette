using NSJ_MVVM;
using UnityEngine;
using UnityEngine.Events;

public class TurnController : MonoBehaviour
{
    [SerializeField] private TurnModel _model;

    public int CurrentTurn { get => _model.CurrentTurn; set => _model.CurrentTurn = value; }
    public int MaxTurn { get => _model.MaxTurn; set => _model.MaxTurn = value; }

    private void Awake()
    {
        _model.InitModel(this);
    }
}

[System.Serializable]
public class TurnModel : BaseModel
{
    public int CurrentTurn { get => _currentTurn; set { _currentTurn = value; OnCurrentTurnChanged?.Invoke(_currentTurn); } }
    public int MaxTurn { get => _maxTurn; set { _maxTurn = value; OnMaxTurnChanged?.Invoke(_maxTurn); } }

    public event UnityAction<int> OnCurrentTurnChanged;
    public event UnityAction<int> OnMaxTurnChanged;

    [SerializeField]private int _currentTurn;
    [SerializeField]private int _maxTurn;

    protected override void Awake()
    {
       BindingSystem<TurnViewModel>.Bind(this);
    }

    protected override void Destroy()
    {
       BindingSystem<TurnViewModel>.UnBind(this);
    }

    protected override void Start()
    {
        
    }
}