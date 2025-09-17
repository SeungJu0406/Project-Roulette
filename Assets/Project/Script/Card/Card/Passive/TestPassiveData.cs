using UnityEngine;

[CreateAssetMenu(fileName = "TestPassiveData", menuName = "Scriptable Objects/Card/Passive/TestPassiveData")]
public class TestPassiveData : PassiveCardData
{
    protected override PassiveCard CreateCard()
    {
        return new TestPassive();
    }
}
