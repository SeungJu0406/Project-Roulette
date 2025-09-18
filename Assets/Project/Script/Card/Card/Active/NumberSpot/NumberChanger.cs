using UnityEngine;

public class NumberChanger : ActiveCard
{
    NumberChangerData _data;
    public NumberChanger(NumberChangerData data) : base(data)
    {
        _data = data;
    }

    public override bool Apply()
    {
        RouletteSlot slot = Manager.SlotPoint.GetSlot();
        // slot ¡§∫∏ »πµÊ
        if (slot == null)
            return false;

        // ΩΩ∑‘ ¡§∫∏ ¡∂¿€
        int slotNumber = slot.Number + _data.NumberChangeAmount;

        if (slotNumber < 0 || slotNumber > 36)
            return false;

        slot.InitNumber(slotNumber);
        return true;

    }
}
