namespace DataStructures.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataStructures;
    using Xunit;

    public class EmptyBinaryHeap
    {
        private BinaryHeap<int> heap;

        public EmptyBinaryHeap()
        {
            this.heap = new BinaryHeap<int>();
        }

        [Fact]
        public void Count_ShouldBeZero()
        {
            Assert.Equal(0, this.heap.Count);
        }

        [Fact]
        public void IsEmpty_ShouldBeTrue()
        {
            Assert.True(this.heap.IsEmpty);
        }

        [Fact]
        public void Min_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => this.heap.Min());
        }

        [Fact]
        public void ExtractMin_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => this.heap.ExtractMin());
        }
    }

    public class BinaryHeapWithOneElement
    {
        private BinaryHeap<int> heap;
        private int addedElement = 42;

        public BinaryHeapWithOneElement()
        {
            this.heap = new BinaryHeap<int>();
            this.heap.Add(this.addedElement);
        }

        [Fact]
        public void Count_ShouldBeOne()
        {
            Assert.Equal(1, this.heap.Count);
        }

        [Fact]
        public void IsEmpty_ShouldBeFalse()
        {
            Assert.False(this.heap.IsEmpty);
        }

        [Fact]
        public void Min_ShouldReturnAddedElement()
        {
            Assert.Equal(this.addedElement, this.heap.Min());
        }

        [Fact]
        public void ExtractMin_ShouldReturnAddedElement()
        {
            Assert.Equal(this.addedElement, this.heap.ExtractMin());
        }

        [Fact]
        public void ExtractMin_ShouldMakeCountEqualZero()
        {
            this.heap.ExtractMin();

            Assert.Equal(0, this.heap.Count);
        }

        [Fact]
        public void ExtractMin_ShouldMakeHeapEmpty()
        {
            this.heap.ExtractMin();

            Assert.True(this.heap.IsEmpty);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Add_ShouldReturnNewlyAddedElementAsMin(int element)
        {
            this.heap.Add(element);

            Assert.Equal(element, this.heap.Min());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Add_ShouldReturnNewlyAddedElementIfExtractMin(int element)
        {
            this.heap.Add(element);

            Assert.Equal(element, this.heap.ExtractMin());
        }

        [Theory]
        [InlineData(44)]
        [InlineData(100)]
        public void Add_ShouldReturnInitiallyAddedElementAsMin(int element)
        {
            this.heap.Add(element);

            Assert.Equal(this.addedElement, this.heap.Min());
        }

        [Theory]
        [InlineData(44)]
        [InlineData(100)]
        public void Add_ShouldReturnInitiallyAddedElementIfExtractMin(int element)
        {
            this.heap.Add(element);

            Assert.Equal(this.addedElement, this.heap.ExtractMin());
        }
    }

    public class BinaryHeapWithEightElements
    {
        private BinaryHeap<int> heap;

        private int[] elements = new int[] { 1, 10, -8, 7, 1000, 5, 1, 2 };

        public BinaryHeapWithEightElements()
        {
            this.heap = new BinaryHeap<int>(this.elements);
        }

        [Fact]
        public void Count_ShouldBeEqualToEight()
        {
            Assert.Equal(this.elements.Length, this.heap.Count);
        }

        [Fact]
        public void IsEmpty_ShouldBeFalse()
        {
            Assert.False(this.heap.IsEmpty);
        }

        [Fact]
        public void Min_ShouldBeEqualToMinusEight()
        {
            Assert.Equal(-8, this.heap.Min());
        }

        [Fact]
        public void ExtractMin_ShouldBeEqualToMinusEight()
        {
            Assert.Equal(-8, this.heap.ExtractMin());
        }

        [Fact]
        public void ExtractMin_EqualsToSortedArrayIfExtractAllElements()
        {
            Array.Sort(this.elements);
            var allExtractedElements = Enumerable.Range(0, 8)
                .Select((_) => this.heap.ExtractMin())
                .ToArray();

            Assert.Equal(this.elements, allExtractedElements);
        }

        [Fact]
        public void Clear_ShouldMakeHeapEmpty()
        {
            this.heap.Clear();

            Assert.True(this.heap.IsEmpty);
        }

        [Fact]
        public void Clear_ShouldMakeCountEqualZero()
        {
            this.heap.Clear();

            Assert.Equal(0, this.heap.Count);
        }
    }

    public class MergeHeaps
    {
        private BinaryHeap<int> firstHeap;
        private BinaryHeap<int> secondHeap;

        public MergeHeaps()
        {
            this.firstHeap = new BinaryHeap<int>(new int[] { 1, 2, 3, 4, 5 });
            this.secondHeap = new BinaryHeap<int>(new int[] { -1, 0, 1 });
        }

        [Fact]
        public void Meld_MutatesBothHeaps()
        {
            this.firstHeap.Meld(this.secondHeap);

            Assert.Equal(0, this.secondHeap.Count);
            Assert.Equal(8, this.firstHeap.Count);
        }

        [Fact]
        public void Meld_MeldedHeapReturnsMinElementOfBothHeaps()
        {
            this.firstHeap.Meld(this.secondHeap);

            Assert.Equal(-1, this.firstHeap.Min());
        }

        [Fact]
        public void Merge_DoesntMutateAnyHeap()
        {
            this.firstHeap.Merge(this.secondHeap);

            Assert.Equal(5, this.firstHeap.Count);
            Assert.Equal(3, this.secondHeap.Count);
        }

        [Fact]
        public void Merge_MergedHeapContainsElementsFromBothHeaps()
        {
            var newHeap = this.firstHeap.Merge(this.secondHeap);

            Assert.Equal(8, newHeap.Count);
        }

        [Fact]
        public void Merge_MergedHeapReturnsMinElementFromBothHeaps()
        {
            var newHeap = this.firstHeap.Merge(this.secondHeap);

            Assert.Equal(-1, newHeap.Min());
        }
    }
}
