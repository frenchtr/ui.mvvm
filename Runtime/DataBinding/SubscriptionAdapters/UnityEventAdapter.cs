using System;
using System.Collections.Generic;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;
using UnityEngine.Events;

namespace TravisRFrench.UI.MVVM.DataBinding.SubscriptionAdapters
{
	/// <summary>
	/// Adapts a UnityEvent (no args) to Subscription.
	/// </summary>
	public sealed class UnityEventAdapter : ISubscriptionAdapter
	{
		private readonly UnityEvent unityEvent;

		public UnityEventAdapter(UnityEvent unityEvent)
		{
			this.unityEvent = unityEvent ?? throw new ArgumentNullException(nameof(unityEvent));
		}

		public Subscription Create()
		{
			var handlers = new Dictionary<Action, UnityAction>();

			void Subscribe(Action callback)
			{
				if (callback == null) throw new ArgumentNullException(nameof(callback));
				if (handlers.ContainsKey(callback)) return;

				UnityAction ua = callback.Invoke; // no lambda allocation; still must be cached for RemoveListener
				handlers.Add(callback, ua);
				this.unityEvent.AddListener(ua);
			}

			void Unsubscribe(Action callback)
			{
				if (callback == null) throw new ArgumentNullException(nameof(callback));
				if (!handlers.TryGetValue(callback, out var ua)) return;

				this.unityEvent.RemoveListener(ua);
				handlers.Remove(callback);
			}

			return new Subscription(Subscribe, Unsubscribe);
		}
	}
	
	/// <summary>
	/// Adapts UnityEvent&lt;T&gt; to Subscription&lt;T&gt;.
	/// </summary>
	public sealed class UnityEventAdapter<T> : ISubscriptionAdapter<T>
	{
		private readonly UnityEvent<T> unityEvent;

		public UnityEventAdapter(UnityEvent<T> unityEvent)
		{
			this.unityEvent = unityEvent ?? throw new ArgumentNullException(nameof(unityEvent));
		}

		public Subscription Create()
		{
			var handlers = new Dictionary<Action, UnityAction<T>>();

			void Subscribe(Action callback)
			{
				if (callback == null) throw new ArgumentNullException(nameof(callback));
				if (handlers.ContainsKey(callback)) return;

				UnityAction<T> ua = _ => callback.Invoke();
				handlers.Add(callback, ua);
				this.unityEvent.AddListener(ua);
			}

			void Unsubscribe(Action callback)
			{
				if (callback == null) throw new ArgumentNullException(nameof(callback));
				if (!handlers.TryGetValue(callback, out var ua)) return;

				this.unityEvent.RemoveListener(ua);
				handlers.Remove(callback);
			}

			return new Subscription(Subscribe, Unsubscribe);
		}
	}
}
