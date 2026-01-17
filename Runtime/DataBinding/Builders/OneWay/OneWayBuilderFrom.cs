using System;
using System.Linq.Expressions;
using TravisRFrench.UI.MVVM.Core;
using TravisRFrench.UI.MVVM.DataBinding.SubscriptionAdapters;

namespace TravisRFrench.UI.MVVM.DataBinding.Builders.OneWay
{
	public sealed class OneWayBuilderFrom<TViewModel, TValue>
		where TViewModel : class, IViewModel
	{
		private readonly TViewModel viewModel;
		private readonly string propertyName;
		private readonly Func<TValue> getVm;
		private readonly SubscriptionFactory subscriptions = new();

		internal OneWayBuilderFrom(TViewModel viewModel, Expression<Func<TViewModel, TValue>> vmProperty)
		{
			this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

			this.propertyName = ExpressionUtil.PropertyName(vmProperty);
			var getter = vmProperty.Compile();
			this.getVm = () => getter(this.viewModel);
		}

		public OneWayBuilderTo<TValue> To(Action<TValue> setView)
		{
			if (setView == null) throw new ArgumentNullException(nameof(setView));

			var changed = this.subscriptions.FromProperty(this.viewModel, this.propertyName);
			return new OneWayBuilderTo<TValue>(this.getVm, setView, changed);
		}
	}
}