using UnityEngine;

[CreateAssetMenu(fileName = "NumberChanger", menuName = "Scriptable Objects/Card/Active/NumberChanger")]
public class NumberChangerData : ActiveCardData
{
    public int NumberChangeAmount;
    protected override ActiveCard CreateCard()
    {
        return new NumberChanger(this);
    }
}
