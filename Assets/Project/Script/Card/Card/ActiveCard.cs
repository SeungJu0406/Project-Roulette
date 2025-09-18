using UnityEngine;

public abstract class ActiveCard :Card
{
    public ActiveCard(ActiveCardData data)
    {

    }
    public abstract bool Apply();
}
