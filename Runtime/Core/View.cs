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
        
        [Inject]
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public TViewModel ViewModel { get; private set; }
        
        protected virtual void ConfigureManualBindings(IBindingRegistry registry)
        {
        }

        protected override void OnLifeCycleWireEndpoints()
        {
            this.BindAll();
        }

        protected override void OnLifeCycleDispose()
        {
            this.bindingRegistry.Dispose();
        }

        private void BindAll()
        {
            foreach (var binding in this.bindingRegistry.Bindings
                         .Where(b => !b.IsBound))
            {
                binding.Bind();
            }
        }

        private void UnbindAll()
        {
            foreach (var binding in this.bindingRegistry.Bindings
                         .Where(b => b.IsBound))
            {
                binding.Unbind();
            }
        }
    }
}
