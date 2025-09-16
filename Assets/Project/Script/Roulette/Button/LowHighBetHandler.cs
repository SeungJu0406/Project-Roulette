using UnityEngine;
using UnityEngine.EventSystems;

public class LowHighBetHandler : RouletteBetController
{
    [SerializeField] private int _minValue = 1;
    [SerializeField] private int _maxValue = 18;
    public override void SetSlots(RouletteSlot[] allSlots)
    {
       for(int i = 0; i < allSlots.Length; i++)
       {
           if(allSlots[i].Number >= _minValue && allSlots[i].Number <= _maxValue)
           {
               _slots.Add(allSlots[i]);
           }
        }
    }
}
