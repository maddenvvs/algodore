using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    public class BinaryHeap<T>
    {
        public BinaryHeap() : this(Array.Empty<T>(), Comparer<T>.Default) { }

        public BinaryHeap(IEnumerable<T> collection) : this(collection, Comparer<T>.Default) { }

        public BinaryHeap(IComparer<T> comparer) : this(Array.Empty<T>(), comparer) { }

        public BinaryHeap(IEnumerable<T> collection, IComparer<T> comparer)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));

            Items = collection.ToList();
            Comparer = comparer;
            buildHeap();
        }

        private IComparer<T> Comparer { get; }

        private List<T> Items { get; }

        public int Count { get; } => Items.Count;

        public bool IsEmpty { get; } => Count == 0;

        public T MinValue()
        {
            ensureHeapIsNotEmpty();

            return Items[0];
        }

        public T ExtractMinValue()
        {
            ensureHeapIsNotEmpty();

            var minValue = MinValue();

            Items[0] = Items[Count - 1];
            Items.RemoveAt(Count - 1);
            siftDown(0);

            return minValue;
        }

        public BinaryHeap<T> Add(T item)
        {
            Items.Add(item);
            siftUp(Count - 1);

            return this;
        }

        public BinaryHeap<T> Clear()
        {
            Items.Clear();

            return this;
        }

        private void ensureHeapIsNotEmpty()
        {
            if (Count == 0) throw new InvalidOperationException("Heap is empty.");
        }

        private void buildHeap()
        {
            for (var idx = Items.Count / 2 - 1; idx >= 0; idx--)
            {
                siftDown(idx);
            }
        }

        private void siftDown(int index)
        {
            var leftChild = leftChildOf(index),
                rightChild = rightChildOf(index);

            while (leftChild < Items.Count)
            {
                var destIndex = leftChild;
                if (rightChild < Items.Count && itemIsLess(rightChild, leftChild))
                {
                    destIndex = rightChild;
                }

                if (!itemIsLess(destIndex, index))
                    break;

                swapItems(index, destIndex);

                index = destIndex;
                leftChild = leftChildOf(index);
                rightChild = rightChildOf(index);
            }
        }

        private void siftUp(int index)
        {
            var parentIndex = parentOf(index);

            while (parentIndex >= 0 && !isInvariantPreserved(index, parentIndex))
            {
                swapItems(index, parentIndex);

                index = parentIndex;
                parentIndex = parentOf(index);
            }
        }

        private bool isInvariantPreserved(int childIndex, int parentIndex) =>
            itemIsLess(parentIndex, childIndex);

        private bool itemIsLess(int first, int second) =>
            Comparer.Compare(Items[first], Items[second]) < 0;

        private void swapItems(int first, int second)
        {
            var temp = Items[first];
            Items[first] = Items[second];
            Items[second] = temp;
        }

        private static int parentOf(int childIndex) => (childIndex - 1) / 2;

        private static int leftChildOf(int parentIndex) => 2 * parentIndex + 1;

        private static int rightChildOf(int parentIndex) => 2 * parentIndex + 2;
    }
}