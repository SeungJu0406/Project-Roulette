using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSJ_MVVM
{
    public interface IViewModel
    {
        bool EnableAutoBind { get; } // �ڵ� ���ε� ��� ����

        void OnSetModel(IModel model);
        void OnRemoveModel();
    }

    public interface IViewModel<T> : IViewModel
    {
        void OnSetModel(T model);
    }

    public interface IModel
    {

    }
}