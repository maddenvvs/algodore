namespace DataStructures
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Union-Find (Disjoint Set Union) data structure implementation.
    /// </summary>
    /// <typeparam name="T">Type of values stored in the UF.</typeparam>
    public class UnionFind<T>
    {
        /// <summary>
        /// Creates empty UF.
        /// </summary>
        public UnionFind()
            : this(EqualityComparer<T>.Default)
        {
        }

        /// <summary>
        /// Creates empty UF and populates it with values from the collection.
        ///
        /// Time complexity: O(N).
        ///
        /// Space complexity: O(N).
        ///
        /// N - length of the collection.
        /// </summary>
        /// <param name="collection">
        /// The collection to populate values from.
        /// </param>
        public UnionFind(IEnumerable<T> collection)
            : this(collection, EqualityComparer<T>.Default)
        {
        }

        /// <summary>
        /// Creates empty UF with custom equality comparer.
        /// </summary>
        /// <param name="comparer"> Custom comparer implementation.</param>
        public UnionFind(IEqualityComparer<T> comparer)
            : this(Array.Empty<T>(), comparer)
        {
        }

        /// <summary>
        /// Creates empty UF with custom equality comparer and populates it
        /// with values from the collection.
        ///
        /// Time complexity: O(N).
        ///
        /// Space complexity: O(N).
        ///
        /// N - length of the collection.
        /// </summary>
        /// <param name="collection">
        /// The collection to populate values from.
        /// </param>
        /// <param name="comparer"> Custom comparer implementation.</param>
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
            this.Comparer = comparer;

            foreach (var item in collection)
            {
                this.MakeSet(item);
            }
        }

        /// <summary>
        /// Returns total number of values stored in the UF.
        ///
        /// Time complexity: O(1).
        ///
        /// Space complexity: O(1).
        /// </summary>
        /// <value>The total number of values.</value>
        public int Count => this.Parent.Count;

        /// <summary>
        /// Returns total number of disjoint sets in the UF.
        ///
        /// Time complexity: O(1).
        ///
        /// Space complexity: O(1).
        /// </summary>
        /// <value>The total number of disjoint sets.</value>
        public int DisjointSetsCount { get; private set; }

        private Dictionary<T, T> Parent { get; }

        private Dictionary<T, int> Size { get; }

        private IEqualityComparer<T> Comparer { get; }

        /// <summary>
        /// Returns the size of the set the given item belongs to.
        ///
        /// Time complexity: O(α(N)).
        ///
        /// Space complexity: O(α(N)).
        ///
        /// N - number of items in the UF. α(N) - inverse Ackermann function.
        /// </summary>
        /// <param name="item">The item of the set of question.</param>
        /// <exception cref="System.ArgumentException">
        /// Thrown when given item isn't contained in the UF.
        /// </exception>
        /// <returns>Returns the size of the set of question.</returns>
        public int SizeOf(T item) => this.Size[this.Find(item)];

        /// <summary>
        /// Creates new disjoint set with given item.
        ///
        /// Time complexity: O(1).
        ///
        /// Space complexity: O(1).
        /// </summary>
        /// <param name="item">The item (and root) of the new set.</param>
        /// <exception cref="System.ArgumentException">
        /// Thrown when given item is already contained in the UF.
        /// </exception>
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

        /// <summary>
        /// Returns root item of the set the given item belongs to.
        ///
        /// Time complexity: O(α(N)).
        ///
        /// Space complexity: O(α(N)).
        ///
        /// N - number of items in the UF. α(N) - inverse Ackermann function.
        /// </summary>
        /// <param name="item">The item of the set.</param>
        /// <exception cref="System.ArgumentException">
        /// Thrown when given item isn't contained in the UF.
        /// </exception>
        /// <returns>Returns the root item of the found set.</returns>
        public T Find(T item)
        {
            if (!this.Parent.ContainsKey(item))
            {
                throw new ArgumentException("Union-find doesn't contain provided item.");
            }

            return this.GetRoot(item);
        }

        /// <summary>
        /// Unions two sets two items are belong to respectively.
        ///
        /// Time complexity: O(α(N)).
        ///
        /// Space complexity: O(α(N)).
        ///
        /// N - number of items in the UF. α(N) - inverse Ackermann function.
        /// </summary>
        /// <param name="first">The element of the first set.</param>
        /// <param name="second">The element of the second set.</param>
        /// <returns>Returns true if items united, false otherwise.</returns>
        public bool Union(T first, T second)
        {
            var (firstRoot, secondRoot) = (this.Find(first), this.Find(second));

            if (this.Comparer.Equals(firstRoot, secondRoot))
            {
                return false;
            }

            (int first, int second) sizes =
                (this.Size[firstRoot], this.Size[secondRoot]);

            if (sizes.first < sizes.second)
            {
                (firstRoot, secondRoot) = (secondRoot, firstRoot);
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
