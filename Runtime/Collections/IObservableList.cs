using System;
using System.Collections.Generic;

namespace TravisRFrench.UI.MVVM.Collections
{
    public interface IObservableList<T> : IReadOnlyList<T>
    {
        event Action<CollectionChange> Changed;

        void Add(T item);
        bool Remove(T item);
        void RemoveAt(int index);
        void Replace(int index, T item);
        void Clear();
    }
}
