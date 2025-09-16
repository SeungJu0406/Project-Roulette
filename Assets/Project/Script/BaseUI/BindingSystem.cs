using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace NSJ_MVVM
{
    /// <summary>
    /// ��𵨰� ���� ���ε��ϴ� �ý����Դϴ�.
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
        /// ��𵨰� �並 ���ε��ϴ� ����Ʈ�Դϴ�.
        /// </summary>
        private List<BindingEntry> _bindings;

        /// <summary>
        /// ���� ���� �����ϱ� ���� ť�Դϴ�. ����� ���� �� ����� �����մϴ�.
        /// </summary>
        private List<IModel> _modelStroage;

        private bool _isPersistent = false;

        /// <summary>
        /// ��𵨰� �並 ���ε��ϴ� ������Ʈ���Դϴ�.
        /// </summary>
        /// <param name="view"></param>
        public static void Resister(IViewModel view)
        {
            if (Instance._bindings == null)
            {
                Instance._bindings = new();
            }

            // ��� �����ϴ� ���(�̱��� ��) �ٷ� ���� �����մϴ�.
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

            // ���� ��ID �丮��Ʈ�� ������ �߰��մϴ�
            AddViewToBinding(view);

            // ���� ���� ����Ʈ�� �ִ��� Ȯ���մϴ�.
            if (Instance._modelStroage == null)
                return;
            // ���� �̹� ���ε��Ǿ� �ִ��� Ȯ���մϴ�.
            if (Instance._modelStroage.Count <= 0)
                return; // ���� ���� ť�� ��������� �ƹ� �۾��� ���� �ʽ��ϴ�.

            int index = Instance._bindings.FindIndex(binding => binding.Views.Contains(view));
            IModel model = Instance._modelStroage[0];
            Instance._modelStroage.RemoveAt(0);
            view.OnSetModel(model);
            // ���ε� ������ ǥ���մϴ�
            Instance._bindings[index].Model = model;


        }

        /// <summary>
        /// ���� �����ÿ� ���ε��� �����ϴ� ������Ʈ���Դϴ�.
        /// </summary>
        /// <param name="view"></param>
        public static void UnResister(IViewModel view)
        {
            view.OnRemoveModel();
            // ��� ����
            RemoveViewFromBinding(view);
        }

        /// <summary>
        /// ����� �信 ���ε��մϴ�. ����� �̹� ���ε��Ǿ� ������ true�� ��ȯ�ϰ�, ���ο� �信 ���ε��Ǹ� true�� ��ȯ�մϴ�.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool TryBind(IModel model)
        {
            // ���� ���� ť�� ����ϴ�.
            if (Instance._modelStroage == null)
                Instance._modelStroage = new List<IModel>();
            if (Instance._bindings == null)
                Instance._bindings = new List<BindingEntry>();

            if (Instance._isPersistent == true)
            {
                if (Instance._bindings.Count <= 0)
                {
                    // ���ε��� ��������� ���� �����մϴ�.
                    Instance._bindings.Add(new BindingEntry(null, model));
                }
                else
                {
                    // �̹� ���ε��� ���, ���� �����մϴ�.
                    Instance._bindings[0].Model = model;
                    // ���� ���ε��� ����� ã�����Ƿ�, �ش� ��𵨿� ���� �����մϴ�.
                    SetViewModelToViews(Instance._bindings[0].Views, model);
                }
                return true;
            }
            // ���� �̹� ���� ���� ť�� �ִ��� Ȯ���մϴ�.
            else if (Instance._modelStroage.Contains(model) == false)
            {
                Instance._modelStroage.Add(model);
            }


            // ���ε��� ����ְų�, ��� ���ε� ��Ʈ���� ���� �ִ� ��� false�� ��ȯ�մϴ�.
            if (Instance._bindings == null || Instance._bindings.FirstOrDefault(v => v.Model == null) == null)
            {
                return false;
            }

            // �̹� ���� ���ε� �Ǿ� �ִ� ��� true�� ��ȯ�մϴ�.
            if (Instance._bindings.Any(x => x.Model == model))
                return true;

            List<IViewModel> targetViews = null;

            // ����� ViewID�� ������ ���� ���� ���, ���ε����� ���� �並 ã���ϴ�.
            targetViews = Instance._bindings.FirstOrDefault(v => v.Model == null).Views;

            if (targetViews == null) return false;

            // ���� ���ε��� ����� ã�����Ƿ�, �ش� ��𵨿� ���� �����մϴ�.
            SetViewModelToViews(targetViews, model);

            int index = Instance._bindings.FindIndex(x => x.Views == targetViews);
            // �̹� ���ε��� �䰡 �ִ� ���, �ش� �信 ����� �ٽ� �����մϴ�.
            Instance._bindings[index].Model = model;

            // ���� ���� ���� ť�� �ִ� ���, �ش� ���� �����մϴ�.
            if (Instance._modelStroage.Contains(model))
            {
                Instance._modelStroage.Remove(model);
            }

            return true;
        }
        /// <summary>
        /// �� ����ε�
        /// </summary>
        /// <param name="model"></param>
        public static void UnBind(IModel model)
        {
            if (Instance._bindings == null) return;

            BindingEntry entry = Instance._bindings.FirstOrDefault(x => x.Model == model);
            if (entry != null)
            {
                entry.Model = null;
                RemoveViewModelToViews(entry.Views); // ��鵵 ����ε�
            }
        }

        /// <summary>
        /// ����� �信 ����ε��մϴ�. ����� ���ε��Ǿ� ������ �ش� �信 �ٽ� ���ε��ϰ� true�� ��ȯ�մϴ�.
        /// �������� ����� ������ �� ���˴ϴ�.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool TryRebind(IModel model)
        {
            if (Instance._bindings == null)
                return false;

            // �̹� ���ε� ���
            int index = Instance._bindings.FindIndex(v => v.Model == model);
            if (index >= 0)
            {
                List<IViewModel> targetViews = Instance._bindings[index].Views;
                SetViewModelToViews(targetViews, model);
                return true;
            }

            // ����� ����� �ִ� ���
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
        /// ���ε����� ���� �並 �����մϴ�. ���ε��� �䰡 ����, �䰡 �ִ� ��쿡�� ȣ��˴ϴ�.
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

            // ���ε��� ���� ���� ��� ���� �����մϴ�.
            BindingEntry newEntry = new BindingEntry(targetView);
            Instance._bindings.Add(newEntry);

        }
        private static void RemoveViewFromBinding(IViewModel targetView)
        {
            // ��� ����
            foreach (BindingEntry entry in Instance._bindings)
            {
                if (entry.Views.Contains(targetView))
                {
                    entry.Views.Remove(targetView);
                    // ��𵨰� ���� ���°�� ���ε��� �����մϴ�.
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