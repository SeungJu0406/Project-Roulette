using UnityEngine;

[CreateAssetMenu(fileName = "AllInWrath", menuName = "Scriptable Objects/Card/Passive/AllInWrath")]
public class AllInWrathData : PassiveCardData
{
   public float ExtraBetMultiplier = 2f;
    protected override PassiveCard CreateCard()
    {
        return new AllInWrath(this);
    }
}
