using UnityEngine;
using UnityEngine.EventSystems;

public class SlotBetHandler : RouletteBetController
{
    [SerializeField]private int _number;
    public override void SetSlots(RouletteSlot[] allSlots)
    {
        foreach(var slot in allSlots)
        {
            if(slot.Number == _number)
            {
                _slots.Add(slot);
            }
        }
    }
    public void SetNumber(int number) => _number = number;
}
