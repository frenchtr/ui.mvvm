using System;
using UnityEngine;

namespace TravisRFrench.UI.MVVM.Core
{
    [Serializable]
    public struct ViewProperty<TValue>
    {
        [SerializeReference]
        private TValue value;
        private Func<TValue> getter;
        private Action<TValue> setter;

        public TValue Value
        {
            get => this.getter();
            set
            {
                this.value = value;
                this.setter(value);
            }
        }

        public void BindGetter(Func<TValue> getter)
        {
            this.getter = getter;
        }
        
        public void BindSetter(Action<TValue> setter)
        {
            this.setter = setter;
        }
    }
}
