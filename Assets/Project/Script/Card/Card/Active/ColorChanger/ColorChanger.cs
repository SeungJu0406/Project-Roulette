using UnityEngine;

public class ColorChanger : ActiveCard
{
    public ColorChanger(ActiveCardData data) : base(data)
    {
    }

    public override bool Apply()
    {
        RouletteSlot slot = Manager.SlotPoint.GetSlot();
        if (slot == null) return false;

        SlotColorType color = slot.Color;

        if (color == SlotColorType.Red)
        {
            slot.InitColor(SlotColorType.Black);
        }
        else
        {
            slot.InitColor(SlotColorType.Red);
        }
        return true;
    }
}
