using System;
using System.Collections.Generic;

namespace DataStructures
{
    public class UnionFind<T>
    {
        public UnionFind() : this(EqualityComparer<T>.Default) { }

        public UnionFind(IEnumerable<T> collection) : this(collection, EqualityComparer<T>.Default) { }

        public UnionFind(IEqualityComparer<T> comparer) : this(Array.Empty<T>(), comparer) { }

        public UnionFind(IEnumerable<T> collection, IEqualityComparer<T> comparer)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));

            Parent = new Dictionary<T, T>(comparer);
            Size = new Dictionary<T, int>(comparer);
            DisjointSetsCount = 0;

            foreach (var item in collection)
            {
                MakeSet(item);
            }
        }

        private Dictionary<T, T> Parent { get; set; }

        private Dictionary<T, int> Size { get; set; }

        private IEqualityComparer<T> Comparer { get; set; }

        public int Count { get; } => Parent.Count;

        public int SizeOf(T item) => Size[Find(item)];

        public int DisjointSetsCount { get; private set; }

        public void MakeSet(T item)
        {
            if (Parent.ContainsKey(item))
            {
                throw new ArgumentException("Union-find already contains given item.");
            }

            Parent[item] = item;
            Size[item] = 1;
            DisjointSetsCount++;
        }

        public T Find(T item)
        {
            if (!Parent.ContainsKey(item))
            {
                throw new ArgumentException("Union-find doesn't contain provided item.");
            }

            return getRoot(item);
        }

        public bool Union(T first, T second)
        {
            var firstRoot = Find(first),
                secondRoot = Find(second);

            if (Comparer.Equals(firstRoot, secondRoot))
            {
                return false;
            }

            var firstSize = Size[firstRoot],
                secondSize = Size[secondRoot];
            if (firstSize < secondSize)
            {
                var tmp = firstRoot;
                firstRoot = secondRoot;
                secondRoot = tmp;
            }

            Parent[secondRoot] = firstRoot;
            Size[firstRoot] += Size[secondRoot];
            DisjointSetsCount--;

            return true;
        }

        private T getRoot(T item)
        {
            if (Comparer.Equals(item, Parent[item]))
            {
                return item;
            }

            return Parent[item] = getRoot(Parent[item]);
        }
    }
}
