using NSJ_MVVM;
using TMPro;
using UnityEngine;
using Utility;

public class BetMultiplierView : BaseView<BetMultiplierViewModel>
{
    private TMP_Text _multiplier;
    protected override void ClearView()
    {
        
    }

    protected override void InitAwake()
    {
       
    }

    protected override void InitGetUI()
    {
        _multiplier = GetUI<TMP_Text>("Multiplier");
    }

    protected override void InitStart()
    {

    }

    protected override void OnViewModelSet()
    {
        Model.BetMultiplier.Bind(OnBetMultiplierChanged);
    }

    protected override void SubscribeEvents()
    {

    }

    private void OnBetMultiplierChanged(float value)
    {
        _multiplier.text = $"{value}";
    }
}

public class BetMultiplierViewModel : BaseViewModel<RouletteModel>
{
    public Bindable<float> BetMultiplier = new Bindable<float>(1f);
    protected override void OnModelSet()
    {
        Model.OnBetMultiplierChanged += OnBetMultiplierChanged;
    }
    protected override void OnModelRemove()
    {
        Model.OnBetMultiplierChanged -= OnBetMultiplierChanged;
    }
    private void OnBetMultiplierChanged(float value)
    {
        BetMultiplier.Value = value;
    }
}
