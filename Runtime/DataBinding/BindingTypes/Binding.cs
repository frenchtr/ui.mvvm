using System;

namespace TravisRFrench.UI.MVVM.DataBinding.BindingTypes
{
    public abstract class Binding : IBinding
    {
        public bool IsBound { get; private set; }
        
        public void Bind()
        {
            if (this.IsBound)
            {
                return;
            }
            
            this.OnBind();
            this.Refresh();
            this.IsBound = true;
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
        
        public virtual void Refresh()
        {
        }
        
        void IDisposable.Dispose()
        {
            this.Unbind();
        }
        
        protected virtual void OnBind()
        {
        }

        protected virtual void OnUnbind()
        {
        }
    }
}
