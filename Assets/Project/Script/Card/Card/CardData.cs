using UnityEngine;

public abstract class CardData : ScriptableObject
{
    public float Weight = 1f;
    public string Name;
    [TextArea]
    public string Description;

}
