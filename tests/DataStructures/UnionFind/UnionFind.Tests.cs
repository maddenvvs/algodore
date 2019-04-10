namespace DataStructures.Tests
{
    using System;
    using DataStructures;
    using Xunit;

    public class EmptyUnionFind
    {
        private UnionFind<int> uf;

        public EmptyUnionFind()
        {
            this.uf = new UnionFind<int>();
        }

        [Fact]
        public void Count_ShouldBeEqualToZero()
        {
            Assert.Equal(0, this.uf.Count);
        }

        [Fact]
        public void DisjointSetsCount_ShouldBeEqualToZero()
        {
            Assert.Equal(0, this.uf.DisjointSetsCount);
        }

        [Fact]
        public void Find_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => this.uf.Find(1));
        }

        [Fact]
        public void SizeOf_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => this.uf.SizeOf(1));
        }

        [Fact]
        public void Union_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => this.uf.Union(1, 2));
        }

        [Fact]
        public void MakeSet_ShouldMakeCountEqualToOne()
        {
            this.uf.MakeSet(1);

            Assert.Equal(1, this.uf.Count);
        }

        [Fact]
        public void MakeSet_ShouldMakeNumberOfDisjointSetsEqualToOne()
        {
            this.uf.MakeSet(1);

            Assert.Equal(1, this.uf.DisjointSetsCount);
        }
    }

    public class UnionFindWithOneElement
    {
        private UnionFind<int> uf;

        private int addedElement = 42;

        public UnionFindWithOneElement()
        {
            this.uf = new UnionFind<int>();
            this.uf.MakeSet(this.addedElement);
        }

        [Fact]
        public void Count_ShouldReturnOne()
        {
            Assert.Equal(1, this.uf.Count);
        }

        [Fact]
        public void DisjointSetsCount_ShouldBeEqualTo1()
        {
            Assert.Equal(1, this.uf.DisjointSetsCount);
        }

        [Fact]
        public void Find_ShouldReturnAddedElement()
        {
            Assert.Equal(this.addedElement, this.uf.Find(this.addedElement));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(1000)]
        [InlineData(-1000)]
        public void Find_ShouldThrowForNonExistingElements(int element)
        {
            Assert.Throws<ArgumentException>(() => this.uf.Find(element));
        }

        [Fact]
        public void SizeOf_ShouldReturnOneForAddedElement()
        {
            Assert.Equal(1, this.uf.SizeOf(this.addedElement));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(1000)]
        [InlineData(-1000)]
        public void SizeOf_ShouldThrowForNonExistingElements(int element)
        {
            Assert.Throws<ArgumentException>(() => this.uf.SizeOf(element));
        }

        [Fact]
        public void Union_ShouldReturnFalseWhenUnionAddedElementWithItself()
        {
            Assert.False(this.uf.Union(this.addedElement, this.addedElement));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(1000)]
        [InlineData(-1000)]
        public void Union_ShouldThrowForNonExistingElements(int element)
        {
            Assert.Throws<ArgumentException>(() =>
                this.uf.Union(this.addedElement, element));
        }

        [Fact]
        public void MakeSet_ShouldThrowWhenAddingExistingElement()
        {
            Assert.Throws<ArgumentException>(() =>
                this.uf.MakeSet(this.addedElement));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(1000)]
        [InlineData(-1000)]
        public void MakeSet_ShouldChangeCountTo2ForNonAddedElement(int element)
        {
            this.uf.MakeSet(element);

            Assert.Equal(2, this.uf.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(1000)]
        [InlineData(-1000)]
        public void MakeSet_DisjointSetsCountEquals2ForNonAddedElement(int element)
        {
            this.uf.MakeSet(element);

            Assert.Equal(2, this.uf.DisjointSetsCount);
        }
    }

    public class UnionFindWithManyElements
    {
        private UnionFind<int> uf;

        private int[] addedElements = new int[] { 17, 4, -1, 0, 19, -27 };

        public UnionFindWithManyElements()
        {
            this.uf = new UnionFind<int>(this.addedElements);
        }

        [Fact]
        public void Count_ShouldBeEqualToSix()
        {
            Assert.Equal(6, this.uf.Count);
        }

        [Fact]
        public void DisjointSetsCount_ShouldBeEqualToSix()
        {
            Assert.Equal(6, this.uf.DisjointSetsCount);
        }

        [Theory]
        [InlineData(17)]
        [InlineData(4)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(19)]
        [InlineData(-27)]
        public void Find_ShouldReturnItselfForExistingElement(int element)
        {
            Assert.Equal(element, this.uf.Find(element));
        }

        [Theory]
        [InlineData(100)]
        [InlineData(-99)]
        [InlineData(5)]
        public void Find_ShouldThrowForNonExistingElement(int element)
        {
            Assert.Throws<ArgumentException>(() => this.uf.Find(element));
        }

        [Theory]
        [InlineData(17)]
        [InlineData(4)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(19)]
        [InlineData(-27)]
        public void SizeOf_ShouldReturnOneForExistingElement(int element)
        {
            Assert.Equal(1, this.uf.SizeOf(element));
        }

        [Theory]
        [InlineData(100)]
        [InlineData(-99)]
        [InlineData(5)]
        public void SizeOf_ShouldThrowForNonExistingElement(int element)
        {
            Assert.Throws<ArgumentException>(() => this.uf.SizeOf(element));
        }

        [Theory]
        [InlineData(17)]
        [InlineData(4)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(19)]
        [InlineData(-27)]
        public void MakeSet_ShouldThrowForExistingElement(int element)
        {
            Assert.Throws<ArgumentException>(() => this.uf.MakeSet(element));
        }

        [Theory]
        [InlineData(100)]
        [InlineData(-99)]
        [InlineData(5)]
        public void MakeSet_CountEqualsSevenForNonExistingElement(int element)
        {
            this.uf.MakeSet(element);

            Assert.Equal(7, this.uf.Count);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(-99)]
        [InlineData(5)]
        public void MakeSet_DisjointSetsCountToSevenForNonExistingElement(int element)
        {
            this.uf.MakeSet(element);

            Assert.Equal(7, this.uf.DisjointSetsCount);
        }

        [Theory]
        [InlineData(new object[] { 111, -1 })]
        [InlineData(new object[] { 0, 5 })]
        [InlineData(new object[] { 19, 7 })]
        [InlineData(new object[] { 5, -27 })]
        public void Union_ShouldThrowWhenUnionNonExistingElement(int l, int r)
        {
            Assert.Throws<ArgumentException>(() => this.uf.Union(l, r));
        }

        [Theory]
        [InlineData(new object[] { 4, -1 })]
        [InlineData(new object[] { 0, 19 })]
        [InlineData(new object[] { 19, 17 })]
        [InlineData(new object[] { -1, -27 })]
        public void Union_DisjointSetsCountDecreasedByOne(int l, int r)
        {
            this.uf.Union(l, r);

            Assert.Equal(5, this.uf.DisjointSetsCount);
        }

        [Theory]
        [InlineData(new object[] { 4, -1 })]
        [InlineData(new object[] { 0, 19 })]
        [InlineData(new object[] { 19, 17 })]
        [InlineData(new object[] { -1, -27 })]
        public void Find_ReturnSameRootForTwoElementsFromTheSameSet(int l, int r)
        {
            this.uf.Union(l, r);

            Assert.Equal(this.uf.Find(l), this.uf.Find(r));
        }

        [Theory]
        [InlineData(new object[] { 4, -1 })]
        [InlineData(new object[] { 0, 19 })]
        [InlineData(new object[] { 19, 17 })]
        [InlineData(new object[] { -1, -27 })]
        public void SizeOf_ReturnTwoAfterUnionTwoSeparatedElements(int l, int r)
        {
            this.uf.Union(l, r);

            Assert.Equal(2, this.uf.SizeOf(l));
            Assert.Equal(2, this.uf.SizeOf(r));
        }

        [Theory]
        [InlineData(new object[] { 4, -1, 0 })]
        [InlineData(new object[] { 0, 19, 17 })]
        [InlineData(new object[] { 19, 17, -1 })]
        [InlineData(new object[] { 4, -1, -27 })]
        public void Find_ReturnSameRootForThreeElementsFromTheSameSet(
            int a,
            int b,
            int c)
        {
            this.uf.Union(a, b);
            this.uf.Union(c, b);

            Assert.Equal(this.uf.Find(a), this.uf.Find(b));
            Assert.Equal(this.uf.Find(b), this.uf.Find(c));
            Assert.Equal(this.uf.Find(c), this.uf.Find(a));
        }

        [Theory]
        [InlineData(new object[] { 4, -1, 0 })]
        [InlineData(new object[] { 0, 19, 17 })]
        [InlineData(new object[] { 19, 17, -1 })]
        [InlineData(new object[] { 4, -1, -27 })]
        public void SizeOf_Return3AfterUnionThreeSeparatedElements(
            int a,
            int b,
            int c)
        {
            this.uf.Union(a, b);
            this.uf.Union(c, b);

            Assert.Equal(3, this.uf.SizeOf(a));
            Assert.Equal(3, this.uf.SizeOf(b));
            Assert.Equal(3, this.uf.SizeOf(c));
        }
    }
}
