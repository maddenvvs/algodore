namespace DataStructures.Tests
{
    using System;
    using System.Linq;
    using DataStructures;
    using Xunit;

    public class EmptyTrie
    {
        private Trie trie;

        public EmptyTrie()
        {
            this.trie = new Trie();
        }

        [Fact]
        public void IsEmpty_ShouldBeTrue()
        {
            Assert.True(this.trie.IsEmpty);
        }

        [Fact]
        public void Count_ShouldBeEqualZero()
        {
            Assert.Equal(0, this.trie.Count);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("b")]
        [InlineData("abasdwdqasd")]
        public void ContainsWord_ShouldReturnFalse(string word)
        {
            Assert.False(this.trie.ContainsWord(word));
        }

        [Theory]
        [InlineData("a")]
        [InlineData("b")]
        [InlineData("abasdwdqasd")]
        public void ContainsPrefix_ShouldReturnFalse(string word)
        {
            Assert.False(this.trie.ContainsPrefix(word));
        }

        [Theory]
        [InlineData("a")]
        [InlineData("b")]
        [InlineData("abasdwdqasd")]
        public void DeleteWord_ShouldThrowException(string word)
        {
            Assert.Throws<InvalidOperationException>(
                () => this.trie.DeleteWord(word));
        }
    }

    public class TrieWithOneWord
    {
        private Trie trie;
        private string wordToAdd = "word";

        public TrieWithOneWord()
        {
            this.trie = new Trie();
            this.trie.AddWord(this.wordToAdd);
        }

        [Fact]
        public void Count_ShouldBeOne()
        {
            Assert.Equal(1, this.trie.Count);
        }

        [Fact]
        public void IsEmpty_ShouldBeFalse()
        {
            Assert.False(this.trie.IsEmpty);
        }

        [Fact]
        public void ContainsWord_ShouldReturnTrueForAddedWord()
        {
            Assert.True(this.trie.ContainsWord(this.wordToAdd));
        }

        [Theory]
        [InlineData("w")]
        [InlineData("wo")]
        [InlineData("wor")]
        [InlineData("word")]
        public void ContainsPrefix_ShouldReturnTrueForAllAvailablePrefixes(string prefix)
        {
            Assert.True(this.trie.ContainsPrefix(prefix));
        }

        [Theory]
        [InlineData("w")]
        [InlineData("wo")]
        [InlineData("wor")]
        [InlineData("word")]
        public void FindWordWithPrefix_ShouldReturnOneWordForAvailablePrefixes(string prefix)
        {
            var words = this.trie.FindWordsWithPrefix(prefix).ToList();

            Assert.Single(words);
        }

        [Theory]
        [InlineData("w")]
        [InlineData("wo")]
        [InlineData("wor")]
        [InlineData("word")]
        public void FindWordWithPrefix_ShouldReturnAddedWord(string prefix)
        {
            var words = this.trie.FindWordsWithPrefix(prefix).ToList();

            Assert.Equal(this.wordToAdd, words[0]);
        }

        [Fact]
        public void DeleteWord_ShouldMakeTrieEmpty()
        {
            this.trie.DeleteWord(this.wordToAdd);

            Assert.True(this.trie.IsEmpty);
        }

        [Fact]
        public void DeleteWord_ShouldMakeCountEqualZero()
        {
            this.trie.DeleteWord(this.wordToAdd);

            Assert.Equal(0, this.trie.Count);
        }

        [Fact]
        public void FindAllWords_ShouldReturnOneWord()
        {
            var words = this.trie.FindAllWords().ToList();

            Assert.Single(words);
        }

        [Fact]
        public void FindAllWords_ShouldReturnAddedWord()
        {
            var words = this.trie.FindAllWords().ToList();

            Assert.Equal(this.wordToAdd, words[0]);
        }

        [Fact]
        public void AddWord_ShouldNotAddAlreadyAddedWord()
        {
            this.trie.AddWord(this.wordToAdd);

            Assert.Equal(1, this.trie.Count);
        }
    }

    public class TrieWithManyWords
    {
        private Trie trie;

        private string[] words = new string[] { "a", "b", "bbd", "bba", "zxcvb", "zxc", "zxcv" };

        public TrieWithManyWords()
        {
            this.trie = new Trie(this.words);
        }

        [Fact]
        public void Count_ShouldReturnSeven()
        {
            Assert.Equal(7, this.trie.Count);
        }

        [Fact]
        public void IsEmpty_ShouldBeFalse()
        {
            Assert.False(this.trie.IsEmpty);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("b")]
        [InlineData("bbd")]
        [InlineData("bba")]
        [InlineData("zxcv")]
        [InlineData("zxc")]
        [InlineData("zxcvb")]
        public void ContainsWord_ShouldReturnTrueForAddedWords(string word)
        {
            Assert.True(this.trie.ContainsWord(word));
        }

        [Theory]
        [InlineData("ab")]
        [InlineData("bb")]
        [InlineData("zx")]
        [InlineData("z")]
        [InlineData("zxcvbh")]
        [InlineData("f")]
        public void ContainsWord_ShouldReturnFalseForNotAddedWords(string word)
        {
            Assert.False(this.trie.ContainsWord(word));
        }

        [Theory]
        [InlineData("a")]
        [InlineData("bb")]
        [InlineData("zx")]
        [InlineData("zxcv")]
        [InlineData("zxcvb")]
        public void ContainsPrefix_ShouldReturnTrueForAvailablePrefixes(string prefix)
        {
            Assert.True(this.trie.ContainsPrefix(prefix));
        }

        [Theory]
        [InlineData("aa")]
        [InlineData("bbf")]
        [InlineData("y")]
        [InlineData("zxcvbb")]
        public void ContainsPrefix_ShouldReturnFalseForUnavailablePrefixes(string prefix)
        {
            Assert.False(this.trie.ContainsPrefix(prefix));
        }

        [Fact]
        public void FindAllWords_ShouldReturnSevenWords()
        {
            var words = this.trie.FindAllWords().ToList();

            Assert.Equal(7, words.Count);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("b")]
        [InlineData("bbd")]
        [InlineData("bba")]
        [InlineData("zxcv")]
        [InlineData("zxc")]
        [InlineData("zxcvb")]
        public void FindAllWords_ShouldReturnAllAddedWords(string word)
        {
            Assert.Contains(word, this.trie.FindAllWords());
        }

        [Theory]
        [InlineData(new object[] { "a", 1 })]
        [InlineData(new object[] { "b", 3 })]
        [InlineData(new object[] { "zx", 3 })]
        public void FindWordWithPrefix_ShouldWorkForAvailablePrefixes(string prefix, int wordsCount)
        {
            var words = this.trie.FindWordsWithPrefix(prefix).ToList();

            Assert.Equal(wordsCount, words.Count);
        }

        [Theory]
        [InlineData("ab")]
        [InlineData("bdeeq")]
        [InlineData("ul")]
        public void FindWordWithPrefix_ShouldReturnZeroForUnavailablePrefixes(string prefix)
        {
            Assert.Empty(this.trie.FindWordsWithPrefix(prefix));
        }

        [Theory]
        [InlineData("a")]
        [InlineData("b")]
        [InlineData("bbd")]
        [InlineData("bba")]
        [InlineData("zxcv")]
        [InlineData("zxc")]
        [InlineData("zxcvb")]
        public void DeleteWord_ShouldDeleteAddedWords(string word)
        {
            this.trie.DeleteWord(word);

            Assert.False(this.trie.ContainsWord(word));
        }

        [Theory]
        [InlineData("x")]
        [InlineData("bb")]
        [InlineData("cvb")]
        [InlineData("asdasdsadasdasdasdassad")]
        public void DeleteWord_ShouldThrowWhenDeleteNonAddedWords(string word)
        {
            Assert.Throws<InvalidOperationException>(() => this.trie.DeleteWord(word));
        }
    }
}
