using System;
using TravisRFrench.UI.MVVM.DataBinding.SubscriptionAdapters;
using UnityEngine.Events;

namespace TravisRFrench.UI.MVVM.DataBinding.Builders.Command
{
    public sealed class CommandBuilderStart
    {
        private readonly SubscriptionFactory subscriptions = new();

        public CommandBuilderFrom From(UnityEvent invoked)
        {
            if (invoked == null) throw new ArgumentNullException(nameof(invoked));
            return new CommandBuilderFrom(this.subscriptions.FromUnityEvent(invoked));
        }

        public CommandBuilderFrom From<T>(UnityEvent<T> invoked)
        {
            if (invoked == null) throw new ArgumentNullException(nameof(invoked));
            return new CommandBuilderFrom(this.subscriptions.FromUnityEvent(invoked));
        }

        public CommandBuilderFrom From(Action<Action> add, Action<Action> remove)
        {
            if (add == null) throw new ArgumentNullException(nameof(add));
            if (remove == null) throw new ArgumentNullException(nameof(remove));
            return new CommandBuilderFrom(this.subscriptions.FromEvent(add, remove));
        }
    }
}
