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
    /// 룰렛 최초 생성
    /// </summary>
    [ContextMenu("CreateRoulette")]
    private void CreateRoulette()
    {
        DeleteRoulette();

        _slots = new RouletteSlot[36];
        for (int i = 0; i < 36; i++)
        {
            _slots[i] = Instantiate(_slotPrefab, _slotsParent);
            // 넘버
            int number = i + 1;
            // 색깔
            SlotColorType color;
            if (i % 2 == 0)
                color = SlotColorType.Red;
            else
                color = SlotColorType.Black;
            // 행
            int verticalNum = i % _maxVerticalNum;
            // 열
            int HorizontalNum = i / _maxHorizontalNum;
            // 슬롯 생성
            _slots[i].Initialize(number, color, verticalNum, HorizontalNum);

            int Horizontal = i / _maxVerticalNum;
            // 위치 지정
            float prefabSizeX = _slotPrefab.transform.localScale.x;
            float prefabSizeY = _slotPrefab.transform.localScale.y;

            float xOffset = -(prefabSizeX * _maxHorizontalNum / 2) + Horizontal * prefabSizeX;
            float yOffset = -(prefabSizeY * _maxVerticalNum / 2) + verticalNum * prefabSizeY;
            _slots[i].transform.localPosition = new Vector3(xOffset, yOffset, 0);

            // 슬롯 입력 컨트롤러 설정
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
