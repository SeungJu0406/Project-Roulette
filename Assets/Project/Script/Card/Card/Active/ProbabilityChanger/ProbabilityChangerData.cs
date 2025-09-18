using UnityEngine;

[CreateAssetMenu(fileName = "ProbabilityChanger", menuName = "Scriptable Objects/Card/Active/ProbabilityChanger")]
public class ProbabilityChangerData : ActiveCardData
{
    public float ProbabilityChangeRate = 20f;
    protected override ActiveCard CreateCard()
    {
        return new ProbabilityChanger(this);
    }
}
