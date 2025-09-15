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
    /// �귿 ���� ����
    /// </summary>
    private void CreateRoulette()
    {
        slots = new RouletteSlot[36];
        for(int i = 0; i < 36; i++)
        {
            // �ѹ�
            int number = i + 1;
            // ����
            SlotColorType color;
            if (i % 2 == 0)
                color = SlotColorType.Red;
            else
                color = SlotColorType.Black;
            // ��
            int verticalNum= i % _maxVerticalNum;
            // ��
            int HorizontalNum= i / _maxHorizontalNum;
            // ���� ����
            slots[i] = new RouletteSlot(number, color, verticalNum, HorizontalNum);
        }
    }
}
