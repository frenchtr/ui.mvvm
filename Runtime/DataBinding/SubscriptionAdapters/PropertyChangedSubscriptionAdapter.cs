using System;
using System.Collections.Generic;
using System.ComponentModel;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;

namespace TravisRFrench.UI.MVVM.DataBinding.SubscriptionAdapters
{
	public class PropertyChangedSubscriptionAdapter : ISubscriptionAdapter
	{
		private readonly INotifyPropertyChanged obj;
		private readonly string propertyName;

		public PropertyChangedSubscriptionAdapter(INotifyPropertyChanged obj, string propertyName = null)
		{
			this.obj = obj;
			this.propertyName = propertyName;
		}

		public Subscription Adapt()
		{
			if (this.obj == null)
			{
				throw new ArgumentNullException(nameof(this.obj));
			}

			var map = new Dictionary<Action, PropertyChangedEventHandler>();

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

					PropertyChangedEventHandler wrapped = (_, args) =>
					{
						var name = args?.PropertyName;

						if (string.IsNullOrEmpty(this.propertyName) ||
						    string.IsNullOrEmpty(name) ||
						    string.Equals(name, this.propertyName, StringComparison.Ordinal))
						{
							handler();
						}
					};

					map[handler] = wrapped;
					this.obj.PropertyChanged += wrapped;
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

					this.obj.PropertyChanged -= wrapped;
					map.Remove(handler);
				}
			);
		}
	}
}
