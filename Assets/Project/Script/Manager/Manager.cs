using UnityEngine;

public static class Manager
{
    public static EventManager Event => _event;
    public static PointManager Point => _point;

    private static EventManager _event;
    private static PointManager _point;
    public static void SetEventManager(EventManager turn) => _event = turn;
    public static void SetPointManager(PointManager point) => _point = point;
}
