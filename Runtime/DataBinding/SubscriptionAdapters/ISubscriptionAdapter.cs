using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;

namespace UI.Binding.Adapters
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
