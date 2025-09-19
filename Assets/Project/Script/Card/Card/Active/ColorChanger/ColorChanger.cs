using UnityEngine;

public class ColorChanger : ActiveCard
{
    public ColorChanger(ActiveCardData data) : base(data)
    {
    }

    public override bool Apply()
    {
        RouletteSlot slot = Manager.Point.GetSlot();
        if (slot == null) return false;

        SlotColorType color = slot.SlotColor;

        if (color == SlotColorType.Red)
        {
            slot.SetColor(SlotColorType.Black);
        }
        else
        {
            slot.SetColor(SlotColorType.Red);
        }
        return true;
    }
}
