using System;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;

namespace TravisRFrench.UI.MVVM.DataBinding.Registration
{
    public interface IBindingRegistry : IDisposable
    {
        void Register(IBinding binding);
        void Unregister(IBinding binding);
    }
}
