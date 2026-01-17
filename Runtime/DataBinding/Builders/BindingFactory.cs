using TravisRFrench.UI.MVVM.DataBinding.Builders.Command;
using TravisRFrench.UI.MVVM.DataBinding.Builders.OneWay;
using TravisRFrench.UI.MVVM.DataBinding.Builders.TwoWay;

namespace TravisRFrench.UI.MVVM.DataBinding.Builders
{
	public static class BindingFactory
	{
		public static OneWayBuilderStart CreateOneWay() => new();
		public static TwoWayBuilderStart CreateTwoWay() => new();
		public static CommandBuilderStart CreateCommand() => new();
	}
}