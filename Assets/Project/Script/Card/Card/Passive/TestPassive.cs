using UnityEngine;

public class TestPassive : PassiveCard
{
    public TestPassive(PassiveCardData data) : base(data)
    {
    }

    public override void OnSpin()
    {
        Apply();
    }

    protected override void Apply()
    {
        _roulette.AddBetMultiplier(2f);
    }
}
