using System;

namespace TravisRFrench.UI.MVVM.Core
{
    public interface IViewModel : IObservable, IDisposable
    {
        void Initialize();
    }
}
