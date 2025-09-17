using UnityEngine;

public abstract class ActiveCardData : CardData
{
    public ActiveCard GetCard()
    {
        return CreateCard();
    }
    protected abstract ActiveCard CreateCard();
}
