using UnityEngine;

[System.Serializable]
public class RouletteCreateHandler
{
    [SerializeField] private RouletteSlot _slotPrefab;
    [SerializeField] private int _maxVerticalNum = 3;
    [SerializeField] private int _maxHorizontalNum = 12;

    private RouletteController _controller;
    private RouletteSlot[] _slots { get => _controller.Slots; set => _controller.Slots = value; }
    public void Initialize(RouletteController roulette)
    {
        _controller = roulette;
    }
    /// <summary>
    /// 룰렛 최초 생성
    /// </summary>
    public void CreateRoulette()
    {
        DeleteRoulette();

        Transform slotsParent = new GameObject("Slots").transform;
        slotsParent.SetParent(_controller.transform);
        slotsParent.localPosition = Vector3.zero;
        slotsParent.localRotation = Quaternion.identity;
        slotsParent.localScale = Vector3.one;

        _slots = new RouletteSlot[36];
        for (int i = 0; i < 36; i++)
        {
            // 슬롯 생성
            _slots[i] = GameObject.Instantiate(_slotPrefab, slotsParent);
            // 넘버
            int number = i + 1;
            // 색깔
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

            // 행
            int verticalNum = i % _maxVerticalNum;
            // 열
            int HorizontalNum = i / _maxHorizontalNum;

            int Horizontal = i / _maxVerticalNum;
            // 위치 지정
            float prefabSizeX = _slotPrefab.transform.localScale.x;
            float prefabSizeY = _slotPrefab.transform.localScale.y;

            float xOffset = -(prefabSizeX * _maxHorizontalNum / 2) + Horizontal * prefabSizeX;
            float yOffset = -(prefabSizeY * _maxVerticalNum / 2) + verticalNum * prefabSizeY;
            _slots[i].transform.localPosition = new Vector3(xOffset, yOffset, 0);

            // 슬롯 설정
            _slots[i].Initialize(number, color, verticalNum, HorizontalNum);

            float probability = 100f / 36f;
            _slots[i].SetProbability(probability);

            // 슬롯 입력 컨트롤러 설정
            SlotBetHandler slotInputController = _slots[i].GetComponent<SlotBetHandler>();
            slotInputController.SetNumber(number);
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
