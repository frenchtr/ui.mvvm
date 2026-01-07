using System;

namespace TravisRFrench.UI.MVVM.DataBinding
{
    public interface IValueChangeNotifier
    {
        event Action ValueChanged;
    }
}
