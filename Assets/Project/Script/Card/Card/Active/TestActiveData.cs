using UnityEngine;

[CreateAssetMenu(fileName = "TestActiveData", menuName = "Scriptable Objects/Card/Active/TestActiveData")]
public class TestActiveData : ActiveCardData
{

    protected override ActiveCard CreateCard()
    {
       return new TestActive(this);
    }
}
