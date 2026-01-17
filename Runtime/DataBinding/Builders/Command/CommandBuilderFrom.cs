using System;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;

namespace TravisRFrench.UI.MVVM.DataBinding.Builders.Command
{
	public sealed class CommandBuilderFrom
	{
		private readonly Subscription invoked;

		internal CommandBuilderFrom(Subscription invoked)
		{
			this.invoked = invoked;
		}

		public CommandBuilderTo To(Action execute)
		{
			if (execute == null) throw new ArgumentNullException(nameof(execute));
			return new CommandBuilderTo(this.invoked, execute);
		}
	}
}