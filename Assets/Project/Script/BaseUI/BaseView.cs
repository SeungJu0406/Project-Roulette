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
        /// 뷰가 Awake 단계에서 초기화되는 메서드입니다.
        /// </summary>
        protected abstract void InitAwake();

        /// <summary>
        /// 뷰가 Start 단계에서 초기화되는 메서드입니다.
        /// </summary>
        protected abstract void InitStart();
        /// <summary>
        /// 뷰가 이벤트를 구독하는 메서드입니다. 이 메서드는 뷰가 Start 단계에서 호출됩니다.
        /// </summary>
        protected abstract void SubscribeEvents();
    }

    public abstract class BaseView<TViewModel> : BaseView, IView<TViewModel>
        where TViewModel : BaseViewModel, IViewModel, new()
    {

        /// <summary>
        /// 뷰모델이 설정되었는지 여부를 나타냅니다.
        /// </summary>
        public bool HasViewModel { get; set; } = false;

        /// <summary>
        /// 뷰모델을 나타내는 필드입니다.
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
        // 어처피 뷰 있을떄 뷰모델 생성하면 자동 삽입되고
        // 뷰가 없어도 뷰모델이 잠깐 대기하다 뷰 생성됬을때 자동삽입되니 필요없을듯?
        //}

        /// <summary>
        /// 레지스트리에 본인을 등록합니다 
        /// 예시: BindingSystem.Resister(this);
        /// </summary>
        public void Register()
        {
            if (Model != null)
            {
                BindingSystem<TViewModel>.Resister(Model);
            }
        }

        /// <summary>
        /// 레지스트리에 본인을 해제합니다
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
        /// 명시적으로 강제 캐스팅
        /// </summary>
        /// <param name="viewModel"></param>
        public void OnSetViewModel(IViewModel viewModel)
        {
            OnSetViewModel((TViewModel)viewModel);
        }
        /// <summary>
        /// 뷰모델을 설정하였을때, 설정을 초기화합니다. 이미 설정된 경우에는 아무 작업도 하지 않습니다.
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
        /// 뷰모델이 설정되었을 때 호출되는 메서드입니다.
        /// </summary>
        protected abstract void OnViewModelSet();
        /// <summary>
        /// 뷰를 초기화합니다
        /// </summary>
        protected abstract void ClearView();
        /// <summary>
        /// 모델 파괴시에 이벤트로 호출됩니다
        /// </summary>
        private void DestroyModel()
        {
            //BindingSystem<BaseView<TViewModel>>.RemoveRebind(this);
            ClearView();
        }
    }
}
