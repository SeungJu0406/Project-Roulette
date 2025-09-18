using UnityEngine;

[CreateAssetMenu(fileName = "LuckStarter", menuName = "Scriptable Objects/Card/Passive/LuckStarter")]
public class LuckStarterData : PassiveCardData
{
    public float ExtraMultiplier = 1.5f;
    protected override PassiveCard CreateCard()
    {
        return new LuckStarter(this);
    }
}
