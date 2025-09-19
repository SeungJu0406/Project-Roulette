using UnityEngine;
using UnityEngine.EventSystems;

public class OddEvenBetHandler : RouletteBetController
{
    enum OddEven
    {
        Odd,
        Even
    }
    [SerializeField] private OddEven _oddEven;

    
    public override void SetSlots(RouletteSlot[] allSlots)
    {
        if (_oddEven == OddEven.Odd)
        {
            for (int i = 0; i < allSlots.Length; i++)
            {
                if (allSlots[i].Number % 2 == 1)
                    _slots.Add(allSlots[i]);
            }
        }
        else
        {
            for (int i = 0; i < allSlots.Length; i++)
            {
                if (allSlots[i].Number % 2 == 0)
                    _slots.Add(allSlots[i]);
            }
        }
    }
}
