using System;
using UnityEngine;
using UnityEngine.Events;
using Utility;

namespace NSJ_MVVM
{
    public class BaseCanvas : MonoBehaviour
    {
        [SerializeField] protected BasePanel[] _panels;
        public virtual int DefaultPanelIndex => 0; // �⺻ �г� �ε���

        public event UnityAction<int> OnPanelChanged;

        private int _curPanelIndex = -1;
        void Awake()
        {
            BindPanel();
            UIManager.SetCanvas(this); // UIManager�� ���� Canvas ����
        }

        /// <summary>
        /// �г��� �����մϴ�. Enum Ÿ���� ����Ͽ� �г��� ������ �� �ֽ��ϴ�.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam> 
        public void ChangePanel<TEnum>(TEnum panel) where TEnum : Enum
        {
            int panelIndex = Util.ToIndex(panel);
            ChangePanel(panelIndex);
        }
        public void ChangePanel(int index)
        {
            if(_curPanelIndex == index)
                return; // �̹� Ȱ��ȭ�� �г��̸� ����
            _curPanelIndex = index;
            for (int i = 0; i < _panels.Length; i++)
            {
                if (_panels[i].IsAllwaysActive == true)
                    continue; // �׻� Ȱ��ȭ�� �г��� ����
                _panels[i].gameObject.SetActive(i == index);
            }
            ChangePanelAfter(index);
            OnPanelChanged?.Invoke(index); // �г� ���� �̺�Ʈ ȣ��
        }

        public void ChangePanel(string name)
        {
            int index = 0;
            for (int i = 0; i < _panels.Length; i++)
            {
                if (_panels[i].IsAllwaysActive == true)
                    continue; // �׻� Ȱ��ȭ�� �г��� ����
                _panels[i].gameObject.SetActive(_panels[i].gameObject.name == name);
                if (_panels[i].gameObject.name == name)
                    index = i;
            }
            ChangePanelAfter(index);
            OnPanelChanged?.Invoke(index); // �г� ���� �̺�Ʈ ȣ��
        }

        protected virtual void ChangePanelAfter(int index) { }

        /// <summary>
        /// �г��� ���ε��մϴ�. �� �г��� Canvas �Ӽ��� �����Ͽ� ��ȣ�ۿ��� �� �ֵ��� �մϴ�.
        /// </summary>
        private void BindPanel()
        {
            _panels = GetComponentsInChildren<BasePanel>(true);
            foreach (BasePanel panel in _panels)
            {
                panel.Canvas = this;
            }
        }
    }
}