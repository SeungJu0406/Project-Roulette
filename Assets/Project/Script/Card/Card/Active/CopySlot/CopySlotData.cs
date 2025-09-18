using UnityEngine;

[CreateAssetMenu(fileName = "CopySlot", menuName = "Scriptable Objects/Card/Active/CopySlot")]
public class CopySlotData : ActiveCardData
{
    protected override ActiveCard CreateCard()
    {
        return new CopySlot(this);
    }
}
