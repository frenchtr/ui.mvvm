using System;

namespace TravisRFrench.UI.MVVM.DataBinding.BindingTypes
{
    /// <summary>
    /// Framework-agnostic event subscription wrapper.
    /// </summary>
    public readonly struct Subscription
    {
        private readonly Action<Action> subscribe;
        private readonly Action<Action> unsubscribe;

        public Subscription(Action<Action> subscribe, Action<Action> unsubscribe)
        {
            this.subscribe = subscribe ?? throw new ArgumentNullException(nameof(subscribe));
            this.unsubscribe = unsubscribe ?? throw new ArgumentNullException(nameof(unsubscribe));
        }

        public void Subscribe(Action handler) => this.subscribe(handler);
        public void Unsubscribe(Action handler) => this.unsubscribe(handler);
    }
    
    /// <summary>
    /// Typed subscription wrapper for Action&lt;T&gt; notifications.
    /// </summary>
    public sealed class Subscription<T>
    {
        private readonly Action<Action<T>> subscribe;
        private readonly Action<Action<T>> unsubscribe;

        public Subscription(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe)
        {
            this.subscribe = subscribe ?? throw new ArgumentNullException(nameof(subscribe));
            this.unsubscribe = unsubscribe ?? throw new ArgumentNullException(nameof(unsubscribe));
        }

        public void Subscribe(Action<T> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            this.subscribe(handler);
        }

        public void Unsubscribe(Action<T> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            this.unsubscribe(handler);
        }
    }
}
