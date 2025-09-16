using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Utility;

public class UIFocusManager : SingleTon<UIFocusManager>
{
    [SerializeField] private bool _isDebug = false;

    private GameObject _select;
    public GameObject Select
    {
        get => _select;
        set
        {
            if(_select == value)
                return;
            _select = value;
            OnSelectChanged?.Invoke(_select);
        }
    }

    public event UnityAction<GameObject> OnSelectChanged;
    protected override void InitAwake()
    {

    }


    private void LateUpdate()
    {
        GameObject select = EventSystem.current.currentSelectedGameObject;
        if (select == null)
        {
            EventSystem.current.SetSelectedGameObject(_select);
        }
        else
        {
            Select = select;
        }

        if (_isDebug)
        {
            Debug.Log($"Current Selected: {_select?.name}");
        }
    }

    public static void SetSelect(GameObject select)
    {
        if (Instance == null)
        {
            Debug.LogError("UIFocusManager instance is not initialized.");
            return;
        }
        EventSystem.current.SetSelectedGameObject(select);
    }

    public static void SetSelect<T>(T select) where T : Component
    {
        SetSelect(select.gameObject);
    }
}
