using System;

namespace TravisRFrench.UI.MVVM.DataBinding.Notifiers
{
    public interface IValueChangeNotifier
    {
        event Action ValueChanged;
    }
}
