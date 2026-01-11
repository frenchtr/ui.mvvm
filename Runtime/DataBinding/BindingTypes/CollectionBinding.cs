// =========================
// File: CollectionBinding.cs
// =========================
using System;
using TravisRFrench.UI.MVVM.Collections;

namespace TravisRFrench.UI.MVVM.DataBinding.BindingTypes
{
    /// <summary>
    /// Binds an observable collection to a target. When the source changes, pushes a change descriptor into the target.
    /// </summary>
    public sealed class CollectionBinding : Binding
    {
        private readonly Action<CollectionChange> applyChange;
        private readonly Subscription<CollectionChange> onSourceChanged;

        private readonly Action<CollectionChange> sourceChangedHandler;

        public CollectionBinding(
            Action<CollectionChange> applyChange,
            Subscription<CollectionChange> onSourceChanged)
        {
            this.applyChange = applyChange ?? throw new ArgumentNullException(nameof(applyChange));
            this.onSourceChanged = onSourceChanged ?? throw new ArgumentNullException(nameof(onSourceChanged));

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
                return;

            this.applyChange(CollectionChange.Reset());
        }

        private void OnSourceChanged(CollectionChange change)
        {
            if (!this.IsBound)
                return;

            this.applyChange(change);
        }
    }
}
