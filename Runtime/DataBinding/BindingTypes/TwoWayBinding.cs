using System;
using System.Collections.Generic;

namespace TravisRFrench.UI.MVVM.DataBinding.BindingTypes
{
    public sealed class TwoWayBinding<T> : Binding
    {
        private readonly Func<T> getA;
        private readonly Action<T> setA;
        private readonly Subscription onAChanged;

        private readonly Func<T> getB;
        private readonly Action<T> setB;
        private readonly Subscription onBChanged;

        private readonly Func<T, T, bool> areEqual;

        private bool refreshingFromA;
        private bool refreshingFromB;

        private readonly Action aChangedHandler;
        private readonly Action bChangedHandler;

        public TwoWayBinding(
            Func<T> getA,
            Action<T> setA,
            Subscription onAChanged,
            Func<T> getB,
            Action<T> setB,
            Subscription onBChanged,
            Func<T, T, bool> areEqual = null)
        {
            this.getA = getA ?? throw new ArgumentNullException(nameof(getA));
            this.setA = setA ?? throw new ArgumentNullException(nameof(setA));
            this.onAChanged = onAChanged;

            this.getB = getB ?? throw new ArgumentNullException(nameof(getB));
            this.setB = setB ?? throw new ArgumentNullException(nameof(setB));
            this.onBChanged = onBChanged;

            this.areEqual = areEqual ?? EqualityComparer<T>.Default.Equals;

            this.aChangedHandler = this.OnAChanged;
            this.bChangedHandler = this.OnBChanged;
        }

        protected override void OnBind()
        {
            this.onAChanged.Subscribe(this.aChangedHandler);
            this.onBChanged.Subscribe(this.bChangedHandler);
        }

        protected override void OnUnbind()
        {
            this.onAChanged.Unsubscribe(this.aChangedHandler);
            this.onBChanged.Unsubscribe(this.bChangedHandler);
        }

        /// <summary>Default refresh pushes A -> B.</summary>
        public override void Refresh() => this.RefreshFromA();

        private void RefreshFromA()
        {
            if (!this.IsBound || this.refreshingFromB)
            {
                return;
            }

            try
            {
                this.refreshingFromA = true;

                var a = this.getA();
                var b = this.getB();

                // Optional: avoid redundant sets (can prevent feedback loops in some systems).
                if (!this.areEqual(a, b))
                {
                    this.setB(a);
                }
            }
            finally
            {
                this.refreshingFromA = false;
            }
        }

        private void RefreshFromB()
        {
            if (!this.IsBound || this.refreshingFromA)
            {
                return;
            }

            try
            {
                this.refreshingFromB = true;

                var b = this.getB();
                var a = this.getA();

                if (!this.areEqual(a, b))
                {
                    this.setA(b);
                }
            }
            finally
            {
                this.refreshingFromB = false;
            }
        }

        private void OnAChanged() => this.RefreshFromA();
        private void OnBChanged() => this.RefreshFromB();
    }
}
