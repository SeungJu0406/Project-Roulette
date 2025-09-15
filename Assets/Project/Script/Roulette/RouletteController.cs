using UnityEngine;

public class RouletteController : MonoBehaviour
{
    public RouletteSlot[] Slots => _slots;

    [SerializeField] private int _maxVerticalNum = 3;
    [SerializeField] private int _maxHorizontalNum = 12;
    [SerializeField] private RouletteSlot[] _slots;
    [SerializeField] private RouletteSlot _slotPrefab;
    [SerializeField] private Transform _slotsParent;

    /// <summary>
    /// �귿 ���� ����
    /// </summary>
    [ContextMenu("CreateRoulette")]
    private void CreateRoulette()
    {
        DeleteRoulette();

        _slots = new RouletteSlot[36];
        for (int i = 0; i < 36; i++)
        {
            _slots[i] = Instantiate(_slotPrefab, _slotsParent);
            // �ѹ�
            int number = i + 1;
            // ����
            SlotColorType color;
            if (i % 2 == 0)
                color = SlotColorType.Red;
            else
                color = SlotColorType.Black;
            // ��
            int verticalNum = i % _maxVerticalNum;
            // ��
            int HorizontalNum = i / _maxHorizontalNum;
            // ���� ����
            _slots[i].Initialize(number, color, verticalNum, HorizontalNum);

            int Horizontal = i / _maxVerticalNum;
            // ��ġ ����
            float prefabSizeX = _slotPrefab.transform.localScale.x;
            float prefabSizeY = _slotPrefab.transform.localScale.y;

            float xOffset = -(prefabSizeX * _maxHorizontalNum / 2) + Horizontal * prefabSizeX;
            float yOffset = -(prefabSizeY * _maxVerticalNum / 2) + verticalNum * prefabSizeY;
            _slots[i].transform.localPosition = new Vector3(xOffset, yOffset, 0);

            // ���� �Է� ��Ʈ�ѷ� ����
            SlotInputHandler slotInputController = _slots[i].GetComponent<SlotInputHandler>();
            slotInputController.SetIndex(i);
        }
    }

    private void DeleteRoulette()
    {
        if (_slots == null)
            return;
        for (int i = _slots.Length - 1; i >= 0; i--)
        {
            if (_slots[i] != null)
                Destroy(_slots[i].gameObject);
        }
    }
}
