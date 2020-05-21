namespace DataStructures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Min binary heap implementation.
    /// </summary>
    /// <typeparam name="T">Type of elements stored in the heap.</typeparam>
    public sealed class BinaryHeap<T>
    {
        /// <summary>
        /// Construct empty default min binary heap.
        ///
        /// Time complexity: O(1).
        ///
        /// Space complexity: O(1).
        /// </summary>
        public BinaryHeap()
            : this(Array.Empty<T>(), Comparer<T>.Default)
        {
        }

        /// <summary>
        /// Construct default min binary heap from the given collection.
        ///
        /// Time complexity: O(N).
        ///
        /// Space complexity: O(N).
        ///
        /// N is size of the collection.
        /// </summary>
        /// <param name="collection">The elements collection to build the heap from.</param>
        public BinaryHeap(IEnumerable<T> collection)
            : this(collection, Comparer<T>.Default)
        {
        }

        /// <summary>
        /// Construct empty binary heap with custom element comparer.
        ///
        /// Time complexity: O(1).
        ///
        /// Space complexity: O(1).
        /// </summary>
        /// <param name="comparer">
        /// The comparer used to determine the element order in the heap.
        /// </param>
        public BinaryHeap(IComparer<T> comparer)
            : this(Array.Empty<T>(), comparer)
        {
        }

        /// <summary>
        /// Construct binary heap with custom element comparer from the given collection.
        ///
        /// Time complexity: O(N).
        ///
        /// Space complexity: O(N).
        ///
        /// N is size of the collection.
        /// </summary>
        /// <param name="collection">The elements collection to build the heap from.</param>
        /// <param name="comparer">
        /// The comparer used to determine the element order in the heap.
        /// </param>
        public BinaryHeap(IEnumerable<T> collection, IComparer<T> comparer)
            : this(collection.ToList(), comparer)
        {
        }

        private BinaryHeap(List<T> collection, IComparer<T> comparer)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            this.Items = collection;
            this.Comparer = comparer;

            this.BuildHeap();
        }

        /// <summary>
        /// Return the amount of elements in the heap.
        ///
        /// Time complexity: O(1).
        ///
        /// Space complexity: O(1).
        /// </summary>
        /// <value>Return non-negative number of elements.</value>
        public int Count => this.Items.Count;

        /// <summary>
        /// Determine whether the heap is empty.
        ///
        /// Time complexity: O(1).
        ///
        /// Space complexity: O(1).
        /// </summary>
        /// <value>Return true if the heap is empty, false otherwise.</value>
        public bool IsEmpty => this.Count == 0;

        private IComparer<T> Comparer { get; }

        private List<T> Items { get; }

        /// <summary>
        /// Return minimal element from the heap without any modification.
        ///
        /// Time complexity: O(1).
        ///
        /// Space complexity: O(1).
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the heap is empty.
        /// </exception>
        /// <returns>Return minimal element.</returns>
        public T Min()
        {
            this.EnsureHeapIsNotEmpty();

            return this.Items[0];
        }

        /// <summary>
        /// Extract minimal element from the heap preserving binary heap invariant.
        ///
        /// Time complexity: O(log(N)).
        ///
        /// Space complexity: O(1).
        ///
        /// N is size of the heap.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the heap is empty.
        /// </exception>
        /// <returns>Return extracted minimal element.</returns>
        public T ExtractMin()
        {
            this.EnsureHeapIsNotEmpty();

            var minValue = this.Min();

            var indexOfLast = this.Count - 1;
            this.Items[0] = this.Items[indexOfLast];
            this.Items.RemoveAt(indexOfLast);
            this.SiftDown(0);

            return minValue;
        }

        /// <summary>
        /// Insert element into the heap preserving binary heap invariant.
        ///
        /// Time complexity: O(log(N)).
        ///
        /// Space complexity: O(1).
        ///
        /// N is size of the heap.
        /// </summary>
        /// <param name="item">New item to add.</param>
        public void Add(T item)
        {
            this.Items.Add(item);
            this.SiftUp(this.Count - 1);
        }

        /// <summary>
        /// Merge two heaps into first one.
        /// Both heaps will be modified.
        ///
        /// Time complexity: O(N * lon(M + N)).
        ///
        /// Space complexity: O(N).
        ///
        /// M and N are sizes of both heaps.
        /// </summary>
        /// <param name="other">Another heap to merge from.</param>
        public void Meld(BinaryHeap<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            while (!other.IsEmpty)
            {
                this.Add(other.ExtractMin());
            }
        }

        /// <summary>
        /// Merge two heaps and return new one.
        /// Initial heaps will not be modified.
        ///
        /// Time complexity: O(M + N).
        ///
        /// Space complexity: O(M + N).
        ///
        /// M and N are sizes of both heaps.
        /// </summary>
        /// <param name="other">Another heap to merge from.</param>
        /// <returns>Return new binary heap containing elements from both heaps.</returns>
        public BinaryHeap<T> Merge(BinaryHeap<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            List<T> elements = new List<T>(this.Count + other.Count);
            foreach (var item in this.Items)
            {
                elements.Add(item);
            }

            foreach (var item in other.Items)
            {
                elements.Add(item);
            }

            return new BinaryHeap<T>(elements, this.Comparer);
        }

        /// <summary>
        /// Remove all elements from the heap.
        /// </summary>
        public void Clear() => this.Items.Clear();

        private static int ParentOf(int childIndex) =>
            (childIndex - 1) / 2;

        private static int LeftChildOf(int parentIndex) =>
            (2 * parentIndex) + 1;

        private static int RightChildOf(int parentIndex) =>
            (2 * parentIndex) + 2;

        private void EnsureHeapIsNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Heap is empty.");
            }
        }

        /// <summary>
        /// Efficiently build binary heap.
        ///
        /// Time complexity: O(N).
        ///
        /// Space complexity: O(1).
        ///
        /// N is the size of the internal collection.
        /// </summary>
        private void BuildHeap()
        {
            for (var idx = (this.Items.Count / 2) - 1; idx >= 0; idx--)
            {
                this.SiftDown(idx);
            }
        }

        private void SiftDown(int parentIndex)
        {
            int leftChild = LeftChildOf(parentIndex),
                rightChild = RightChildOf(parentIndex);

            while (leftChild < this.Items.Count)
            {
                var destIndex = leftChild;
                if (rightChild < this.Items.Count && this.ItemIsLess(rightChild, leftChild))
                {
                    destIndex = rightChild;
                }

                if (!this.ItemIsLess(destIndex, parentIndex))
                {
                    break;
                }

                this.SwapItems(parentIndex, destIndex);

                parentIndex = destIndex;

                leftChild = LeftChildOf(parentIndex);
                rightChild = RightChildOf(parentIndex);
            }
        }

        private void SiftUp(int childIndex)
        {
            var parentIndex = ParentOf(childIndex);

            while (this.ItemIsLess(childIndex, parentIndex))
            {
                this.SwapItems(childIndex, parentIndex);

                childIndex = parentIndex;
                parentIndex = ParentOf(childIndex);
            }
        }

        private bool ItemIsLess(int first, int second) =>
            this.Comparer.Compare(this.Items[first], this.Items[second]) < 0;

        private void SwapItems(int first, int second)
        {
            var temp = this.Items[first];
            this.Items[first] = this.Items[second];
            this.Items[second] = temp;
        }
    }
}
