using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseUI : MonoBehaviour
{
    private Dictionary<string, GameObject> gameObjectDic;
    private Dictionary<(string, System.Type), Component> componentDic;

    protected bool _isBind = false;
    void Awake()
    {
        Bind();
        InitGetUI();
        InitAwake();
    }
    protected abstract void InitGetUI();
    protected abstract void InitAwake();

    // ���� �ð��� ���ӿ�����Ʈ�� ���ε�
    protected void Bind()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>(true);
        gameObjectDic = new Dictionary<string, GameObject>(transforms.Length << 2);
        foreach (Transform child in transforms)
        {
            gameObjectDic.TryAdd(child.gameObject.name, child.gameObject);
        }

        componentDic = new Dictionary<(string, System.Type), Component>();

        _isBind = true;
    }

    // ���� ���� �ð��� ���ӿ�����Ʈ�� ��� ������Ʈ ���ε�
    protected void BindAll()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>(true);
        gameObjectDic = new Dictionary<string, GameObject>(transforms.Length << 2);

        foreach (Transform child in transforms)
        {
            gameObjectDic.TryAdd(child.gameObject.name, child.gameObject);
        }

        Component[] components = GetComponentsInChildren<Component>(true);
        componentDic = new Dictionary<(string, System.Type), Component>(components.Length << 4);
        foreach (Component child in components)
        {
            componentDic.TryAdd((child.gameObject.name, components.GetType()), child);
        }
        _isBind = true;
    }

    // �̸��� name�� UI ���ӿ�����Ʈ ��������
    // GetUI("Key") : Key �̸��� ���ӿ�����Ʈ ��������
    public GameObject GetUI(in string name)
    {
        gameObjectDic.TryGetValue(name, out GameObject gameObject);
        return gameObject;
    }

    // �̸��� name�� UI���� ������Ʈ T ��������
    // GetUI<Image>("Key") : Key �̸��� ���ӿ�����Ʈ���� Image ������Ʈ ��������
    public T GetUI<T>(in string name) where T : Component
    {
        (string, System.Type) key = (name, typeof(T));

        componentDic.TryGetValue(key, out Component component);
        if (component != null)
            return component as T;

        gameObjectDic.TryGetValue(name, out GameObject gameObject);
        if (gameObject == null)
            return null;

        component = gameObject.GetComponent<T>();
        if (component == null)
            return null;

        componentDic.TryAdd(key, component);
        return component as T;
    }
}