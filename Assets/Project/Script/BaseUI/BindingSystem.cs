using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace NSJ_MVVM
{
    /// <summary>
    /// 뷰모델과 모델을 바인딩하는 시스템입니다.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public class BindingSystem<TViewModel> where TViewModel : IViewModel
    {
        private static BindingSystem<TViewModel> _instance;
        public static BindingSystem<TViewModel> Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BindingSystem<TViewModel>();
                    SceneManager.sceneUnloaded += ClearUnUsed;
                }
                return _instance;
            }
        }

        class BindingEntry
        {
            public List<IViewModel> Views = new List<IViewModel>();
            public IModel Model;
            public BindingEntry(IViewModel view = null, IModel model = null)
            {
                Views.Add(view);
                Model = model;
            }
        }
        /// <summary>
        /// 뷰모델과 뷰를 바인딩하는 리스트입니다.
        /// </summary>
        private List<BindingEntry> _bindings;

        /// <summary>
        /// 모델을 지연 저장하기 위한 큐입니다. 뷰모델이 없을 때 뷰모델을 저장합니다.
        /// </summary>
        private List<IModel> _modelStroage;

        private bool _isPersistent = false;

        /// <summary>
        /// 뷰모델과 뷰를 바인딩하는 레지스트리입니다.
        /// </summary>
        /// <param name="view"></param>
        public static void Resister(IViewModel view)
        {
            if (Instance._bindings == null)
            {
                Instance._bindings = new();
            }

            // 계속 존재하는 경우(싱글톤 등) 바로 모델을 설정합니다.
            if (Instance._isPersistent == true)
            {
                if (Instance._bindings.Count <= 0)
                {
                    Instance._bindings.Add(new BindingEntry(view));
                }
                else
                {
                    Instance._bindings[0].Views.Add(view);
                    view.OnSetModel(Instance._bindings[0].Model);
                }
                return;
            }

            // 동일 뷰ID 뷰리스트에 본인을 추가합니다
            AddViewToBinding(view);

            // 모델이 저장 리스트에 있는지 확인합니다.
            if (Instance._modelStroage == null)
                return;
            // 모델이 이미 바인딩되어 있는지 확인합니다.
            if (Instance._modelStroage.Count <= 0)
                return; // 지연 저장 큐가 비어있으면 아무 작업도 하지 않습니다.

            int index = Instance._bindings.FindIndex(binding => binding.Views.Contains(view));
            IModel model = Instance._modelStroage[0];
            Instance._modelStroage.RemoveAt(0);
            view.OnSetModel(model);
            // 바인딩 됬음을 표시합니다
            Instance._bindings[index].Model = model;


        }

        /// <summary>
        /// 뷰의 삭제시에 바인딩을 해제하는 레지스트리입니다.
        /// </summary>
        /// <param name="view"></param>
        public static void UnResister(IViewModel view)
        {
            view.OnRemoveModel();
            // 뷰모델 해제
            RemoveViewFromBinding(view);
        }

        /// <summary>
        /// 뷰모델을 뷰에 바인딩합니다. 뷰모델이 이미 바인딩되어 있으면 true를 반환하고, 새로운 뷰에 바인딩되면 true를 반환합니다.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool TryBind(IModel model)
        {
            // 지연 저장 큐를 만듭니다.
            if (Instance._modelStroage == null)
                Instance._modelStroage = new List<IModel>();
            if (Instance._bindings == null)
                Instance._bindings = new List<BindingEntry>();

            if (Instance._isPersistent == true)
            {
                if (Instance._bindings.Count <= 0)
                {
                    // 바인딩이 비어있으면 새로 생성합니다.
                    Instance._bindings.Add(new BindingEntry(null, model));
                }
                else
                {
                    // 이미 바인딩된 경우, 모델을 설정합니다.
                    Instance._bindings[0].Model = model;
                    // 모델이 바인딩된 뷰모델을 찾았으므로, 해당 뷰모델에 모델을 설정합니다.
                    SetViewModelToViews(Instance._bindings[0].Views, model);
                }
                return true;
            }
            // 모델이 이미 지연 저장 큐에 있는지 확인합니다.
            else if (Instance._modelStroage.Contains(model) == false)
            {
                Instance._modelStroage.Add(model);
            }


            // 바인딩이 비어있거나, 모든 바인딩 엔트리에 모델이 있는 경우 false를 반환합니다.
            if (Instance._bindings == null || Instance._bindings.FirstOrDefault(v => v.Model == null) == null)
            {
                return false;
            }

            // 이미 모델이 바인딩 되어 있는 경우 true를 반환합니다.
            if (Instance._bindings.Any(x => x.Model == model))
                return true;

            List<IViewModel> targetViews = null;

            // 뷰모델이 ViewID를 가지고 있지 않은 경우, 바인딩되지 않은 뷰를 찾습니다.
            targetViews = Instance._bindings.FirstOrDefault(v => v.Model == null).Views;

            if (targetViews == null) return false;

            // 모델이 바인딩된 뷰모델을 찾았으므로, 해당 뷰모델에 모델을 설정합니다.
            SetViewModelToViews(targetViews, model);

            int index = Instance._bindings.FindIndex(x => x.Views == targetViews);
            // 이미 바인딩된 뷰가 있는 경우, 해당 뷰에 뷰모델을 다시 설정합니다.
            Instance._bindings[index].Model = model;

            // 모델이 지연 저장 큐에 있는 경우, 해당 모델을 제거합니다.
            if (Instance._modelStroage.Contains(model))
            {
                Instance._modelStroage.Remove(model);
            }

            return true;
        }
        /// <summary>
        /// 모델 언바인딩
        /// </summary>
        /// <param name="model"></param>
        public static void UnBind(IModel model)
        {
            if (Instance._bindings == null) return;

            BindingEntry entry = Instance._bindings.FirstOrDefault(x => x.Model == model);
            if (entry != null)
            {
                entry.Model = null;
                RemoveViewModelToViews(entry.Views); // 뷰들도 언바인딩
            }
        }

        /// <summary>
        /// 뷰모델을 뷰에 재바인딩합니다. 뷰모델이 바인딩되어 있으면 해당 뷰에 다시 바인딩하고 true를 반환합니다.
        /// 동적으로 뷰모델을 변경할 때 사용됩니다.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool TryRebind(IModel model)
        {
            if (Instance._bindings == null)
                return false;

            // 이미 매핑된 경우
            int index = Instance._bindings.FindIndex(v => v.Model == model);
            if (index >= 0)
            {
                List<IViewModel> targetViews = Instance._bindings[index].Views;
                SetViewModelToViews(targetViews, model);
                return true;
            }

            // 뷰모델이 빈곳이 있는 경우
            index = Instance._bindings.FindIndex(v => v.Model == null);
            if (index >= 0)
            {
                List<IViewModel> targetViews = Instance._bindings[index].Views;
                SetViewModelToViews(targetViews, model);
                Instance._bindings[index].Model = model;
                return true;
            }

            return false;
        }


        /// <summary>
        /// 바인딩되지 않은 뷰를 제거합니다. 바인딩된 뷰가 없고, 뷰가 있는 경우에만 호출됩니다.
        /// </summary>
        public static void ClearUnUsed(Scene scene)
        {
            if (Instance._isPersistent == true)
                return;

            if (Instance._bindings != null) Instance._bindings.Clear();

            if (Instance._modelStroage != null) Instance._modelStroage.Clear();

        }

        public static void SetPersistent(bool isPersistent) => Instance._isPersistent = isPersistent;
        private static void AddViewToBinding(IViewModel targetView)
        {


            foreach (BindingEntry entry in Instance._bindings)
            {
                if (entry.Views.FirstOrDefault(x => x.GetType() == targetView.GetType()) == null)
                {
                    entry.Views.Add(targetView);
                    return;
                }
            }

            // 바인딩할 모델이 없는 경우 새로 생성합니다.
            BindingEntry newEntry = new BindingEntry(targetView);
            Instance._bindings.Add(newEntry);

        }
        private static void RemoveViewFromBinding(IViewModel targetView)
        {
            // 뷰모델 해제
            foreach (BindingEntry entry in Instance._bindings)
            {
                if (entry.Views.Contains(targetView))
                {
                    entry.Views.Remove(targetView);
                    // 뷰모델과 모델이 없는경우 바인딩을 제거합니다.
                    if (entry.Views.Count <= 0 && entry.Model == null)
                    {
                        Instance._bindings.Remove(entry);
                    }
                    return;
                }
            }
        }

        private static void SetViewModelToViews(List<IViewModel> views, IModel Model)
        {

            int count = views.Count;
            for (int i = 0; i < count; i++)
            {
                if (views[i] == null)
                    continue;
                views[i].OnSetModel(Model);
            }
        }
        private static void RemoveViewModelToViews(List<IViewModel> views)
        {
            int count = views.Count;
            for (int i = 0; i < count; i++)
            {
                if (views[i] == null)
                    continue;
                views[i].OnRemoveModel();
            }
        }
    }
}