using UnityEngine;
using UnityEngine.Events;

public class PointManager : MonoBehaviour
{
    public event UnityAction<RouletteSlot> OnSetSlotEvent;
    public event UnityAction<RouletteBetController> OnSetBetSpotEvent;

    private RouletteSlot _slot;
    private RouletteBetController _betSpot;
    private void Awake()
    {
        Manager.SetPointManager(this);
    }
    private void OnDestroy()
    {
        Manager.SetPointManager(null);
    }

    public void SetBetSpot(RouletteBetController betSpot)
    {
        _betSpot = betSpot;
        OnSetBetSpotEvent?.Invoke(betSpot);
    }
    public RouletteBetController GetBetSpot()
    {
        return _betSpot;
    }
    public void SetSlot(RouletteSlot slot)
    {
        _slot = slot;
        OnSetSlotEvent?.Invoke(slot);
    }
    public RouletteSlot GetSlot()
    {
        return _slot;
    }

}
