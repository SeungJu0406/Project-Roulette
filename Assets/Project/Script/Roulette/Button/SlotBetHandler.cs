using UnityEngine;
using UnityEngine.EventSystems;

public class SlotBetHandler : RouletteBetController
{
    [SerializeField]private int _index;
    public override void SetSlots(RouletteSlot[] allSlots)
    {
        _slots.Add(allSlots[_index]);
    }
    public void SetIndex(int index) => _index = index;
}
