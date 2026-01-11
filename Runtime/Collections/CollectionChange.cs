namespace TravisRFrench.UI.MVVM.Collections
{
    public readonly struct CollectionChange
    {
        public CollectionChangeType Type { get; }
        public int Index { get; }
        public int OldIndex { get; }
        public int Count { get; }

        private CollectionChange(
            CollectionChangeType type,
            int index,
            int oldIndex,
            int count)
        {
            this.Type = type;
            this.Index = index;
            this.OldIndex = oldIndex;
            this.Count = count;
        }

        public static CollectionChange Add(int index, int count = 1)
            => new(CollectionChangeType.Add, index, -1, count);

        public static CollectionChange Remove(int index, int count = 1)
            => new(CollectionChangeType.Remove, index, -1, count);

        public static CollectionChange Replace(int index, int count = 1)
            => new(CollectionChangeType.Replace, index, -1, count);

        public static CollectionChange Move(int oldIndex, int newIndex)
            => new(CollectionChangeType.Move, newIndex, oldIndex, 1);

        public static CollectionChange Clear()
            => new(CollectionChangeType.Clear, -1, -1, 0);

        public static CollectionChange Reset()
            => new(CollectionChangeType.Reset, -1, -1, 0);
    }
}
