using System;
using Unity.VisualScripting;
using UnityEngine;

namespace NSJ_MVVM
{
    public abstract class BaseView : BaseUI
    {
        [HideInInspector] public BaseGroup Group;
        [HideInInspector] public BasePanel Panel => Group.Panel;
        [HideInInspector] public BaseCanvas Canvas => Group.Panel.Canvas;
        public bool IsAllwaysActive;
        protected override void Awake()
        {
            base.Awake();
            InitGetUI();
            InitAwake();
            UIFocusManager.Instance.OnSelectChanged += OnSelectObject;
        }
        protected virtual void Start()
        {
            InitStart();
            SubscribeEvents();
        }

        protected virtual void OnDestroy()
        {
            UIFocusManager.Instance.OnSelectChanged -= OnSelectObject;
        }

        protected virtual void OnSelectObject(GameObject select) { }

        protected abstract void InitGetUI();
        /// <summary>
        /// �䰡 Awake �ܰ迡�� �ʱ�ȭ�Ǵ� �޼����Դϴ�.
        /// </summary>
        protected abstract void InitAwake();

        /// <summary>
        /// �䰡 Start �ܰ迡�� �ʱ�ȭ�Ǵ� �޼����Դϴ�.
        /// </summary>
        protected abstract void InitStart();
        /// <summary>
        /// �䰡 �̺�Ʈ�� �����ϴ� �޼����Դϴ�. �� �޼���� �䰡 Start �ܰ迡�� ȣ��˴ϴ�.
        /// </summary>
        protected abstract void SubscribeEvents();
    }

    public abstract class BaseView<TViewModel> : BaseView, IView<TViewModel>
        where TViewModel : BaseViewModel, IViewModel, new()
    {

        /// <summary>
        /// ����� �����Ǿ����� ���θ� ��Ÿ���ϴ�.
        /// </summary>
        public bool HasViewModel { get; set; } = false;

        /// <summary>
        /// ����� ��Ÿ���� �ʵ��Դϴ�.
        /// </summary>
        public TViewModel Model { get; set; }


        protected override void Awake()
        {
            Model = ModelFactory.CreateViewModel<TViewModel>();
            base.Awake();
            OnSetViewModel(Model);
            Register();
        }
        protected override void Start()
        {
            base.Start();
            if (Model == null)
                ClearView();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnResister();
        }

        //public void SetViewModel()
        //{
        // ��ó�� �� ������ ��� �����ϸ� �ڵ� ���Եǰ�
        // �䰡 ��� ����� ��� ����ϴ� �� ���������� �ڵ����ԵǴ� �ʿ������?
        //}

        /// <summary>
        /// ������Ʈ���� ������ ����մϴ� 
        /// ����: BindingSystem.Resister(this);
        /// </summary>
        public void Register()
        {
            if (Model != null)
            {
                BindingSystem<TViewModel>.Resister(Model);
            }
        }

        /// <summary>
        /// ������Ʈ���� ������ �����մϴ�
        /// BindingSystem<View>.UnResister(this);
        /// </summary>
        public void UnResister()
        {
            if (Model != null)
            {
                BindingSystem<TViewModel>.UnResister(Model);
            }
        }
        /// <summary>
        /// ��������� ���� ĳ����
        /// </summary>
        /// <param name="viewModel"></param>
        public void OnSetViewModel(IViewModel viewModel)
        {
            OnSetViewModel((TViewModel)viewModel);
        }
        /// <summary>
        /// ����� �����Ͽ�����, ������ �ʱ�ȭ�մϴ�. �̹� ������ ��쿡�� �ƹ� �۾��� ���� �ʽ��ϴ�.
        /// </summary>
        /// <param name="model"></param>
        public void OnSetViewModel(TViewModel model)
        {

            if (model == null) return;

            if (HasViewModel == true)
            {
                OnViewModelSet();
                return;
            }

            Model = model;
            HasViewModel = true;
            Model.OnDestroyEvent += DestroyModel;
            OnViewModelSet();
        }

        /// <summary>
        /// ����� �����Ǿ��� �� ȣ��Ǵ� �޼����Դϴ�.
        /// </summary>
        protected abstract void OnViewModelSet();
        /// <summary>
        /// �並 �ʱ�ȭ�մϴ�
        /// </summary>
        protected abstract void ClearView();
        /// <summary>
        /// �� �ı��ÿ� �̺�Ʈ�� ȣ��˴ϴ�
        /// </summary>
        private void DestroyModel()
        {
            //BindingSystem<BaseView<TViewModel>>.RemoveRebind(this);
            ClearView();
        }
    }
}
