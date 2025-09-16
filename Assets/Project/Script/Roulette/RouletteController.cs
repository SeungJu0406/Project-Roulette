using System.Collections.Generic;
using UnityEngine;

public class RouletteController : MonoBehaviour
{
    public RouletteSlot[] Slots => _slots;

    [SerializeField] private RouletteSlot _slotPrefab;
    [SerializeField] private Transform _slotsParent;
    [SerializeField] private int _maxVerticalNum = 3;
    [SerializeField] private int _maxHorizontalNum = 12;
    [SerializeField] private RouletteSlot[] _slots;
    [SerializeField] private List<RouletteSlot> _betSlots;

    // TODO : 테스트용
    [SerializeField] private int _randomIndex;  

    private RouletteBetController[] _betHandlers;
    private void Awake()
    {
        _betHandlers = GetComponentsInChildren<RouletteBetController>(true);
    }

    private void Start()
    {
        InitBetHandler();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Spin();
            CheckBetResult();
        }
    }
    public void SetBetSlots(List<RouletteSlot> betSlots)
    {
        _betSlots = betSlots;
    }

    public void Spin()
    {
        _randomIndex = Random.Range(0, _slots.Length);
    }
    private void CheckBetResult()
    {
        foreach (var slot in _betSlots)
        {
            if(slot == _slots[_randomIndex])
            {
                Debug.Log("Win! Number: " + slot.Number);
                return;
            }
        }
        Debug.Log("Lose! Winning Number: " + _slots[_randomIndex].Number);
    }

    private void InitBetHandler()
    {
        foreach (var handler in _betHandlers)
        {
            handler.SetRouletteController(this);
            handler.SetSlots(_slots);
        }
    }

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

            switch (i / 9)
            {
                case 0:
                    color = (number % 2 == 0) ? SlotColorType.Black : SlotColorType.Red;
                    break;
                case 1:
                    color = (number % 2 == 0) ? SlotColorType.Red : SlotColorType.Black;
                    if(number == 10)
                        color = SlotColorType.Black;
                    break;
                case 2:
                    color = (number % 2 == 0) ? SlotColorType.Black : SlotColorType.Red;
                    break;
                case 3:
                    color = (number % 2 == 0) ? SlotColorType.Red : SlotColorType.Black;
                    if(number == 28)
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
                Destroy(_slots[i].gameObject);
        }
    }
}
