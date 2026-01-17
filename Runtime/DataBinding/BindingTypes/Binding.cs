using System;

namespace TravisRFrench.UI.MVVM.DataBinding.BindingTypes
{
	/// <summary>
	/// Disposable binding lifetime. Call Bind() to start, Dispose() (or Unbind()) to stop.
	/// </summary>
	public abstract class Binding : IBinding, IDisposable
	{
		public bool IsBound { get; private set; }

		public void Bind()
		{
			if (this.IsBound)
			{
				return;
			}

			this.IsBound = true;
			this.OnBind();
		}

		public void Unbind()
		{
			if (!this.IsBound)
			{
				return;
			}

			this.OnUnbind();
			this.IsBound = false;
		}

		public void Dispose() => this.Unbind();

		/// <summary>Pushes the current A value into B (or equivalent for derived types).</summary>
		public abstract void Refresh();

		protected abstract void OnBind();
		protected abstract void OnUnbind();
	}
}
