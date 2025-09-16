using System;

namespace NSJ_MVVM
{
    public abstract class BaseViewModel : IViewModel
    {
        public bool EnableAutoBind => true;
        public bool HasViewID { get; set; }
        public int ViewID { get; set; }


        public event Action OnDestroyEvent;


        public virtual void OnSetModel(IModel model)
        {

        }
        public virtual void OnRemoveModel()
        {

        }



        protected void DestroyViewModel()
        {

            OnDestroyEvent?.Invoke();
        }
    }

    public abstract class BaseViewModel<TModel> : BaseViewModel, IViewModel<TModel>
        where TModel : BaseModel
    {
        protected TModel Model { get; set; }


        /// <summary>
        /// 뷰모델을 설정하는 메서드입니다. 이 메서드는 뷰모델이 설정될 때 호출됩니다.
        /// </summary>

        public override void OnSetModel(IModel model)
        {

            OnSetModel((TModel)model);
        }

        public override void OnRemoveModel()
        {
            if (Model == null)
                return;
            Model.OnDestroyEvent -= DestroyViewModel;
            OnModelRemove();
            Model = null;
        }
        public void OnSetModel(TModel model)
        {
            if (model == null)
                return;

            Model = model;
            Model.OnDestroyEvent += DestroyViewModel;
            // 뷰모델이 설정되었을 때 호출되는 메서드를 실행합니다.
            OnModelSet();
        }

        /// <summary>
        /// 뷰모델이 설정되었을 때 호출되는 메서드입니다. 이 메서드는 SetModel 메서드에서 호출됩니다.
        /// </summary>
        protected abstract void OnModelSet();
        protected abstract void OnModelRemove();

        public void SetViewModel(IView view)
        {
        }

    }
}
