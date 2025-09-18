using UnityEngine;

public abstract class ActiveCard :Card
{
    public bool CanMultipleChoice = false;

    public ActiveCard(ActiveCardData data)
    {

    }
    public abstract bool Apply();
}
