using UnityEngine;

public class AllInWrath : PassiveCard
{
    AllInWrathData _data;
    public AllInWrath(AllInWrathData data) : base(data)
    {
        _data = data;
    }

    public override void OnSpin()
    {
        if (_chip.HoldChip > 0)
            return;

        Apply();
    }

    protected override void ProcessApply()
    {
        _roulette.AddBetMultiplier(_data.ExtraBetMultiplier);
    }
}
