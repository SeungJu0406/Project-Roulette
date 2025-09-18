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

        float newProbability = slot.Probability + _data.ProbabilityChangeRate;
        slot.SetProbability(newProbability);
        return true;
    }
}
