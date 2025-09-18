using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace NSJ_MVVM
{
    [System.Serializable]
    public abstract class BaseModel : IModel
    {
        public event Action OnDestroyEvent;

        /// <summary>
        /// ���� �ʱ�ȭ�ϴ� �޼����Դϴ�.
        /// </summary>
        public void InitModel(MonoBehaviour behaviour)
        {
            BaseModelTracker tracker = behaviour.GetOrAddComponent<BaseModelTracker>();
            tracker.SetModel(this);
            Awake();
        }
        public void InitStart()
        {
            Start();
        }

        /// <summary>
        /// ��ü�� �ı��Ǿ����� ȣ��Ǵ� �޼����Դϴ�
        /// </summary>
        public void DestroyModel()
        {
            OnDestroyEvent?.Invoke();
            Destroy();
        }

        /// <summary>
        /// ���� �ʱ�ȭ�ϴ� �޼����Դϴ�. �� �޼���� ���� ������ �� ȣ��˴ϴ�.
        /// </summary>
        protected abstract void Awake();

        protected abstract void Start();
        protected abstract void Destroy();
    }
}