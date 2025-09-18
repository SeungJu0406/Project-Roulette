using UnityEngine;

[CreateAssetMenu(fileName = "ColorChanger", menuName = "Scriptable Objects/Card/Active/ColorChanger")]
public class ColorChangerData : ActiveCardData
{
    protected override ActiveCard CreateCard()
    {
        return new ColorChanger(this);
    }
}
