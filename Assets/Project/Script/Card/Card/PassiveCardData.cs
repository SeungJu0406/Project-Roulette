using UnityEngine;

public abstract class PassiveCardData : CardData
{
    public PassiveCard GetCard()
    {
        return CreateCard();
    }
    protected abstract PassiveCard CreateCard();
}
