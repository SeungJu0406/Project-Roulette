public class TestActive : ActiveCard
{
    TestActiveData _data;
    public TestActive(TestActiveData data) : base(data)
    {
        _data = data;
    }

    public override bool Apply()
    {
        // 그렇다면 슬롯 정보를 어떻게 얻지?
        // 기본적으로 마우스로 하는 게임
        // 마우스가 가리키는 슬롯 정보를 얻어야 함
        // 마우스가 가리키는 슬롯 정보를 알아내는 매니저 클래스가 필요할듯?

        RouletteSlot slot = Manager.SlotPoint.GetSlot();
        // slot 정보 획득
        if (slot == null)
            return false;

        // 슬롯 정보 조작
        int slotNumber = 36;
        slot.InitNumber(slotNumber);

        return true;
    }
}
