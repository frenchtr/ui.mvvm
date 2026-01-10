using System.Linq;
using TravisRFrench.Dependencies.Injection;
using TravisRFrench.Lifecycles.Runtime;
using TravisRFrench.UI.MVVM.DataBinding.Registration;

namespace TravisRFrench.UI.MVVM.Core
{
    public abstract class View<TViewModel> : ManagedMonoBehaviour, IView<TViewModel>
        where TViewModel : class, IViewModel
    {
        private readonly IBindingRegistry bindingRegistry = new BindingRegistry();
        
        public TViewModel ViewModel { get; private set; }

        [Inject]
        private void Inject(TViewModel viewModel)
        {
            this.ViewModel = viewModel;
            this.ViewModel.Initialize();
        }
        
        protected virtual void ConfigureManualBindings(IBindingRegistry registry)
        {
        }
        
        protected override void OnLifecycleSetup()
        {
            this.ConfigureManualBindings(this.bindingRegistry);
            this.bindingRegistry.BindAll();
        }

        protected override void OnLifecycleDispose()
        {
            this.bindingRegistry.Dispose();
            this.ViewModel.Dispose();

            this.ViewModel = null;
        }
    }
}
