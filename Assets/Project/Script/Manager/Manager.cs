using UnityEngine;

public static class Manager
{
    public static EventManager Event => _event;
    public static GameFlowManager GameFlow => _gameFlow;

    private static EventManager _event;
    private static GameFlowManager _gameFlow;
    public static void SetEventManager(EventManager turn) => _event = turn;
    public static void SetGameFlowManager(GameFlowManager gameFlow) => _gameFlow = gameFlow;
}
