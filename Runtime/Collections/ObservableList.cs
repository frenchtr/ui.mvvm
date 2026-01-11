using System;
using System.Collections;
using System.Collections.Generic;

namespace TravisRFrench.UI.MVVM.Collections
{
    public sealed class ObservableList<T> : IObservableList<T>
    {
        private readonly List<T> items;

        public event Action<CollectionChange> Changed;

        public ObservableList()
        {
            this.items = new List<T>();
        }

        public ObservableList(IEnumerable<T> source)
        {
            this.items = new List<T>(source);
        }

        public int Count => this.items.Count;
        public T this[int index] => this.items[index];

        public void Add(T item)
        {
            var index = this.items.Count;
            this.items.Add(item);
            this.Changed?.Invoke(CollectionChange.Add(index));
        }

        public bool Remove(T item)
        {
            var index = this.items.IndexOf(item);
            if (index < 0)
                return false;

            this.items.RemoveAt(index);
            this.Changed?.Invoke(CollectionChange.Remove(index));
            return true;
        }

        public void RemoveAt(int index)
        {
            this.items.RemoveAt(index);
            this.Changed?.Invoke(CollectionChange.Remove(index));
        }

        public void Replace(int index, T item)
        {
            this.items[index] = item;
            this.Changed?.Invoke(CollectionChange.Replace(index));
        }

        public void Move(int oldIndex, int newIndex)
        {
            if (oldIndex == newIndex)
                return;

            var item = this.items[oldIndex];
            this.items.RemoveAt(oldIndex);
            this.items.Insert(newIndex, item);

            this.Changed?.Invoke(CollectionChange.Move(oldIndex, newIndex));
        }

        public void Clear()
        {
            if (this.items.Count == 0)
                return;

            this.items.Clear();
            this.Changed?.Invoke(CollectionChange.Clear());
        }

        public IEnumerator<T> GetEnumerator()
            => this.items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
    }
}
