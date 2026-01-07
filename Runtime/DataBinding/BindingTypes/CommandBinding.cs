using System;

namespace TravisRFrench.UI.MVVM.DataBinding.BindingTypes
{
	/// <summary>
	/// Binds an invocation signal (target event) to a command action (source).
	/// Framework-agnostic: notification is supplied via a Subscription.
	/// </summary>
	public sealed class CommandBinding : Binding
	{
		private readonly Subscription onInvoked;
		private readonly Action command;

		private readonly Action invokedHandler;

		public CommandBinding(Subscription onInvoked, Action command)
		{
			this.onInvoked = onInvoked;
			this.command = command;

			this.invokedHandler = this.OnInvoked;
		}

		protected override void OnBind()
		{
			this.onInvoked.Subscribe(this.invokedHandler);
		}

		protected override void OnUnbind()
		{
			this.onInvoked.Unsubscribe(this.invokedHandler);
		}

		public override void Refresh()
		{
			// No-op: commands are event driven.
		}

		private void OnInvoked()
		{
			if (!this.IsBound)
			{
				return;
			}

			this.command?.Invoke();
		}
	}
}
