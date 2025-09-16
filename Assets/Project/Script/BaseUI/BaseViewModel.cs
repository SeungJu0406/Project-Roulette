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
        /// ����� �����ϴ� �޼����Դϴ�. �� �޼���� ����� ������ �� ȣ��˴ϴ�.
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
            // ����� �����Ǿ��� �� ȣ��Ǵ� �޼��带 �����մϴ�.
            OnModelSet();
        }

        /// <summary>
        /// ����� �����Ǿ��� �� ȣ��Ǵ� �޼����Դϴ�. �� �޼���� SetModel �޼��忡�� ȣ��˴ϴ�.
        /// </summary>
        protected abstract void OnModelSet();
        protected abstract void OnModelRemove();

        public void SetViewModel(IView view)
        {
        }

    }
}
