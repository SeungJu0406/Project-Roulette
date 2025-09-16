using NSJ_MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;


public class ChipView : BaseView<ChipViewModel>
{

    private Button _chipAddFirstButton;
    private Button _chipAddSecondButton;
    private Button _chipSubFirstButton;
    private Button _chipSubSecondButton;

    private TMP_Text _holdChipText;
    private TMP_Text _betChipText;
    protected override void ClearView()
    {

    }

    protected override void InitAwake()
    {

    }

    protected override void InitGetUI()
    {
        _chipAddFirstButton = GetUI<Button>("10Add");
        _chipAddSecondButton = GetUI<Button>("1Add");
        _chipSubFirstButton = GetUI<Button>("10Sub");
        _chipSubSecondButton = GetUI<Button>("1Sub");

        _holdChipText = GetUI<TMP_Text>("HoldChip");
        _betChipText = GetUI<TMP_Text>("BetChip");
    }

    protected override void InitStart()
    {

    }

    protected override void OnViewModelSet()
    {
        Model.HoldChip.Bind(UpdateHoldChip);
        Model.BetChip.Bind(UpdateBetChip);
    }

    protected override void SubscribeEvents()
    {
        _chipAddFirstButton.onClick.AddListener(() => Model.AddHoldChip(10));
        _chipAddSecondButton.onClick.AddListener(() => Model.AddHoldChip(1));
        _chipSubFirstButton.onClick.AddListener(() => Model.SubHoldChip(10));
        _chipSubSecondButton.onClick.AddListener(() => Model.SubHoldChip(1));
    }

    public void UpdateHoldChip(int count)
    {
        _holdChipText.text = $"{count}";
    }
    public void UpdateBetChip(int count)
    {
        _betChipText.text = $"{count}";
    }
}
public class ChipViewModel : BaseViewModel<ChipControllerModel>
{

    public Bindable<int> HoldChip = new Bindable<int>();
    public Bindable<int> BetChip = new Bindable<int>();

    public void AddHoldChip(int count)
    {
        Model.OnBetReceiverInvoke(count);
    }
    public void SubHoldChip(int count)
    {
        Model.OnBetReceiverInvoke(-count);
    }

    protected override void OnModelSet()
    {
        Model.OnHoldChipChanged += UpdateHoldChip;
        Model.OnBetChipChanged += UpdateBetChip;

        UpdateHoldChip(Model.HoldChip);
        UpdateBetChip(Model.BetChip);
    }
    protected override void OnModelRemove()
    {
        Model.OnHoldChipChanged -= UpdateHoldChip;
        Model.OnBetChipChanged -= UpdateBetChip;
    }

    private void UpdateHoldChip(int count) => HoldChip.Value = count;
    private void UpdateBetChip(int count) => BetChip.Value = count;

}
