using System;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;
using TravisRFrench.UI.MVVM.DataBinding.Registration;

namespace TravisRFrench.UI.MVVM.DataBinding.Builders.OneWay
{
	public sealed class OneWayBuilderTo<TValue>
	{
		private readonly Func<TValue> getSource;
		private readonly Action<TValue> setTarget;
		private readonly Subscription changed;

		internal OneWayBuilderTo(Func<TValue> getSource, Action<TValue> setTarget, Subscription changed)
		{
			this.getSource = getSource;
			this.setTarget = setTarget;
			this.changed = changed;
		}

		public void Register(IBindingRegistry registry)
		{
			if (registry == null) throw new ArgumentNullException(nameof(registry));

			var binding = new OneWayBinding<TValue>(this.getSource, this.setTarget, this.changed);
			registry.Register(binding);
		}
	}
}