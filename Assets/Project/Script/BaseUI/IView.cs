namespace NSJ_MVVM
{
    public interface IView
    {

        bool HasViewModel { get; set; }

        void OnSetViewModel(IViewModel viewModel);
    }

    public interface IView<T> : IView
    {
        T Model { get; set; }
        void OnSetViewModel(T viewModel); 
    }
}