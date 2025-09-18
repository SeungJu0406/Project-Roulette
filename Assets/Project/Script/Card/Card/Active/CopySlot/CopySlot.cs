
using TMPro.Examples;
using UnityEngine;

public class CopySlot : ActiveCard
{
   private RouletteSlot First { get => _first; 
        set 
        { 
            _first = value;  
            CanMultipleChoice = _first != null;
        } 
    }

    private RouletteSlot _first;
    private RouletteSlot _second;

    public CopySlot(ActiveCardData data) : base(data)
    {
    }


    public override bool Apply()
    {
        RouletteSlot current = Manager.Point.GetSlot();

        if (First == null)
        {
            // 첫번째 선택
            First = current;
            // 첫번째 슬롯만 선택후 다시 사용
            return false;
        }
        if (First != null && _second == null)
        {
            if(current == null)
            {
                First = null;
                return false;
            }

            if (First == current)
            {
                // 같은 슬롯 선택시 무시
                return false;
            }
            _second = current;
        }

        _second.SetNumber(First.Number);
        _second.SetColor(First.Color);
        _second.AddProbability(-_second.Probability);
        _second.AddProbability(First.Probability);
        return true;
    }
}
