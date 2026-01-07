namespace TravisRFrench.UI.MVVM.Core
{
    public interface IView<TViewModel>
        where TViewModel : class, IViewModel
    {
        public TViewModel ViewModel { get; }
    }
}
