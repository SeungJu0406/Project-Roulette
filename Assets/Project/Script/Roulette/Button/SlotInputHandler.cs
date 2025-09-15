using UnityEngine;
using UnityEngine.EventSystems;

public class SlotInputHandler : RouletteInputController
{
    [SerializeField]private int _index;
    protected override void SetSlots(RouletteSlot[] allSlots)
    {
        _slots.Add(allSlots[_index]);
    }
    public void SetIndex(int index) => _index = index;
}
