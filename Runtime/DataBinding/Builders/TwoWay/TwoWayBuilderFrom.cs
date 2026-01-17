using System;
using System.Linq.Expressions;
using TravisRFrench.UI.MVVM.Core;
using TravisRFrench.UI.MVVM.DataBinding.SubscriptionAdapters;
using UnityEngine.Events;

namespace TravisRFrench.UI.MVVM.DataBinding.Builders.TwoWay
{
	public sealed class TwoWayBuilderFrom<TViewModel, TValue>
		where TViewModel : class, IViewModel
	{
		private readonly TViewModel viewModel;
		private readonly string propertyName;
		private readonly Func<TValue> getVm;
		private readonly Action<TValue> setVm;

		private readonly SubscriptionFactory subscriptions = new();

		internal TwoWayBuilderFrom(TViewModel viewModel, Expression<Func<TViewModel, TValue>> vmProperty)
		{
			this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

			this.propertyName = ExpressionUtil.PropertyName(vmProperty);

			var getter = vmProperty.Compile();
			var setter = ExpressionUtil.Setter(vmProperty);

			this.getVm = () => getter(this.viewModel);
			this.setVm = v => setter(this.viewModel, v);
		}

		public TwoWayBuilderTo<TValue> To(
			Func<TValue> getView,
			Action<TValue> setView,
			UnityEvent viewChanged)
		{
			if (getView == null) throw new ArgumentNullException(nameof(getView));
			if (setView == null) throw new ArgumentNullException(nameof(setView));
			if (viewChanged == null) throw new ArgumentNullException(nameof(viewChanged));

			var vmChanged = this.subscriptions.FromProperty(this.viewModel, this.propertyName);
			var vChanged = this.subscriptions.FromUnityEvent(viewChanged);

			return new TwoWayBuilderTo<TValue>(this.getVm, this.setVm, vmChanged, getView, setView, vChanged);
		}

		public TwoWayBuilderTo<TValue> To(
			Func<TValue> getView,
			Action<TValue> setView,
			UnityEvent<TValue> viewChanged)
		{
			if (getView == null) throw new ArgumentNullException(nameof(getView));
			if (setView == null) throw new ArgumentNullException(nameof(setView));
			if (viewChanged == null) throw new ArgumentNullException(nameof(viewChanged));

			var vmChanged = this.subscriptions.FromProperty(this.viewModel, this.propertyName);
			var vChanged = this.subscriptions.FromUnityEvent(viewChanged); // payload ignored

			return new TwoWayBuilderTo<TValue>(this.getVm, this.setVm, vmChanged, getView, setView, vChanged);
		}

		public TwoWayBuilderTo<TValue> To(
			Func<TValue> getView,
			Action<TValue> setView,
			Action<Action> add,
			Action<Action> remove)
		{
			if (getView == null) throw new ArgumentNullException(nameof(getView));
			if (setView == null) throw new ArgumentNullException(nameof(setView));
			if (add == null) throw new ArgumentNullException(nameof(add));
			if (remove == null) throw new ArgumentNullException(nameof(remove));

			var vmChanged = this.subscriptions.FromProperty(this.viewModel, this.propertyName);
			var vChanged = this.subscriptions.FromEvent(add, remove);

			return new TwoWayBuilderTo<TValue>(this.getVm, this.setVm, vmChanged, getView, setView, vChanged);
		}
	}
}