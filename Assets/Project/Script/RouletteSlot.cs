using UnityEngine;

[System.Serializable]
public class RouletteSlot
{
    public int Number;
    public SlotColorType Color;
    public int VerticalNum;
    public int HorizontalNum;

    public RouletteSlot(int number, SlotColorType color, int verticalNum, int horizontalNum)
    {
        Number = number;
        Color = color;
        VerticalNum = verticalNum;
        HorizontalNum = horizontalNum;
    }
}
