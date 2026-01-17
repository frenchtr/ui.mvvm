using System;
using System.Linq.Expressions;
using TravisRFrench.UI.MVVM.Core;

namespace TravisRFrench.UI.MVVM.DataBinding.Builders.OneWay
{
    public sealed class OneWayBuilderStart
    {
        public OneWayBuilderFrom<TViewModel, TValue> From<TViewModel, TValue>(
            TViewModel viewModel,
            Expression<Func<TViewModel, TValue>> vmProperty)
            where TViewModel : class, IViewModel
            => new(viewModel, vmProperty);
    }
}
