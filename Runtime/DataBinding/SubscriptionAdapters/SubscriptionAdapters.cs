using System.ComponentModel;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;
using UnityEngine.Events;

namespace TravisRFrench.UI.MVVM.DataBinding.SubscriptionAdapters
{
	public static class Subscriptions
	{
		public static Subscription CreateFromPropertyChangedNotifier(
			INotifyPropertyChanged obj,
			string propertyName = null)
		{
			return new PropertyChangedSubscriptionAdapter(obj, propertyName).Adapt();
		}

		public static Subscription CreateFromUnityEvent(UnityEvent unityEvent)
		{
			return new UnityEventSubscriptionAdapter(unityEvent).Adapt();
		}

		public static Subscription CreateFromUnityEvent<T>(UnityEvent<T> unityEvent)
		{
			return new UnityEventSubscriptionAdapter<T>(unityEvent).Adapt();
		}
	}
}
