using NSJ_MVVM;
using TMPro;

public class SlotInfoView : BaseView
{
    private TMP_Text _number;
    private TMP_Text _color;
    private TMP_Text _probability;

    protected override void InitAwake()
    {
        UpdateSlot(null);
    }

    protected override void InitGetUI()
    {
        _number = GetUI<TMP_Text>("Number");
        _color = GetUI<TMP_Text>("Color");
        _probability = GetUI<TMP_Text>("Probability");
    }

    protected override void InitStart()
    {
      
    }

    protected override void SubscribeEvents()
    {
        Manager.Point.OnSetSlotEvent += UpdateSlot;
    }

    private void UpdateSlot(RouletteSlot slot)
    {
        if(slot == null)
        {
            _number.text = "-";
            _color.text = "-";
            _probability.text = "-";
            return;
        }
        _number.text = slot.Number.ToString();
        _color.text = slot.Color == SlotColorType.Red? "Red" : "Black";
        _probability.text = $"{slot.Probability.ToString("F1")}%";
    }
}
