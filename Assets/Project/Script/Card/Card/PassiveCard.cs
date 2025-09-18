using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class PassiveCard :Card
{
    public PassiveCard(PassiveCardData data) { }
    
    public event UnityAction OnApplyEvent;

    public virtual void OnSpin() { }
    public virtual void OnWin() { }
    public virtual void OnLose() { }
    public virtual void OnTurnStart() { }

    public virtual void OnTurnEnd() { }

    protected void Apply()
    {
        ProcessApply();
        OnApplyEvent?.Invoke();
    }
    protected abstract void ProcessApply();
}
