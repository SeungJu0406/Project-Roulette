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
        /// 모델을 초기화하는 메서드입니다.
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
        /// 객체가 파괴되었을떄 호출되는 메서드입니다
        /// </summary>
        public void DestroyModel()
        {
            OnDestroyEvent?.Invoke();
            Destroy();
        }

        /// <summary>
        /// 모델을 초기화하는 메서드입니다. 이 메서드는 모델이 설정될 때 호출됩니다.
        /// </summary>
        protected abstract void Awake();

        protected abstract void Start();
        protected abstract void Destroy();
    }
}