using UnityEngine;
using UnityEngine.Events;
using Utility;

public class EventManager : MonoBehaviour
{

    public event UnityAction OnTurnStartEvent;
    public event UnityAction OnTurnEndEvent;
    public event UnityAction OnSpinEvent;
    public event UnityAction OnWinEvent;
    public event UnityAction OnLoseEvent;
    public event UnityAction OnRoundEndEvent;
    public event UnityAction OnRoundStartEvent;
    public event UnityAction OnShopStartEvent;
    public event UnityAction OnShopEndEvent;
    public event UnityAction OnCommandSpinEvent;

    private void Awake()
    {
        Manager.SetEventManager(this);
    }
    private void OnDestroy()
    {
        Manager.SetEventManager(null);
    }

    public void EndTurnInvoke()
    {
        OnTurnEndEvent?.Invoke();
    }
    public  void StartTurnInvoke()
    {
        OnTurnStartEvent?.Invoke();
    }
    public  void SpinInvoke()
    {
        OnSpinEvent?.Invoke();
    }
    public void WinInvoke()
    {
        OnWinEvent?.Invoke();
    }
    public void LoseInvoke()
    {
        OnLoseEvent?.Invoke();
    }
    public void ShopStartInvoke()
    {
        OnShopStartEvent?.Invoke();
    }
    public void ShopEndInvoke()
    {
        OnShopEndEvent?.Invoke();
    }
    public void RoundEndInvoke()
    {
        OnRoundEndEvent?.Invoke();
    }
    public void RoundStartInvoke()
    {
        OnRoundStartEvent?.Invoke();
    }
    public void CommandSpinInvoke()
    {
        OnCommandSpinEvent?.Invoke();
    }


}
