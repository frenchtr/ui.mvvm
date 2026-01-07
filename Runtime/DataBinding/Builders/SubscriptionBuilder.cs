using System;
using System.ComponentModel;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;
using TravisRFrench.UI.MVVM.DataBinding.SubscriptionAdapters;
using UnityEngine.Events;

namespace TravisRFrench.UI.MVVM.DataBinding.Builders
{
	/// <summary>
	/// Shared core so the TValue and non-TValue builders stay DRY.
	/// </summary>
	public abstract class SubscriptionBuilderBase<TBinding>
	{
		private readonly TBinding bindingBuilder;

		public Subscription Subscription { get; protected set; }

		protected SubscriptionBuilderBase(TBinding bindingBuilder)
		{
			this.bindingBuilder = bindingBuilder;
		}

		protected TBinding ReturnToBindingBuilder() => this.bindingBuilder;

		protected Subscription CreatePropertyChangedSubscription(INotifyPropertyChanged notifier, string propertyName = null)
		{
			return new PropertyChangedSubscriptionAdapter(notifier, propertyName).Adapt();
		}

		protected Subscription CreateUnityEventSubscription(UnityEvent unityEvent)
		{
			return new UnityEventSubscriptionAdapter(unityEvent).Adapt();
		}

		protected Subscription CreateUnityEventSubscription<TArg>(UnityEvent<TArg> unityEvent)
		{
			return new UnityEventSubscriptionAdapter<TArg>(unityEvent).Adapt();
		}
	}

	/// <summary>
	/// Non-TValue variant: supports INotifyPropertyChanged and UnityEvent (no args).
	/// Also supports UnityEvent&lt;TArg&gt; when the caller provides TArg explicitly.
	/// </summary>
	public sealed class SubscriptionBuilder<TBinding> : SubscriptionBuilderBase<TBinding>
	{
		public SubscriptionBuilder(TBinding bindingBuilder) : base(bindingBuilder)
		{
		}

		public TBinding ByPropertyChangedNotifier(INotifyPropertyChanged notifier, string propertyName = null)
		{
			this.Subscription = CreatePropertyChangedSubscription(notifier, propertyName);
			return ReturnToBindingBuilder();
		}

		public TBinding ByUnityEvent(UnityEvent unityEvent)
		{
			this.Subscription = CreateUnityEventSubscription(unityEvent);
			return ReturnToBindingBuilder();
		}

		public TBinding ByUnityEvent<TArg>(UnityEvent<TArg> unityEvent)
		{
			this.Subscription = CreateUnityEventSubscription(unityEvent);
			return ReturnToBindingBuilder();
		}
	}

	/// <summary>
	/// TValue variant: convenience overload for UnityEvent&lt;TValue&gt;.
	/// </summary>
	public sealed class SubscriptionBuilder<TValue, TBinding> : SubscriptionBuilderBase<TBinding>
	{
		public SubscriptionBuilder(TBinding bindingBuilder) : base(bindingBuilder)
		{
		}

		public TBinding ByPropertyChangedNotifier(INotifyPropertyChanged notifier, string propertyName = null)
		{
			this.Subscription = CreatePropertyChangedSubscription(notifier, propertyName);
			return ReturnToBindingBuilder();
		}

		public TBinding ByUnityEvent(UnityEvent unityEvent)
		{
			this.Subscription = CreateUnityEventSubscription(unityEvent);
			return ReturnToBindingBuilder();
		}

		public TBinding ByUnityEvent(UnityEvent<TValue> unityEvent)
		{
			this.Subscription = CreateUnityEventSubscription(unityEvent);
			return ReturnToBindingBuilder();
		}
	}
}
