using UnityEngine;
using UnityEngine.Events;
using Utility;

public class TurnManager : SingleTon<TurnManager>
{

    public event UnityAction OnTurnStartEvent;
    public event UnityAction OnTurnEndEvent;
    public event UnityAction OnSpinEvent;
    protected override void InitAwake()
    {
      
    }


    public static void EndTurnInvoke()
    {
        Instance.OnTurnEndEvent?.Invoke();

        // TODO : 잠깐 바로 턴 시작
        StartTurnInvoke();
    }
    public static void StartTurnInvoke()
    {
        Instance.OnTurnStartEvent?.Invoke();
    }
    public static void SpinInvoke()
    {
        Instance.OnSpinEvent?.Invoke();
    }


}
