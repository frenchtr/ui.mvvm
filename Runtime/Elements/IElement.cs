using TravisRFrench.UI.MVVM.Core;

namespace TravisRFrench.UI.MVVM.Elements
{
	public interface IElement : IViewModel,
		ICanSetActive,
		ICanSetEnabled,
		ICanSetVisible
	{
	}
}
