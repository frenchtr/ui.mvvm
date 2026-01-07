using System;
using System.ComponentModel;
using TravisRFrench.UI.MVVM.Core;

namespace TravisRFrench.UI.MVVM.DataBinding.BindingTypes
{
    public sealed class OneWayBinding<TValue> : Binding
    {
        private readonly IObservable observable;
        private readonly string observablePropertyName;
        private readonly Func<TValue> getter;
        private readonly Action<TValue> setter;

        public OneWayBinding(IObservable observable,
            string observablePropertyName,
            Func<TValue> getter,
            Action<TValue> setter)
        {
            this.observable = observable;
            this.observablePropertyName = observablePropertyName;
            this.getter = getter;
            this.setter = setter;
        }

        protected override void OnBind()
        {
            this.observable.PropertyChanged += this.OnObservablePropertyChanged;
        }

        protected override void OnUnbind()
        {
            this.observable.PropertyChanged -= this.OnObservablePropertyChanged;
        }
        
        public override void Refresh()
        {
            var value = this.getter();
            this.setter(value);
        }
        
        private void OnObservablePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var name = args?.PropertyName;
            
            // If no name was provided, refresh on any property change
            if (string.IsNullOrEmpty(this.observablePropertyName))
            {
                this.Refresh();
                return;
            }
            
            // If the property name matches our observable property name, refresh
            if (string.Equals(name, this.observablePropertyName, StringComparison.Ordinal))
            {
                this.Refresh();
            }
        }
    }
}
