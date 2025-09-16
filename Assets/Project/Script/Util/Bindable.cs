using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class Bindable<T>
    {
        private T _value;

        /// <summary>
        /// Bindable<TStatus>�� ���Դϴ�. ���� ����� ������ OnValueChanged �̺�Ʈ�� ȣ��˴ϴ�.
        /// </summary>
        public T Value
        {
            get { return _value; }
            set
            {
                if (_isAnyWayEvent == false)
                {
                    if (EqualityComparer<T>.Default.Equals(_value, value) == true) // Default�� ���� �ش� Ÿ�� T�� �´� �⺻ �񱳱� ��ȯ                   
                        return;
                }
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }
        public Action<T> OnValueChanged;

        private bool _isAnyWayEvent = false;


        public static implicit operator T(Bindable<T> bindable)
        {
            return bindable.Value;
        }
        /// <summary>
        /// ���ε��� �ݹ��� ȣ���ϰ�, ���� ����� ������ �ݹ��� ȣ���մϴ�.
        /// </summary>
        /// <param name="callback"></param>
        public void Bind(Action<T> callback, bool IsInvoke = true)
        {
            OnValueChanged += callback;
            if(IsInvoke == true)
                callback?.Invoke(Value);
        }

        /// <summary>
        /// ���ε��� �ݹ��� ȣ���ϰ�, �ʱⰪ�� �����մϴ�. ���� ���� ����� ������ �ݹ��� ȣ���մϴ�.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="initialValue"></param>
        public void Bind(Action<T> callback, T initialValue)
        {
            Value = initialValue;
            Bind(callback);
        }

        public void UnBind(Action<T> callback)
        {
            OnValueChanged -= callback;
        }

        public Bindable(T initialValue = default, bool isAnyWayEvent = false)
        {
            _value = initialValue;
            _isAnyWayEvent = isAnyWayEvent;
        }
    }
}