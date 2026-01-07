using System;

namespace TravisRFrench.UI.MVVM.DataBinding.BindingTypes
{
    public sealed class CommandBinding : Binding
    {
        private readonly IInvocationNotifier target;
        private readonly Action sourceCommand;
        
        public CommandBinding(IInvocationNotifier target, Action sourceCommand)
        {
            this.target = target;
            this.sourceCommand = sourceCommand;
        }

        protected override void OnBind()
        {
            this.target.Invoked += this.OnTargetInvoked;
        }

        protected override void OnUnbind()
        {
            this.target.Invoked -= this.OnTargetInvoked;
        }

        public override void Refresh()
        {
        }
        
        private void OnTargetInvoked()
        {
            this.sourceCommand?.Invoke();
        }
    }
}
