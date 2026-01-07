using System;

namespace TravisRFrench.UI.MVVM.DataBinding
{
    public interface IInvocationNotifier
    {
        event Action Invoked;
    }
}
