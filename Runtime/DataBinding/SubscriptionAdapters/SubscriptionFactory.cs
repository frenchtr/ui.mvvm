using System;
using System.ComponentModel;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;
using UnityEngine.Events;

namespace TravisRFrench.UI.MVVM.DataBinding.SubscriptionAdapters
{
	public sealed class SubscriptionFactory
	{
		public Subscription FromProperty(INotifyPropertyChanged source, string propertyName)
			=> new PropertyChangedAdapter(source, propertyName).Create();

		public Subscription FromUnityEvent(UnityEvent unityEvent)
			=> new UnityEventAdapter(unityEvent).Create();

		public Subscription FromUnityEvent<T>(UnityEvent<T> unityEvent)
			=> new UnityEventAdapter<T>(unityEvent).Create();
		
		public Subscription FromEvent(Action<Action> add, Action<Action> remove)
			=> new CSharpEventAdapter(add, remove).Create();
	}
}
