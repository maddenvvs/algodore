namespace DataStructures
{
    using System;
    using System.Collections.Generic;

    public class UnionFind<T>
    {
        public UnionFind()
            : this(EqualityComparer<T>.Default)
        {
        }

        public UnionFind(IEnumerable<T> collection)
            : this(collection, EqualityComparer<T>.Default)
        {
        }

        public UnionFind(IEqualityComparer<T> comparer)
            : this(Array.Empty<T>(), comparer)
        {
        }

        public UnionFind(IEnumerable<T> collection, IEqualityComparer<T> comparer)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            this.Parent = new Dictionary<T, T>(comparer);
            this.Size = new Dictionary<T, int>(comparer);
            this.DisjointSetsCount = 0;

            foreach (var item in collection)
            {
                this.MakeSet(item);
            }
        }

        public int Count { get => this.Parent.Count; }

        public int DisjointSetsCount { get; private set; }

        private Dictionary<T, T> Parent { get; set; }

        private Dictionary<T, int> Size { get; set; }

        private IEqualityComparer<T> Comparer { get; set; }

        public int SizeOf(T item) => this.Size[this.Find(item)];

        public void MakeSet(T item)
        {
            if (this.Parent.ContainsKey(item))
            {
                throw new ArgumentException("Union-find already contains given item.");
            }

            this.Parent[item] = item;
            this.Size[item] = 1;
            this.DisjointSetsCount++;
        }

        public T Find(T item)
        {
            if (!this.Parent.ContainsKey(item))
            {
                throw new ArgumentException("Union-find doesn't contain provided item.");
            }

            return this.GetRoot(item);
        }

        public bool Union(T first, T second)
        {
            T firstRoot = this.Find(first), secondRoot = this.Find(second);

            if (this.Comparer.Equals(firstRoot, secondRoot))
            {
                return false;
            }

            int firstSize = this.Size[firstRoot],
                secondSize = this.Size[secondRoot];
            if (firstSize < secondSize)
            {
                var tmp = firstRoot;
                firstRoot = secondRoot;
                secondRoot = tmp;
            }

            this.Parent[secondRoot] = firstRoot;
            this.Size[firstRoot] += this.Size[secondRoot];
            this.DisjointSetsCount--;

            return true;
        }

        private T GetRoot(T item)
        {
            if (this.Comparer.Equals(item, this.Parent[item]))
            {
                return item;
            }

            return this.Parent[item] = this.GetRoot(this.Parent[item]);
        }
    }
}
