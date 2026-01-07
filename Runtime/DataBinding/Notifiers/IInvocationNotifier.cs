using System;

namespace TravisRFrench.UI.MVVM.DataBinding.Notifiers
{
    public interface IInvocationNotifier
    {
        event Action Invoked;
    }
}
