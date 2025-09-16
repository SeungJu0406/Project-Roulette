using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSJ_MVVM
{
    public interface IViewModel
    {
        bool EnableAutoBind { get; } // 자동 바인딩 허용 여부

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