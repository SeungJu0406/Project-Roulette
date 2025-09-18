using NSJ_MVVM;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CardControllerModel : BaseModel
{
    public List<ActiveCardStruct> ActiveCards { get => _activeCards; set { _activeCards = value; } }
    public List<PassiveCardStruct> PassiveCards { get => _passiveCards; set { _passiveCards = value; } }

    public UnityAction<int> OnPassiveCardsChanged;
    public UnityAction<int> OnActiveCardsChanged;
    public UnityAction<int> OnUseCardEventReciever;

    [SerializeField] private List<ActiveCardStruct> _activeCards;
    [SerializeField] private List<PassiveCardStruct> _passiveCards;
    protected override void Awake()
    {
        BindingSystem<PassiveViewModel>.Bind(this);
        BindingSystem<ActiveViewModel>.Bind(this);
    }

    protected override void Destroy()
    {
        BindingSystem<PassiveViewModel>.UnBind(this);
        BindingSystem<ActiveViewModel>.UnBind(this);
    }

    protected override void Start()
    {

    }

    public void OnPassiveCardChangedInvoke(int index) => OnPassiveCardsChanged?.Invoke(index);
    public void OnActiveCardChangedInvoke(int index) => OnActiveCardsChanged?.Invoke(index);

    public void ReceiveUseCardEvent(int index) => OnUseCardEventReciever?.Invoke(index);
}
