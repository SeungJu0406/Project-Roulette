using UnityEngine;

[System.Serializable]
public class RouletteCreateHandler
{
    [SerializeField] private RouletteSlot _slotPrefab;
    [SerializeField] private Transform _slotsParent;
    [SerializeField] private int _maxVerticalNum = 3;
    [SerializeField] private int _maxHorizontalNum = 12;

    private RouletteController _controller;
    private RouletteSlot[] _slots { get => _controller.Slots; set => _controller.Slots = value; }
    public void Initialize(RouletteController roulette)
    {
        _controller = roulette;
    }
    /// <summary>
    /// �귿 ���� ����
    /// </summary>
    public void CreateRoulette()
    {
        DeleteRoulette();

        _slots = new RouletteSlot[36];
        for (int i = 0; i < 36; i++)
        {
            _slots[i] = GameObject.Instantiate(_slotPrefab, _slotsParent);
            // �ѹ�
            int number = i + 1;
            // ����
            SlotColorType color;

            switch (i / 9)
            {
                case 0:
                    color = (number % 2 == 0) ? SlotColorType.Black : SlotColorType.Red;
                    break;
                case 1:
                    color = (number % 2 == 0) ? SlotColorType.Red : SlotColorType.Black;
                    if (number == 10)
                        color = SlotColorType.Black;
                    break;
                case 2:
                    color = (number % 2 == 0) ? SlotColorType.Black : SlotColorType.Red;
                    break;
                case 3:
                    color = (number % 2 == 0) ? SlotColorType.Red : SlotColorType.Black;
                    if (number == 28)
                        color = SlotColorType.Black;
                    break;
                default:
                    color = SlotColorType.Red;
                    break;
            }

            // ��
            int verticalNum = i % _maxVerticalNum;
            // ��
            int HorizontalNum = i / _maxHorizontalNum;
            // ���� ����
            _slots[i].Initialize(number, color, HorizontalNum, verticalNum);

            int Horizontal = i / _maxVerticalNum;
            // ��ġ ����
            float prefabSizeX = _slotPrefab.transform.localScale.x;
            float prefabSizeY = _slotPrefab.transform.localScale.y;

            float xOffset = -(prefabSizeX * _maxHorizontalNum / 2) + Horizontal * prefabSizeX;
            float yOffset = -(prefabSizeY * _maxVerticalNum / 2) + verticalNum * prefabSizeY;
            _slots[i].transform.localPosition = new Vector3(xOffset, yOffset, 0);

            // ���� �Է� ��Ʈ�ѷ� ����
            SlotBetHandler slotInputController = _slots[i].GetComponent<SlotBetHandler>();
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
                GameObject.Destroy(_slots[i].gameObject);
        }
    }
}
