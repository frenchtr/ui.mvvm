using System;
using System.ComponentModel;
using TravisRFrench.UI.MVVM.Core;

namespace TravisRFrench.UI.MVVM.DataBinding.BindingTypes
{
    public sealed class TwoWayBinding<TValue> : Binding
    {
        private readonly IObservable source;
        private readonly string sourcePropertyName;
        private readonly IValueChangeNotifier target;
        private readonly Func<TValue> sourceGetter;
        private readonly Action<TValue> sourceSetter;
        private readonly Func<TValue> targetGetter;
        private readonly Action<TValue> targetSetter;
        private bool isRefreshingFromSource;
        private bool isRefreshingFromTarget;

        public TwoWayBinding(
            IObservable source, 
            string sourcePropertyName,
            IValueChangeNotifier target,
            Func<TValue> sourceGetter,
            Action<TValue> sourceSetter,
            Func<TValue> targetGetter,
            Action<TValue> targetSetter)
        {
            this.source = source;
            this.sourcePropertyName = sourcePropertyName;
            this.target = target;
            this.sourceGetter = sourceGetter;
            this.sourceSetter = sourceSetter;
            this.targetGetter = targetGetter;
            this.targetSetter = targetSetter;
        }

        protected override void OnBind()
        {
            this.source.PropertyChanged += this.OnSourcePropertyChanged;
            this.target.ValueChanged += this.OnTargetValueChanged;
        }

        protected override void OnUnbind()
        {
            this.source.PropertyChanged -= this.OnSourcePropertyChanged;
            this.target.ValueChanged -= this.OnTargetValueChanged;
        }

        public override void Refresh()
        {
            if (this.isRefreshingFromTarget)
            {
                return;
            }

            try
            {
                this.isRefreshingFromSource = true;

                var value = this.sourceGetter();
                this.targetSetter(value);
            }
            finally
            {
                this.isRefreshingFromSource = false;
            }
        }
        
        private void RefreshFromTarget()
        {
            if (this.isRefreshingFromSource)
            {
                return;
            }

            try
            {
                this.isRefreshingFromTarget = true;

                var value = this.targetGetter();
                this.sourceSetter(value);
            }
            finally
            {
                this.isRefreshingFromTarget = false;
            }
        }
        
        private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (!this.IsBound)
            {
                return;
            }

            var name = args?.PropertyName;

            if (string.IsNullOrEmpty(this.sourcePropertyName) ||
                string.IsNullOrEmpty(name) ||
                string.Equals(name, this.sourcePropertyName, StringComparison.Ordinal))
            {
                this.Refresh();
            }
        }
        
        private void OnTargetValueChanged()
        {
            if (!this.IsBound)
            {
                return;
            }
            
            this.RefreshFromTarget();
        }
    }
}
