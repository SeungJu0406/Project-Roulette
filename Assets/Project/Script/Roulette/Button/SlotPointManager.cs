using UnityEngine;

public class SlotPointManager : MonoBehaviour
{
    [SerializeField]private RouletteSlot _slot;
    [SerializeField]private RouletteBetController _betSpot;

    private void Awake()
    {
        Manager.SetSlotPointManager(this);
    }
    private void OnDestroy()
    {
        Manager.SetSlotPointManager(null);
    }

    public void SetBetSpot(RouletteBetController betSpot)
    {
        _betSpot = betSpot;
    }
    public RouletteBetController GetBetSpot()
    {
        return _betSpot;
    }
    public void SetSlot(RouletteSlot slot)
    {
        _slot = slot;
    }
    public RouletteSlot GetSlot()
    {
        return _slot;
    }
}
