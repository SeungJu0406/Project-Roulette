using UnityEngine;

public class TestPassive : PassiveCard
{
    public override void OnSpin()
    {
        Apply();
    }

    protected override void Apply()
    {
        _roulette.AddBetMultiplier(2f);
    }
}
