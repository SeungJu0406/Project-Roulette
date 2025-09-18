using UnityEngine;

public class ProbabilityChanger : ActiveCard
{
    ProbabilityChangerData _data;
    public ProbabilityChanger(ProbabilityChangerData data) : base(data)
    {
        _data = data;
    }

    public override bool Apply()
    {
        RouletteSlot slot = Manager.Point.GetSlot();
        if(slot == null) return false;

        slot.AddProbability(_data.ProbabilityChangeRate);
        return true;
    }
}
