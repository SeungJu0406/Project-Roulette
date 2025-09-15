using UnityEngine;

public class RouletteController : MonoBehaviour
{
    [SerializeField] private int _maxVerticalNum = 3;
    [SerializeField] private int _maxHorizontalNum = 12;
    [SerializeField] private RouletteSlot[] slots;

    private void Awake()
    {
        CreateRoulette();
    }

    /// <summary>
    /// 逢房 弥檬 积己
    /// </summary>
    private void CreateRoulette()
    {
        slots = new RouletteSlot[36];
        for(int i = 0; i < 36; i++)
        {
            // 逞滚
            int number = i + 1;
            // 祸彬
            SlotColorType color;
            if (i % 2 == 0)
                color = SlotColorType.Red;
            else
                color = SlotColorType.Black;
            // 青
            int verticalNum= i % _maxVerticalNum;
            // 凯
            int HorizontalNum= i / _maxHorizontalNum;
            // 浇吩 积己
            slots[i] = new RouletteSlot(number, color, verticalNum, HorizontalNum);
        }
    }
}
