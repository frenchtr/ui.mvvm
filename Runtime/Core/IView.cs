namespace TravisRFrench.UI.MVVM.Core
{
    public interface IView<TViewModel>
        where TViewModel : class, IViewModel
    {
        TViewModel ViewModel { get; }
    }
}
