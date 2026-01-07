using System;
using System.Collections.Generic;
using UnityEngine.Events;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;

namespace TravisRFrench.UI.MVVM.DataBinding.SubscriptionAdapters
{
	public sealed class UnityEventSubscriptionAdapter<TValue> : ISubscriptionAdapter
	{
		private readonly UnityEvent<TValue> unityEvent;

		public UnityEventSubscriptionAdapter(UnityEvent<TValue> unityEvent)
		{
			this.unityEvent = unityEvent;
		}

		public Subscription Adapt()
		{
			if (this.unityEvent == null)
			{
				throw new ArgumentNullException(nameof(this.unityEvent));
			}

			var map = new Dictionary<Action, UnityAction<TValue>>();

			return new Subscription(
				subscribe: handler =>
				{
					if (handler == null)
					{
						throw new ArgumentNullException(nameof(handler));
					}

					if (map.ContainsKey(handler))
					{
						return;
					}

					UnityAction<TValue> wrapped = _ => handler();

					map[handler] = wrapped;
					this.unityEvent.AddListener(wrapped);
				},
				unsubscribe: handler =>
				{
					if (handler == null)
					{
						return;
					}

					if (!map.TryGetValue(handler, out var wrapped))
					{
						return;
					}

					this.unityEvent.RemoveListener(wrapped);
					map.Remove(handler);
				}
			);
		}
	}
	
	/// <summary>
	/// Adapts a UnityEvent (no args) into a Subscription.
	/// </summary>
	public sealed class UnityEventSubscriptionAdapter : ISubscriptionAdapter
	{
		private readonly UnityEvent unityEvent;

		public UnityEventSubscriptionAdapter(UnityEvent unityEvent)
		{
			this.unityEvent = unityEvent;
		}

		public Subscription Adapt()
		{
			if (this.unityEvent == null)
			{
				throw new ArgumentNullException(nameof(this.unityEvent));
			}

			var map = new Dictionary<Action, UnityAction>();

			return new Subscription(
				subscribe: handler =>
				{
					if (handler == null)
					{
						throw new ArgumentNullException(nameof(handler));
					}

					if (map.ContainsKey(handler))
					{
						return;
					}

					UnityAction wrapped = () => handler();

					map[handler] = wrapped;
					this.unityEvent.AddListener(wrapped);
				},
				unsubscribe: handler =>
				{
					if (handler == null)
					{
						return;
					}

					if (!map.TryGetValue(handler, out var wrapped))
					{
						return;
					}

					this.unityEvent.RemoveListener(wrapped);
					map.Remove(handler);
				}
			);
		}
	}
}