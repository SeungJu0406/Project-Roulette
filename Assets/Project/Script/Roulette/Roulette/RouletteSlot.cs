using NSJ_MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RouletteSlot : BaseUI
{
    public float Probability { get => _probability; set { _probability = value; } }
    public int Number;
    public SlotColorType Color;
    public int HorizontalNum;
    public int VerticalNum;

    public event UnityAction<RouletteSlot, float> OnProbabilityChanged;

    public event UnityAction OnSlotInfoChanged;

    [SerializeField] private float _probability;

    private TMP_Text _numberText;
    private GameObject _black;
    private GameObject _red;
    private GameObject _outline;

    private SlotBetHandler _betHandler;

    protected override void Awake()
    {
        base.Awake();
        InitGetUI();
        InitAwake();
    }
    private void InitAwake()
    {
        _betHandler = GetComponent<SlotBetHandler>();
        SetOutline(false);
    }

    private void Start()
    {
        Manager.Event.OnTurnEndEvent += () => SetOutline(false);
    }



    private void InitGetUI()
    {
        _numberText = GetUI<TMP_Text>("NumberText");
        _black = GetUI("Black");
        _red = GetUI("Red");
        _outline = GetUI("Outline");
    }

    public void Initialize(int number, SlotColorType color, int horizontalNumber, int verticalNumber)
    {
        if (!_isBind)
            Bind();
        InitGetUI();
        SetNumber(number);
        SetColor(color);
        SetHorizontal(horizontalNumber);
        SetVertical(verticalNumber);
    }

    public void SetNumber(int number)
    {
        Number = number;
        _betHandler?.SetNumber(number);
        _numberText.text = number.ToString();
        OnSlotInfoChanged?.Invoke();
    }
    public void SetColor(SlotColorType color)
    {
        Color = color;
        _black.SetActive(color == SlotColorType.Black);
        _red.SetActive(color == SlotColorType.Red);
        OnSlotInfoChanged?.Invoke();
    }
    public void SetHorizontal(int horizontalNumber)
    {
        HorizontalNum = horizontalNumber;
        OnSlotInfoChanged?.Invoke();
    }
    public void SetVertical(int verticalNumber)
    {
        VerticalNum = verticalNumber;
        OnSlotInfoChanged?.Invoke();
    }

    public void AddProbability(float probability)
    {
        Probability += probability;

        float changeValue = probability;
        if (Probability > 100)
        {
            changeValue = 100 - (Probability - probability);
            Probability = 100;
        }
        

        OnProbabilityChanged?.Invoke(this, changeValue);
    }

    public void RevouleSlot()
    {
        SetOutline(true);
    }

    public void SetOutline(bool isActive)
    {
        _outline.SetActive(isActive);
    }

    protected override void OnPointEnter(PointerEventData eventData)
    {
        Manager.Point.SetSlot(this);
    }
    protected override void OnPointExit(PointerEventData eventData)
    {
        Manager.Point.SetSlot(null);
    }
}
