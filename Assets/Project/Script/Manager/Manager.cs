using UnityEngine;

public static class Manager
{
    public static EventManager Event => _event;
    public static GameFlowManager GameFlow => _gameFlow;
    public static SlotPointManager SlotPoint => _slotPoint;

    private static EventManager _event;
    private static GameFlowManager _gameFlow;
    private static SlotPointManager _slotPoint;
    public static void SetEventManager(EventManager turn) => _event = turn;
    public static void SetGameFlowManager(GameFlowManager gameFlow) => _gameFlow = gameFlow;
    public static void SetSlotPointManager(SlotPointManager slotPoint) => _slotPoint = slotPoint;
}
