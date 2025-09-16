using System;
using System.Collections.Generic;
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
        public void InitModel()
        {
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