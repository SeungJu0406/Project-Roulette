using UnityEngine;

public class NumberChangeSupply : PassiveCard
{
    NumberChangeSupplyData _data;
    public NumberChangeSupply(NumberChangeSupplyData data) : base(data)
    {
        _data = data;
    }

    public override void OnWin()
    {
        Apply();
    }

    protected override void ProcessApply()
    {
        _cardController.AddActiveCard(_data.NumberChanger);
    }
}
