using System;
using Xunit;

using DataStructures;

namespace DataStructures.Tests
{
    public class EmptySinglyLinkedList
    {
        SinglyLinkedList<int> _linkedList;

        public EmptySinglyLinkedList()
        {
            _linkedList = new SinglyLinkedList<int>();
        }

        [Fact]
        public void Count_ShouldReturnZero()
        {
            Assert.Equal(0, _linkedList.Count);
        }

        [Fact]
        public void IsEmpty_ShouldReturnTrue()
        {
            Assert.True(_linkedList.IsEmpty);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(100)]
        public void GetAt_ShouldThrowException(int index)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => _linkedList.GetAt(index));
        }

        [Fact]
        public void RemoveFirst_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(
                () => _linkedList.RemoveFirst());
        }

        [Fact]
        public void RemoveLast_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(
                () => _linkedList.RemoveLast());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(100)]
        public void RemoveAt_ShouldThrowException(int index)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => _linkedList.RemoveAt(index));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(100)]
        [InlineData(-100)]
        [InlineData(0)]
        public void IndexOf_ShouldReturnMinusOne(int value)
        {
            Assert.Equal(-1, _linkedList.IndexOf(value));
        }

        [Fact]
        public void Clear_ShouldModifyInPlace()
        {
            Assert.Equal(_linkedList, _linkedList.Clear());
        }
    }

    public class SinglyLinkedListWithOneElement
    {
        SinglyLinkedList<int> _linkedList;
        int _addedElement = 42;

        public SinglyLinkedListWithOneElement()
        {
            _linkedList = new SinglyLinkedList<int>();
            _linkedList.Append(_addedElement);
        }

        [Fact]
        public void Count_ShouldReturnOne()
        {
            Assert.Equal(1, _linkedList.Count);
        }

        [Fact]
        public void IsEmpty_ShouldReturnFalse()
        {
            Assert.False(_linkedList.IsEmpty);
        }

        [Fact]
        public void RemoveFirst_ShouldMakeLinkedListEmpty()
        {
            _linkedList.RemoveFirst();

            Assert.True(_linkedList.IsEmpty);
            Assert.Equal(0, _linkedList.Count);
        }

        [Fact]
        public void RemoveLast_ShouldMakeLinkedListEmpty()
        {
            _linkedList.RemoveLast();

            Assert.True(_linkedList.IsEmpty);
            Assert.Equal(0, _linkedList.Count);
        }

        [Fact]
        public void RemoveAt_ShouldMakeLinkedListEmpty()
        {
            _linkedList.RemoveAt(0);

            Assert.True(_linkedList.IsEmpty);
            Assert.Equal(0, _linkedList.Count);
        }

        [Fact]
        public void GetAt_ShouldReturnAddedElement()
        {
            Assert.Equal(_addedElement, _linkedList.GetAt(0));
        }
    }
}
