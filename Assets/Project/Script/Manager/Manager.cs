using UnityEngine;

public static class Manager
{
    public static EventManager Event => _event;
    public static GameFlowManager GameFlow => _gameFlow;
    public static PointManager Point => _point;

    private static EventManager _event;
    private static GameFlowManager _gameFlow;
    private static PointManager _point;
    public static void SetEventManager(EventManager turn) => _event = turn;
    public static void SetGameFlowManager(GameFlowManager gameFlow) => _gameFlow = gameFlow;
    public static void SetPointManager(PointManager point) => _point = point;
}
