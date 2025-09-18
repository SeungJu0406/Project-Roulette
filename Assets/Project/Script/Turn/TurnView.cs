using NSJ_MVVM;
using TMPro;
using Utility;

public class TurnView : BaseView<TurnViewModel>
{
    private TMP_Text _currentTurn;
    private TMP_Text _maxTurn;
    protected override void ClearView()
    {

    }

    protected override void InitAwake()
    {

    }

    protected override void InitGetUI()
    {
        _currentTurn = GetUI<TMP_Text>("CurrentTurn");
        _maxTurn = GetUI<TMP_Text>("MaxTurn");
    }

    protected override void InitStart()
    {

    }

    protected override void OnViewModelSet()
    {
        Model.CurrentTurn.Bind(UpdateCurrentTurn);
        Model.MaxTurn.Bind(UpdateMaxTurn);
    }

    protected override void SubscribeEvents()
    {

    }

    private void UpdateCurrentTurn(int turn)
    {
        _currentTurn.text = $"{turn}";
    }
    private void UpdateMaxTurn(int turn)
    {
        _maxTurn.text = $"{turn}";
    }
}

public class TurnViewModel : BaseViewModel<TurnModel>
{
    public Bindable<int> CurrentTurn = new Bindable<int>();
    public Bindable<int> MaxTurn = new Bindable<int>();
    protected override void OnModelRemove()
    {
        Model.OnCurrentTurnChanged -= UpdateCurrentTurn;
        Model.OnMaxTurnChanged -= UpdateMaxTurn;

    }

    protected override void OnModelSet()
    {
        Model.OnCurrentTurnChanged += UpdateCurrentTurn;
        Model.OnMaxTurnChanged += UpdateMaxTurn;

        UpdateCurrentTurn(Model.CurrentTurn);
        UpdateMaxTurn(Model.MaxTurn);
    }
    private void UpdateCurrentTurn(int turn)
    {
        CurrentTurn.Value = turn;
    }
    private void UpdateMaxTurn(int turn)
    {
        MaxTurn.Value = turn;
    }
}
