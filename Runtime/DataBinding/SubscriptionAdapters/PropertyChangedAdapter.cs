using System;
using System.Collections.Generic;
using System.ComponentModel;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;

namespace TravisRFrench.UI.MVVM.DataBinding.SubscriptionAdapters
{
	/// <summary>
	/// Adapts INotifyPropertyChanged to fire when a specific property changes.
	/// </summary>
	public sealed class PropertyChangedAdapter : ISubscriptionAdapter
	{
		private readonly INotifyPropertyChanged source;
		private readonly string propertyName;

		public PropertyChangedAdapter(INotifyPropertyChanged source, string propertyName)
		{
			this.source = source ?? throw new ArgumentNullException(nameof(source));
			this.propertyName = string.IsNullOrWhiteSpace(propertyName)
				? throw new ArgumentException("Property name is required.", nameof(propertyName))
				: propertyName;
		}

		public Subscription Create()
		{
			var handlers = new Dictionary<Action, PropertyChangedEventHandler>();

			void Subscribe(Action callback)
			{
				if (callback == null) throw new ArgumentNullException(nameof(callback));
				if (handlers.ContainsKey(callback)) return;

				PropertyChangedEventHandler h = (sender, e) =>
				{
					if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == this.propertyName)
					{
						callback();
					}
				};

				handlers.Add(callback, h);
				this.source.PropertyChanged += h;
			}

			void Unsubscribe(Action callback)
			{
				if (callback == null) throw new ArgumentNullException(nameof(callback));
				if (!handlers.TryGetValue(callback, out var h)) return;

				this.source.PropertyChanged -= h;
				handlers.Remove(callback);
			}

			return new Subscription(Subscribe, Unsubscribe);
		}
	}
}