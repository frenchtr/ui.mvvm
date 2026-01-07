using TravisRFrench.Dependencies.Injection;
using TravisRFrench.Lifecycles.Runtime;
using TravisRFrench.UI.MVVM.DataBinding.Registration;

namespace TravisRFrench.UI.MVVM.Core
{
    public abstract class View<TViewModel> : ManagedMonoBehaviour, IView<TViewModel>
        where TViewModel : class, IViewModel
    {
        private readonly IBindingRegistry bindingRegistry = new BindingRegistry();
        
        [Inject]
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public TViewModel ViewModel { get; }
        
        protected virtual void ConfigureManualBindings(IBindingRegistry registry)
        {
        }

        protected override void OnLifeCycleDispose()
        {
            this.bindingRegistry.Dispose();
        }
    }
}
