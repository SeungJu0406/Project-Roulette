using UnityEngine;

[CreateAssetMenu(fileName = "NumberChangeSupply", menuName = "Scriptable Objects/Card/Passive/NumberChangeSupply")]
public class NumberChangeSupplyData : PassiveCardData
{
    public NumberChangerData NumberChanger;
    protected override PassiveCard CreateCard()
    {
       return new NumberChangeSupply(this);
    }
}
