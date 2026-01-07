using System;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;

namespace TravisRFrench.UI.MVVM.DataBinding.Builders
{
	public class OneWayBindingBuilder<TValue>
	{
		private Func<TValue> sourceGetter;
		private Action<TValue> targetSetter;
		private SubscriptionBuilder<TValue, OneWayBindingBuilder<TValue>> sourceSubscriptionBuilder;

		public OneWayBindingBuilder()
		{
			this.sourceSubscriptionBuilder = new SubscriptionBuilder<TValue, OneWayBindingBuilder<TValue>>(this);
		}
		
		public SubscriptionBuilder<TValue, OneWayBindingBuilder<TValue>> From(Func<TValue> getter)
		{
			this.sourceGetter = getter;
			return this.sourceSubscriptionBuilder;
		}
		
		public OneWayBindingBuilder<TValue> To(Action<TValue> setter)
		{
			this.targetSetter = setter;

			return this;
		}
		
		public OneWayBinding<TValue> Build()
		{
			return new OneWayBinding<TValue>(this.sourceGetter, this.targetSetter, this.sourceSubscriptionBuilder.Subscription);
		}
	}
}
