using UnityEngine;
using UnityEngine.Events;

public class PointManager : MonoBehaviour
{
    [SerializeField]private RouletteSlot _slot;
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
