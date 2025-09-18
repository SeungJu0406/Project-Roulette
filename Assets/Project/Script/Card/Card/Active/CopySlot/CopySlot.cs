
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
            // ù��° ����
            First = current;
            // ù��° ���Ը� ������ �ٽ� ���
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
                // ���� ���� ���ý� ����
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
