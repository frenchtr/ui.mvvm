using System;

namespace TravisRFrench.UI.MVVM.DataBinding.BindingTypes
{
    public interface IBinding : IDisposable
    {
        bool IsBound { get; }

        void Bind();
        void Unbind();
        void Refresh();
    }
}
