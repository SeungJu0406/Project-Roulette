using UnityEngine;

public abstract class PassiveCard :Card
{
    public virtual void OnSpin() { }
    public virtual void OnWin() { }
    public virtual void OnLose() { }
    protected abstract void Apply();
}
