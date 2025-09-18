using UnityEngine;

[CreateAssetMenu(fileName = "HatWing", menuName = "Scriptable Objects/Card/Active/HatWing")]
public class HatWingData : ActiveCardData
{
    public float BenefitValue = 0.5f;
    protected override ActiveCard CreateCard()
    {
      return new HatWing(this);
    }
}
