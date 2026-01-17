using System;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;
using TravisRFrench.UI.MVVM.DataBinding.Registration;

namespace TravisRFrench.UI.MVVM.DataBinding.Builders.Command
{
	public sealed class CommandBuilderTo
	{
		private readonly Subscription invoked;
		private readonly Action execute;

		internal CommandBuilderTo(Subscription invoked, Action execute)
		{
			this.invoked = invoked;
			this.execute = execute;
		}

		public void Register(IBindingRegistry registry)
		{
			if (registry == null) throw new ArgumentNullException(nameof(registry));

			var binding = new CommandBinding(this.invoked, this.execute);
			registry.Register(binding);
		}
	}
}