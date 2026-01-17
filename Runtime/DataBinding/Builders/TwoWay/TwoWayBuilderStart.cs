using System;
using System.Linq.Expressions;
using TravisRFrench.UI.MVVM.Core;

namespace TravisRFrench.UI.MVVM.DataBinding.Builders.TwoWay
{
    public sealed class TwoWayBuilderStart
    {
        public TwoWayBuilderFrom<TViewModel, TValue> From<TViewModel, TValue>(
            TViewModel viewModel,
            Expression<Func<TViewModel, TValue>> vmProperty)
            where TViewModel : class, IViewModel
            => new(viewModel, vmProperty);
    }
}
