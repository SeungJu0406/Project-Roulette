using UnityEngine;

public static class Manager
{
    public static TurnManager Turn => _turn;
    public static GameFlowManager GameFlow => _gameFlow;

    private static TurnManager _turn;
    private static GameFlowManager _gameFlow;
    public static void SetTurnManager(TurnManager turn) => _turn = turn;
    public static void SetGameFlowManager(GameFlowManager gameFlow) => _gameFlow = gameFlow;
}
