using System;
using System.Collections.Generic;
using System.Linq;
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

        public IReadOnlyList<IBinding> Bindings => this.bindings.AsReadOnly();

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
        
        public void BindAll()
        {
            foreach (var binding in this.Bindings
                         .Where(b => !b.IsBound))
            {
                binding.Bind();
            }
        }

        public void UnbindAll()
        {
            foreach (var binding in this.Bindings
                         .Where(b => b.IsBound))
            {
                binding.Unbind();
            }
        }
        
        public void Dispose()
        {
            foreach (var binding in this.bindings)
            {
                binding.Dispose();
            }
            
            this.bindings.Clear();
        }
    }
}
