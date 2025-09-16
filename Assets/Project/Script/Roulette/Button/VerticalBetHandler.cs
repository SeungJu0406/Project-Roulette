using UnityEngine;

public class VerticalBetHandler : RouletteBetController
{
    [Range(0,2)]
    [SerializeField] private int _verticalIndex = 1;
    public override void SetSlots(RouletteSlot[] allSlots)
    {
        foreach(var slot in allSlots)
        {
            if (slot.VerticalNum == _verticalIndex)
            {
                _slots.Add(slot);
            }
        }
    }
}
