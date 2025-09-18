
using UnityEngine;
using Utility;
using WeightUtility;

public class CardDatabase : SingleTon<CardDatabase>
{
    [SerializeField] private PassiveCardData[] _passives;
    [SerializeField] private ActiveCardData[] _actives;

    WeightTable<PassiveCardData> _passiveWeightTable;
    WeightTable<ActiveCardData> _activeWeightTable;
    protected override void InitAwake()
    {
        InitActiveWeightTabe();
        InitPassiveWeightTabe();
    }

    public static PassiveCardData GetRandomPassive()
    {
        PassiveCardData card = Instance._passiveWeightTable.Pick();
        Instance._passiveWeightTable.RemoveElement(card);
        return card;
    }
    public static void ReturnPassive(PassiveCardData data)
    {
        Instance._passiveWeightTable.AddElement(data, data.Weight);
    }

    public static ActiveCardData GetRandomActive()
    {      
        ActiveCardData card = Instance._activeWeightTable.Pick();
        return card;
    }
    

    private void InitPassiveWeightTabe()
    {
        _passiveWeightTable = new WeightTable<PassiveCardData>();
        foreach (var passive in _passives)
        {
            _passiveWeightTable.AddElement(passive, passive.Weight);
        }
    }
    private void InitActiveWeightTabe()
    {
        _activeWeightTable = new WeightTable<ActiveCardData>();
        foreach (var active in _actives)
        {
            _activeWeightTable.AddElement(active, active.Weight);
        }
    }
}
