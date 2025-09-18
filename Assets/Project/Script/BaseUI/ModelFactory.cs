using UnityEngine;

namespace NSJ_MVVM
{
    public static class ModelFactory
    {
        /// <summary>
        /// �𵨰� ����� �����ϴ� ���丮 �޼����Դϴ�.
        /// </summary>
        public static TModel InitializeModel<TModel>(TModel model)
            where TModel : BaseModel
        {
            // ���� �ʱ�ȭ�մϴ�.
           // model.InitModel();
            return model;
        }

        public static TViewModel CreateViewModel<TViewModel>()
            where TViewModel : BaseViewModel, IViewModel, new()
        {
            TViewModel viewModel = new TViewModel();

            return viewModel;
        }

    }
}