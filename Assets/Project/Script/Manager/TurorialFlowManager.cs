using UnityEngine;

public class TurorialFlowManager : GameFlowManager
{
    protected override void StartGame()
    {
        base.StartGame();
        Manager.Event.StartTurnInvoke();
    }
}
