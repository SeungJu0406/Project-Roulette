using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RouletteSlot : BaseUI
{
    public int Number;
    public SlotColorType Color;
    public int HorizontalNum;
    public int VerticalNum;

    private TMP_Text _numberText;
    private GameObject _black;
    private GameObject _red;



    protected override void InitGetUI()
    {
        _numberText = GetUI<TMP_Text>("NumberText");
        _black = GetUI("Black");
        _red = GetUI("Red");
    }
    protected override void InitAwake()
    {

    }


    public void Initialize(int number, SlotColorType color, int horizontalNumber, int verticalNumber)
    {
        if(!_isBind)
            Bind();
        InitGetUI();
        Number = number;
        Color = color;
        HorizontalNum = horizontalNumber;
        VerticalNum = verticalNumber;

        _black.SetActive(color == SlotColorType.Black);
        _red.SetActive(color == SlotColorType.Red);
        _numberText.text = number.ToString();
    }
}
