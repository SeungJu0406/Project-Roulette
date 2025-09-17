using System;
using UnityEngine;

public abstract class PassiveCard :Card
{
    public PassiveCard(PassiveCardData data) { }
    public virtual void OnSpin() { }
    public virtual void OnWin() { }
    public virtual void OnLose() { }
    public virtual void OnTurnStart() { }

    public virtual void OnTurnEnd() { }
    protected abstract void Apply();
}
