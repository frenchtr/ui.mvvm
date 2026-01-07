using System;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;

namespace TravisRFrench.UI.MVVM.DataBinding.Builders
{
	public class TwoWayBindingBuilder<TValue>
	{
		private readonly SubscriptionBuilder<TValue, TwoWayBindingBuilder<TValue>> fromSubscriptionBuilder;
		private readonly SubscriptionBuilder<TValue, TwoWayBindingBuilder<TValue>> toSubscriptionBuilder;
		private Func<TValue> sourceGetter;
		private Action<TValue> sourceSetter;
		private Func<TValue> targetGetter;
		private Action<TValue> targetSetter;

		public TwoWayBindingBuilder()
		{
			this.fromSubscriptionBuilder = new SubscriptionBuilder<TValue, TwoWayBindingBuilder<TValue>>(this);
			this.toSubscriptionBuilder = new SubscriptionBuilder<TValue, TwoWayBindingBuilder<TValue>>(this);
		}

		public SubscriptionBuilder<TValue, TwoWayBindingBuilder<TValue>> From(Func<TValue> getter, Action<TValue> setter)
		{
			this.sourceGetter = getter;
			this.sourceSetter = setter;
			return this.fromSubscriptionBuilder;
		}
		
		public SubscriptionBuilder<TValue, TwoWayBindingBuilder<TValue>> To(Func<TValue> getter, Action<TValue> setter)
		{
			this.sourceGetter = getter;
			this.sourceSetter = setter;
			return this.toSubscriptionBuilder;
		}
		
		public TwoWayBinding<TValue> Build()
		{
			return new TwoWayBinding<TValue>(
				this.sourceGetter,
				this.sourceSetter,
				this.fromSubscriptionBuilder.Subscription,
				this.targetGetter,
				this.targetSetter,
				this.toSubscriptionBuilder.Subscription);
		}
	}
}
