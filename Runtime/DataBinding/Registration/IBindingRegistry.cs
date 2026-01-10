using System;
using System.Collections.Generic;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;

namespace TravisRFrench.UI.MVVM.DataBinding.Registration
{
    public interface IBindingRegistry : IDisposable
    {
        IReadOnlyList<IBinding> Bindings { get; }
        
        void Register(IBinding binding);
        void Unregister(IBinding binding);
        void BindAll();
        void UnbindAll();
    }
}
