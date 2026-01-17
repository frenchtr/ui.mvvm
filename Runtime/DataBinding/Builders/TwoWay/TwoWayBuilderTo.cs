using System;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;
using TravisRFrench.UI.MVVM.DataBinding.Registration;

namespace TravisRFrench.UI.MVVM.DataBinding.Builders.TwoWay
{
	public sealed class TwoWayBuilderTo<TValue>
	{
		private readonly Func<TValue> getVm;
		private readonly Action<TValue> setVm;
		private readonly Subscription vmChanged;

		private readonly Func<TValue> getView;
		private readonly Action<TValue> setView;
		private readonly Subscription viewChanged;

		internal TwoWayBuilderTo(
			Func<TValue> getVm,
			Action<TValue> setVm,
			Subscription vmChanged,
			Func<TValue> getView,
			Action<TValue> setView,
			Subscription viewChanged)
		{
			this.getVm = getVm;
			this.setVm = setVm;
			this.vmChanged = vmChanged;
			this.getView = getView;
			this.setView = setView;
			this.viewChanged = viewChanged;
		}

		public void Register(IBindingRegistry registry)
		{
			if (registry == null) throw new ArgumentNullException(nameof(registry));

			var binding = new TwoWayBinding<TValue>(
				this.getVm,
				this.setVm,
				this.vmChanged,
				this.getView,
				this.setView,
				this.viewChanged);

			registry.Register(binding);
		}
	}
}