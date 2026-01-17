using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;

namespace TravisRFrench.UI.MVVM.DataBinding.SubscriptionAdapters
{
	/// <summary>
	/// Adapts various event systems into a framework-agnostic Subscription.
	/// </summary>
	public interface ISubscriptionAdapter
	{
		Subscription Create();
	}

	public interface ISubscriptionAdapter<T>
	{
		Subscription Create();
	}
}
