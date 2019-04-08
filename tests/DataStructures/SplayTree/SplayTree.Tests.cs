namespace DataStructures.Tests
{
    using System;
    using System.Linq;
    using DataStructures;
    using Xunit;

    public class EmptySplayTree
    {
        private SplayTree<int> splayTree;

        public EmptySplayTree()
        {
            this.splayTree = new SplayTree<int>();
        }

        [Fact]
        public void Count_ShouldReturnZero()
        {
            Assert.Equal(0, this.splayTree.Count);
        }

        [Fact]
        public void IsEmpty_ShouldReturnTrue()
        {
            Assert.True(this.splayTree.IsEmpty);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(100)]
        public void Contains_ShouldReturnFalseForAnyValue(int value)
        {
            Assert.False(this.splayTree.Contains(value));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(100)]
        public void Remove_ShouldNotChangeCount(int value)
        {
            this.splayTree.Remove(value);

            Assert.Equal(0, this.splayTree.Count);
        }

        [Fact]
        public void Min_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => this.splayTree.Min());
        }

        [Fact]
        public void Max_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => this.splayTree.Max());
        }

        [Fact]
        public void TraverseInOrder_ShouldReturnEmptyCollection()
        {
            Assert.Empty(this.splayTree.TraverseInOrder());
        }

        [Fact]
        public void TraversePreOrder_ShouldReturnEmptyCollection()
        {
            Assert.Empty(this.splayTree.TraversePreOrder());
        }

        [Fact]
        public void TraversePostOrder_ShouldReturnEmptyCollection()
        {
            Assert.Empty(this.splayTree.TraversePostOrder());
        }
    }

    public class SplayTreeWithOneValue
    {
        private SplayTree<int> splayTree;

        private int addedValue = 42;

        public SplayTreeWithOneValue()
        {
            this.splayTree = new SplayTree<int>();
            this.splayTree.Add(this.addedValue);
        }

        [Fact]
        public void Count_ShouldReturnOne()
        {
            Assert.Equal(1, this.splayTree.Count);
        }

        [Fact]
        public void IsEmpty_ShouldReturnFalse()
        {
            Assert.False(this.splayTree.IsEmpty);
        }

        [Fact]
        public void Contains_ShouldReturnTrueForAddedValue()
        {
            Assert.True(this.splayTree.Contains(this.addedValue));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-100)]
        [InlineData(43)]
        public void Contains_ShouldReturnFalseForNonAddedValues(int value)
        {
            Assert.False(this.splayTree.Contains(value));
        }

        [Fact]
        public void Add_ShouldNotChangeSizeIfAddDuplicate()
        {
            this.splayTree.Add(this.addedValue);

            Assert.Equal(1, this.splayTree.Count);
        }

        [Fact]
        public void Remove_ShouldMakeCountEqualZeroAfterDeleteAddedValue()
        {
            this.splayTree.Remove(this.addedValue);

            Assert.Equal(0, this.splayTree.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-100)]
        [InlineData(43)]
        public void Remove_ShouldNotChangeCountAfterDeleteNotAddedValue(int value)
        {
            this.splayTree.Remove(value);

            Assert.Equal(1, this.splayTree.Count);
        }

        [Fact]
        public void Min_ShouldReturnAddedValue()
        {
            Assert.Equal(this.addedValue, this.splayTree.Min());
        }

        [Fact]
        public void Max_ShouldReturnAddedValue()
        {
            Assert.Equal(this.addedValue, this.splayTree.Max());
        }

        [Fact]
        public void TraverseInOrder_ShouldReturnCollectionWithOneElement()
        {
            Assert.Single(this.splayTree.TraverseInOrder());
        }

        [Fact]
        public void TraverseInOrder_ShouldReturnCollectionWithAddedElement()
        {
            Assert.Contains(this.addedValue, this.splayTree.TraverseInOrder());
        }

        [Fact]
        public void TraversePreOrder_ShouldReturnCollectionWithOneElement()
        {
            Assert.Single(this.splayTree.TraversePreOrder());
        }

        [Fact]
        public void TraversePreOrder_ShouldReturnCollectionWithAddedElement()
        {
            Assert.Contains(this.addedValue, this.splayTree.TraversePreOrder());
        }

        [Fact]
        public void TraversePostOrder_ShouldReturnCollectionWithOneElement()
        {
            Assert.Single(this.splayTree.TraversePostOrder());
        }

        [Fact]
        public void TraversePostOrder_ShouldReturnCollectionWithAddedElement()
        {
            Assert.Contains(this.addedValue, this.splayTree.TraversePostOrder());
        }
    }

    public class SplayTreeWithManyPredefinedValues
    {
        private SplayTree<int> splayTree;

        private int[] addedValues = new int[] { 4, 100, -1, 18, 6, -47, 0, 25 };

        public SplayTreeWithManyPredefinedValues()
        {
            this.splayTree = new SplayTree<int>(this.addedValues);
        }

        [Fact]
        public void Count_ShouldReturnEight()
        {
            Assert.Equal(8, this.splayTree.Count);
        }

        [Fact]
        public void IsEmpty_ShouldBeFalse()
        {
            Assert.False(this.splayTree.IsEmpty);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(25)]
        [InlineData(18)]
        public void Add_ShouldNotChangeCountAfterAddExistingValue(int value)
        {
            this.splayTree.Add(value);

            Assert.Equal(8, this.splayTree.Count);
        }

        [Theory]
        [InlineData(-4)]
        [InlineData(19)]
        [InlineData(5)]
        [InlineData(-1000)]
        public void Add_ShouldIncreaseCountByOneAfterAddNonExistingValue(int value)
        {
            this.splayTree.Add(value);

            Assert.Equal(9, this.splayTree.Count);
        }

        [Fact]
        public void Contains_ShouldBeTrueForExistingValue()
        {
            Assert.All(this.addedValues, value =>
                this.splayTree.Contains(value));
        }

        [Theory]
        [InlineData(-4)]
        [InlineData(19)]
        [InlineData(5)]
        [InlineData(-1000)]
        public void Contains_ShouldBeFalseForNonExistingValue(int value)
        {
            Assert.False(this.splayTree.Contains(value));
        }

        [Fact]
        public void Min_ShouldReturnMinValueOfAllAddedValues()
        {
            Assert.Equal(-47, this.splayTree.Min());
        }

        [Fact]
        public void Max_ShouldReturnMinValueOfAllAddedValues()
        {
            Assert.Equal(100, this.splayTree.Max());
        }

        [Fact]
        public void TraverseInOrder_ShouldReturnCollectionOfSizeEight()
        {
            var inOrder = this.splayTree.TraverseInOrder().ToList();

            Assert.Equal(8, inOrder.Count);
        }

        [Fact]
        public void TraverseInOrder_ShouldReturnAllAddedElements()
        {
            Assert.All(this.splayTree.TraverseInOrder(), v =>
                this.addedValues.Contains(v));
        }

        [Fact]
        public void TraverseInOrder_ShouldReturnSortedCollection()
        {
            Array.Sort(this.addedValues);

            Assert.Equal(this.addedValues, this.splayTree.TraverseInOrder());
        }

        [Fact]
        public void TraversePreOrder_ShouldReturnCollectionOfSizeEight()
        {
            var preOrder = this.splayTree.TraversePreOrder().ToList();

            Assert.Equal(8, preOrder.Count);
        }

        [Fact]
        public void TraversePreOrder_ShouldReturnAllAddedElements()
        {
            Assert.All(this.splayTree.TraversePreOrder(), v =>
                this.addedValues.Contains(v));
        }

        [Fact]
        public void TraversePreOrder_ShouldReturnCollectionInPreOrder()
        {
            var preOrder = new int[] { 25, 4, 0, -1, -47, 18, 6, 100 };

            Assert.Equal(preOrder, this.splayTree.TraversePreOrder());
        }

        [Fact]
        public void TraversePostOrder_ShouldReturnCollectionOfSizeEight()
        {
            var postOrder = this.splayTree.TraversePostOrder().ToList();

            Assert.Equal(8, postOrder.Count);
        }

        [Fact]
        public void TraversePostOrder_ShouldReturnAllAddedElements()
        {
            Assert.All(this.splayTree.TraversePostOrder(), v =>
                this.addedValues.Contains(v));
        }

        [Fact]
        public void TraversePostOrder_ShouldReturnCollectionInPostOrder()
        {
            var postOrder = new int[] { -47, -1, 0, 6, 18, 4, 100, 25 };

            Assert.Equal(postOrder, this.splayTree.TraversePostOrder());
        }
    }
}
