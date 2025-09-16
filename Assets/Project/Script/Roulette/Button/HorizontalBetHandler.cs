using UnityEngine;

public class HorizontalBetHandler : RouletteBetController
{
    [Range(0,2)]
    [SerializeField] private int _horizontalIndex = 1;
    public override void SetSlots(RouletteSlot[] allSlots)
    {
        foreach(var slot in allSlots)
        {
            if (slot.HorizontalNum == _horizontalIndex)
            {
                _slots.Add(slot);
            }
        }
    }
}
