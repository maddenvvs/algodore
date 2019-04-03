namespace DataStructures.Tests
{
    using System;

    using DataStructures;
    using Xunit;

    public class EmptySinglyLinkedList
    {
        private SinglyLinkedList<int> linkedList;

        public EmptySinglyLinkedList()
        {
            this.linkedList = new SinglyLinkedList<int>();
        }

        [Fact]
        public void Count_ShouldReturnZero()
        {
            Assert.Equal(0, this.linkedList.Count);
        }

        [Fact]
        public void IsEmpty_ShouldReturnTrue()
        {
            Assert.True(this.linkedList.IsEmpty);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(100)]
        public void GetAt_ShouldThrowException(int index)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => this.linkedList.GetAt(index));
        }

        [Fact]
        public void RemoveFirst_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(
                () => this.linkedList.RemoveFirst());
        }

        [Fact]
        public void RemoveLast_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(
                () => this.linkedList.RemoveLast());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(100)]
        public void RemoveAt_ShouldThrowException(int index)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => this.linkedList.RemoveAt(index));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(100)]
        [InlineData(-100)]
        [InlineData(0)]
        public void IndexOf_ShouldReturnMinusOne(int value)
        {
            Assert.Equal(-1, this.linkedList.IndexOf(value));
        }

        [Fact]
        public void Clear_ShouldModifyInPlace()
        {
            Assert.Equal(this.linkedList, this.linkedList.Clear());
        }
    }

    public class SinglyLinkedListWithOneElement
    {
        private SinglyLinkedList<int> linkedList;
        private int addedElement = 42;

        public SinglyLinkedListWithOneElement()
        {
            this.linkedList = new SinglyLinkedList<int>();
            this.linkedList.Append(this.addedElement);
        }

        [Fact]
        public void Count_ShouldReturnOne()
        {
            Assert.Equal(1, this.linkedList.Count);
        }

        [Fact]
        public void IsEmpty_ShouldReturnFalse()
        {
            Assert.False(this.linkedList.IsEmpty);
        }

        [Fact]
        public void RemoveFirst_ShouldMakeLinkedListEmpty()
        {
            this.linkedList.RemoveFirst();

            Assert.True(this.linkedList.IsEmpty);
            Assert.Equal(0, this.linkedList.Count);
        }

        [Fact]
        public void RemoveLast_ShouldMakeLinkedListEmpty()
        {
            this.linkedList.RemoveLast();

            Assert.True(this.linkedList.IsEmpty);
            Assert.Equal(0, this.linkedList.Count);
        }

        [Fact]
        public void RemoveAt_ShouldMakeLinkedListEmpty()
        {
            this.linkedList.RemoveAt(0);

            Assert.True(this.linkedList.IsEmpty);
            Assert.Equal(0, this.linkedList.Count);
        }

        [Fact]
        public void GetAt_ShouldReturnAddedElement()
        {
            Assert.Equal(this.addedElement, this.linkedList.GetAt(0));
        }
    }
}
