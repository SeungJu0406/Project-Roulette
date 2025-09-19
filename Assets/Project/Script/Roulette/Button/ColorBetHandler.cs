using UnityEngine;

public class ColorBetHandler : RouletteBetController
{
    [SerializeField] private SlotColorType _color;
    public override void SetSlots(RouletteSlot[] allSlots)
    {
        foreach(var slot in allSlots)
        {
            if (slot.SlotColor == _color)
            {
                _slots.Add(slot);
            }
        }
    }
}
