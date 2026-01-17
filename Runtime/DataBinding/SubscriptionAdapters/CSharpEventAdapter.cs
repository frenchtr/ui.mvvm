using System;
using System.Collections.Generic;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;

namespace UI.Binding.Adapters
{
	/// <summary>
	/// Adapts a C# event-like add/remove pair (event Action) to Subscription.
	/// </summary>
	public sealed class CSharpEventAdapter : ISubscriptionAdapter
	{
		private readonly Action<Action> add;
		private readonly Action<Action> remove;

		public CSharpEventAdapter(Action<Action> add, Action<Action> remove)
		{
			this.add = add ?? throw new ArgumentNullException(nameof(add));
			this.remove = remove ?? throw new ArgumentNullException(nameof(remove));
		}

		public Subscription Create()
		{
			var subscribed = new HashSet<Action>();

			void Subscribe(Action callback)
			{
				if (callback == null) throw new ArgumentNullException(nameof(callback));
				if (!subscribed.Add(callback)) return;

				this.add(callback);
			}

			void Unsubscribe(Action callback)
			{
				if (callback == null) throw new ArgumentNullException(nameof(callback));
				if (!subscribed.Remove(callback)) return;

				this.remove(callback);
			}

			return new Subscription(Subscribe, Unsubscribe);
		}
	}
	
	/// <summary>
	/// Adapts a C# event-like add/remove pair (event Action&lt;T&gt;) to Subscription&lt;T&gt;.
	/// </summary>
	public sealed class CSharpEventAdapter<T> : ISubscriptionAdapter<T>
	{
		private readonly Action<Action> add;
		private readonly Action<Action> remove;

		public CSharpEventAdapter(Action<Action> add, Action<Action> remove)
		{
			this.add = add ?? throw new ArgumentNullException(nameof(add));
			this.remove = remove ?? throw new ArgumentNullException(nameof(remove));
		}

		public Subscription Create()
		{
			var subscribed = new HashSet<Action>();

			void Subscribe(Action callback)
			{
				if (callback == null) throw new ArgumentNullException(nameof(callback));
				if (!subscribed.Add(callback)) return;

				this.add(callback);
			}

			void Unsubscribe(Action callback)
			{
				if (callback == null) throw new ArgumentNullException(nameof(callback));
				if (!subscribed.Remove(callback)) return;

				this.remove(callback);
			}

			return new Subscription(Subscribe, Unsubscribe);
		}
	}
}
