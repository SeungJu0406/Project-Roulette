using NSJ_MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RouletteSlot : BaseUI
{
    public float Probability { get => _probability; set { _probability = value; OnProbabilityChanged?.Invoke(this); } }
    public int Number;
    public SlotColorType Color;
    public int HorizontalNum;
    public int VerticalNum;

    public event UnityAction<RouletteSlot> OnProbabilityChanged;

    [SerializeField] private float _probability =1 ;

    private TMP_Text _numberText;
    private GameObject _black;
    private GameObject _red;
    private GameObject _outline;



    protected override void Awake()
    {
        base.Awake();
        InitGetUI();
        InitAwake();
    }

    private void Start()
    {
        TurnManager.Instance.OnTurnEndEvent += () => SetOutline(false);
    }



    private void InitGetUI()
    {
        _numberText = GetUI<TMP_Text>("NumberText");
        _black = GetUI("Black");
        _red = GetUI("Red");
        _outline = GetUI("Outline");
    }
    private void InitAwake()
    {
       SetOutline(false);
    }

    public void Initialize(int number, SlotColorType color, int horizontalNumber, int verticalNumber)
    {
        if(!_isBind)
            Bind();
        InitGetUI();
        InitNumber(number);
        InitColor(color);
        InitHorizontal(horizontalNumber);
        InitVertical(verticalNumber);
    }

    public void InitNumber(int number)
    {
        Number = number;
        _numberText.text = number.ToString();
    }
    public void InitColor(SlotColorType color)
    {
        Color = color;
        _black.SetActive(color == SlotColorType.Black);
        _red.SetActive(color == SlotColorType.Red);
    }
    public void InitHorizontal(int horizontalNumber)
    {
        HorizontalNum = horizontalNumber;
    }
    public void InitVertical(int verticalNumber)
    {
        VerticalNum = verticalNumber;
    }

    public void RevouleSlot()
    {
        SetOutline(true);
    }

    public void SetOutline(bool isActive)
    {
        _outline.SetActive(isActive);
    }
}
