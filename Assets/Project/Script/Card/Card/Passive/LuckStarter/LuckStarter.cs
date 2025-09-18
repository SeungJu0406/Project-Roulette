using UnityEngine;

public class LuckStarter : PassiveCard
{
    LuckStarterData _data;
    TurnController _turnController;

    public LuckStarter(LuckStarterData data) : base(data)
    {
        _data = data;
        _turnController = GameObject.FindAnyObjectByType<TurnController>();
    }

    public override void OnWin()
    {
        if (_turnController.CurrentTurn != 1)
            return;

        Apply();
    }

    protected override void ProcessApply()
    {
        _roulette.AddBetMultiplier(_data.ExtraMultiplier);
    }
}
