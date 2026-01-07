using System;

namespace TravisRFrench.UI.MVVM.DataBinding.BindingTypes
{
    /// <summary>
    /// Binds A -> B. When A changes, pushes A's value into B.
    /// Agnostic to MVVM; all notification and get/set behavior is supplied by delegates.
    /// </summary>
    public sealed class OneWayBinding<T> : Binding
    {
        private readonly Func<T> getSource;
        private readonly Action<T> setTarget;
        private readonly Subscription onSourceChanged;

        private readonly Action sourceChangedHandler;

        public OneWayBinding(
            Func<T> getSource,
            Action<T> setTarget,
            Subscription onSourceChanged)
        {
            this.getSource = getSource ?? throw new ArgumentNullException(nameof(getSource));
            this.setTarget = setTarget ?? throw new ArgumentNullException(nameof(setTarget));
            this.onSourceChanged = onSourceChanged;

            this.sourceChangedHandler = this.OnSourceChanged;
        }

        protected override void OnBind()
        {
            this.onSourceChanged.Subscribe(this.sourceChangedHandler);
        }

        protected override void OnUnbind()
        {
            this.onSourceChanged.Unsubscribe(this.sourceChangedHandler);
        }

        public override void Refresh()
        {
            if (!this.IsBound)
            {
                return;
            }

            this.setTarget(this.getSource());
        }

        private void OnSourceChanged()
        {
            if (!this.IsBound)
            {
                return;
            }

            this.Refresh();
        }
    }

    /// <summary>
    /// One-way binding with conversion: A(TSource) -> B(TTarget).
    /// </summary>
    public sealed class OneWayBinding<TSource, TTarget> : Binding
    {
        private readonly Func<TSource> getSource;
        private readonly Action<TTarget> setTarget;
        private readonly Func<TSource, TTarget> convert;
        private readonly Subscription onSourceChanged;

        private readonly Action sourceChangedHandler;

        public OneWayBinding(
            Func<TSource> getSource,
            Action<TTarget> setTarget,
            Subscription onSourceChanged,
            Func<TSource, TTarget> convert)
        {
            this.getSource = getSource ?? throw new ArgumentNullException(nameof(getSource));
            this.setTarget = setTarget ?? throw new ArgumentNullException(nameof(setTarget));
            this.onSourceChanged = onSourceChanged;
            this.convert = convert ?? throw new ArgumentNullException(nameof(convert));

            this.sourceChangedHandler = this.OnSourceChanged;
        }

        protected override void OnBind()
        {
            this.onSourceChanged.Subscribe(this.sourceChangedHandler);
        }

        protected override void OnUnbind()
        {
            this.onSourceChanged.Unsubscribe(this.sourceChangedHandler);
        }

        public override void Refresh()
        {
            if (!this.IsBound)
            {
                return;
            }

            this.setTarget(this.convert(this.getSource()));
        }

        private void OnSourceChanged()
        {
            if (!this.IsBound)
            {
                return;
            }

            this.Refresh();
        }
    }
}
