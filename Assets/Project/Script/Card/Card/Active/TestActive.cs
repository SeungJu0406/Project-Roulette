public class TestActive : ActiveCard
{
    TestActiveData _data;
    public TestActive(TestActiveData data) : base(data)
    {
        _data = data;
    }

    public override bool Apply()
    {
        // �׷��ٸ� ���� ������ ��� ����?
        // �⺻������ ���콺�� �ϴ� ����
        // ���콺�� ����Ű�� ���� ������ ���� ��
        // ���콺�� ����Ű�� ���� ������ �˾Ƴ��� �Ŵ��� Ŭ������ �ʿ��ҵ�?

        RouletteSlot slot = Manager.SlotPoint.GetSlot();
        // slot ���� ȹ��
        if (slot == null)
            return false;

        // ���� ���� ����
        int slotNumber = 36;
        slot.InitNumber(slotNumber);

        return true;
    }
}
