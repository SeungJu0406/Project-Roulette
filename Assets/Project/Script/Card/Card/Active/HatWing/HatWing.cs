using UnityEngine;

public class HatWing : ActiveCard
{
    HatWingData _data;
    public HatWing(HatWingData data) : base(data)
    {
        _data = data;
    }

    public override bool Apply()
    {
        Debug.Log(_roulette.IsAlwaysWin);
        if (_roulette.IsAlwaysWin == true)
            return false;

        _roulette.IsAlwaysWin = true;
        _roulette.AlwaysWinMultiplier = _data.BenefitValue;

        return true;
    }
}
