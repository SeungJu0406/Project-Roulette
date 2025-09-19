using System.Collections.Generic;
using UnityEngine;

public class UIHoleRaycaseFilter : MonoBehaviour, ICanvasRaycastFilter
{
    [SerializeField] List<RectTransform> _holes = new List<RectTransform>();
    [SerializeField] Canvas _canvas; // �����ϸ� GetComponentInParent�� ã�Ƶ� ��

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
            return true; // ���� ������ ��Ҵ�� ����

        // ���� �����̸� "���� �ʴ´�" => false ��ȯ
        foreach (var hole in _holes)
        {
            if (hole == null) continue;
            if (RectTransformUtility.RectangleContainsScreenPoint(hole, sp, eventCamera))
                return false; // ���!
        }
        return true; // ���� ���� ����
    }
}
