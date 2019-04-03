namespace DataStructures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BinaryHeap<T>
    {
        public BinaryHeap()
            : this(Array.Empty<T>(), Comparer<T>.Default)
        {
        }

        public BinaryHeap(IEnumerable<T> collection)
            : this(collection, Comparer<T>.Default)
        {
        }

        public BinaryHeap(IComparer<T> comparer)
            : this(Array.Empty<T>(), comparer)
        {
        }

        public BinaryHeap(IEnumerable<T> collection, IComparer<T> comparer)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            this.Items = collection.ToList();
            this.Comparer = comparer;
            this.BuildHeap();
        }

        public int Count { get => this.Items.Count; }

        public bool IsEmpty { get => this.Count == 0; }

        private IComparer<T> Comparer { get; }

        private List<T> Items { get; }

        public T MinValue()
        {
            this.EnsureHeapIsNotEmpty();

            return this.Items[0];
        }

        public T ExtractMinValue()
        {
            this.EnsureHeapIsNotEmpty();

            var minValue = this.MinValue();

            this.Items[0] = this.Items[this.Count - 1];
            this.Items.RemoveAt(this.Count - 1);
            this.SiftDown(0);

            return minValue;
        }

        public BinaryHeap<T> Add(T item)
        {
            this.Items.Add(item);
            this.SiftUp(this.Count - 1);

            return this;
        }

        public BinaryHeap<T> Clear()
        {
            this.Items.Clear();

            return this;
        }

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

        private void BuildHeap()
        {
            for (var idx = (this.Items.Count / 2) - 1; idx >= 0; idx--)
            {
                this.SiftDown(idx);
            }
        }

        private void SiftDown(int index)
        {
            int leftChild = LeftChildOf(index),
                rightChild = RightChildOf(index);

            while (leftChild < this.Items.Count)
            {
                var destIndex = leftChild;
                if (rightChild < this.Items.Count && this.ItemIsLess(rightChild, leftChild))
                {
                    destIndex = rightChild;
                }

                if (!this.ItemIsLess(destIndex, index))
                {
                    break;
                }

                this.SwapItems(index, destIndex);

                index = destIndex;
                leftChild = LeftChildOf(index);
                rightChild = RightChildOf(index);
            }
        }

        private void SiftUp(int index)
        {
            var parentIndex = ParentOf(index);

            while (parentIndex >= 0 && !this.IsInvariantPreserved(index, parentIndex))
            {
                this.SwapItems(index, parentIndex);

                index = parentIndex;
                parentIndex = ParentOf(index);
            }
        }

        private bool IsInvariantPreserved(int childIndex, int parentIndex) =>
            this.ItemIsLess(parentIndex, childIndex);

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
