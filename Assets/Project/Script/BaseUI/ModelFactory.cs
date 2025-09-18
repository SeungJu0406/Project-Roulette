using UnityEngine;

namespace NSJ_MVVM
{
    public static class ModelFactory
    {
        /// <summary>
        /// 모델과 뷰모델을 생성하는 팩토리 메서드입니다.
        /// </summary>
        public static TModel InitializeModel<TModel>(TModel model)
            where TModel : BaseModel
        {
            // 모델을 초기화합니다.
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