using NSJ_MVVM;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ShopModel : BaseModel
{
    public List<PassiveCardData> PassiveCards = new List<PassiveCardData>();

    public event UnityAction<int> OnAddPassiveEvent;
    public event UnityAction<int> OnRemovePassiveEvent;

    public event UnityAction<int> OnChoicePassiveReceiver;


    protected override void Awake()
    {
        BindingSystem<PassiveChoiceViewModel>.Bind(this);
    }

    protected override void Destroy()
    {
       BindingSystem<PassiveChoiceViewModel>.UnBind(this);
    }

    protected override void Start()
    {
      
    }

    public void OnAddPassiveEventInvoke(int index)
    {
        OnAddPassiveEvent?.Invoke(index);
    }
    public void OnRemovePassiveEventInvoke(int index)
    {
        OnRemovePassiveEvent?.Invoke(index);
    }
    public void ReceiveOnChoicePassiveInvoke(int index)
    {
        OnChoicePassiveReceiver?.Invoke(index);
    }
}
