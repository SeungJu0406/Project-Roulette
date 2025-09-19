using System.Collections.Generic;
using UnityEngine;

public class UIHoleRaycaseFilter : MonoBehaviour, ICanvasRaycastFilter
{
    [SerializeField] List<RectTransform> _holes = new List<RectTransform>();
    [SerializeField] Canvas _canvas; // 생략하면 GetComponentInParent로 찾아도 됨

    private void Awake()
    {
        if (_canvas == null)
            _canvas = GetComponentInParent<Canvas>();
        if (_canvas == null)
            Debug.LogError("UIHoleRaycaseFilter: Canvas not found in parent hierarchy.");
    }

    bool ICanvasRaycastFilter.IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        if (_holes == null || _holes.Count == 0)
            return true; // 구멍 없으면 평소대로 막기

        // 구멍 안쪽이면 "막지 않는다" => false 반환
        foreach (var hole in _holes)
        {
            if (hole == null) continue;
            if (RectTransformUtility.RectangleContainsScreenPoint(hole, sp, eventCamera))
                return false; // 통과!
        }
        return true; // 구멍 밖은 막기
    }
}
