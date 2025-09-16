using System.Collections.Generic;
using UnityEngine;

namespace NSJ_MVVM
{
    public class BaseUI : MonoBehaviour
    {
        private Dictionary<string, GameObject> gameObjectDic;
        private Dictionary<(string, System.Type), Component> componentDic;

        protected bool _isBind = false;
        protected virtual void Awake()
        {
            Bind();
        }

        // ���� �ð��� ���ӿ�����Ʈ�� ���ε�
        protected void Bind()
        {
            if (gameObjectDic != null) return; // �̹� ���ε��� ��� ��õ� X

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
                componentDic.TryAdd((child.gameObject.name, child.GetType()), child);
            }

             _isBind = true;
        }

        // �̸��� name�� UI ���ӿ�����Ʈ ��������
        // GetUI("Key") : Key �̸��� ���ӿ�����Ʈ ��������
        public GameObject GetUI(in string name)
        {
            if (gameObjectDic == null)
                Bind(); // ����� ȣ��

            gameObjectDic.TryGetValue(name, out GameObject gameObject);
            return gameObject;
        }

        // �̸��� name�� UI���� ������Ʈ TStatus ��������
        // GetUI<Image>("Key") : Key �̸��� ���ӿ�����Ʈ���� Image ������Ʈ ��������
        public T GetUI<T>(in string name) where T : Component
        {
            if (gameObjectDic == null || componentDic == null)
                Bind(); // ����� ȣ��

            (string, System.Type) key = (name, typeof(T));

            if (componentDic.TryGetValue(key, out Component component))
            {
                return component as T;
            }

            if (!gameObjectDic.TryGetValue(name, out GameObject gameObject))
            {
                return null;
            }

            component = gameObject.GetComponent<T>();
            if (component == null)
            {
                return null;
            }

            componentDic.TryAdd(key, component);
            return component as T;
        }
    }
}