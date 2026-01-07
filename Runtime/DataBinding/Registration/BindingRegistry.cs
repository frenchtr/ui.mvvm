using System;
using System.Collections.Generic;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;

namespace TravisRFrench.UI.MVVM.DataBinding.Registration
{
    public class BindingRegistry : IBindingRegistry
    {
        private readonly List<IBinding> bindings;

        public BindingRegistry()
        {
            this.bindings = new List<IBinding>();
        }
        
        public void Register(IBinding binding)
        {
            if (this.bindings.Contains(binding))
            {
                throw new InvalidOperationException("The specified binding has already been registered.");
            }
            
            this.bindings.Add(binding);
        }
        public void Unregister(IBinding binding)
        {
            if (!this.bindings.Contains(binding))
            {
                throw new InvalidOperationException("The specified binding has not been registered.");
            }
            
            this.bindings.Remove(binding);
        }
        
        public void Dispose()
        {
            foreach (var binding in this.bindings)
            {
                binding.Dispose();
            }
        }
    }
}
