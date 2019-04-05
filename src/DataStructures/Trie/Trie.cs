namespace DataStructures
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Trie (prefix tree) implementation.
    /// </summary>
    public class Trie
    {
        /// <summary>
        /// Creates empty trie.
        ///
        /// Time complexity: O(1).
        ///
        /// Space complexity: O(1).
        /// </summary>
        public Trie()
            : this(Array.Empty<string>())
        {
        }

        /// <summary>
        /// Creates empty trie and populates it with the given words collection.
        ///
        /// Time complexity: O(N * K).
        ///
        /// Space complexity: O(N * K).
        ///
        /// N - number of words, K - max length of word.
        /// </summary>
        /// <param name="words">The collection of words to be added to the trie.</param>
        public Trie(IEnumerable<string> words)
        {
            if (words == null)
            {
                throw new ArgumentNullException(nameof(words));
            }

            this.Root = new Node();
            this.Count = 0;

            foreach (var word in words)
            {
                this.AddWord(word);
            }
        }

        /// <summary>
        /// Gets words count stored in the trie.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Determines whether the trie is empty.
        /// </summary>
        public bool IsEmpty { get => this.Count == 0; }

        private Node Root { get; set; }

        /// <summary>
        /// Inserts given word in the trie.
        ///
        /// Time complexity: O(K).
        ///
        /// Space complexity: O(K).
        ///
        /// K - length of word.
        /// </summary>
        /// <param name="word">The word to add to the trie.</param>
        /// <returns>Returns modified trie with added word.</returns>
        public Trie AddWord(string word)
        {
            if (word == null)
            {
                throw new ArgumentNullException(nameof(word));
            }

            Node currentNode = this.Root, nextNode;

            foreach (var letter in word)
            {
                if (!currentNode.Children.TryGetValue(letter, out nextNode))
                {
                    nextNode = new Node(letter, currentNode);
                    currentNode.Children[letter] = nextNode;
                }

                currentNode = nextNode;
            }

            if (!currentNode.IsWord)
            {
                currentNode.IsWord = true;
                this.Count++;
            }

            return this;
        }

        /// <summary>
        /// Removes given word from the trie.
        ///
        /// Time complexity: O(K).
        ///
        /// Space complexity: O(1).
        ///
        /// K - length of word.
        /// </summary>
        /// <param name="word">The word to delete from the trie.</param>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if the trie doesn't contain the given word.
        /// </exception>
        /// <returns>
        /// Returns modified trie without deleted word.
        /// </returns>
        public Trie DeleteWord(string word)
        {
            if (word == null)
            {
                throw new ArgumentNullException(nameof(word));
            }

            var lastNode = this.FindLastNodeOfPrefix(word);

            if (!IsNodeMeansEndOfWord(lastNode))
            {
                throw new InvalidOperationException(
                    $"There is no word '{word}' in trie.");
            }

            var parentNode = lastNode.Parent;
            lastNode.IsWord = false;

            for (var idx = word.Length - 1; idx >= 0; idx--)
            {
                char letter = word[idx];

                if (lastNode.Children.Count > 0 || lastNode.IsWord)
                {
                    break;
                }

                parentNode.Children.Remove(letter);

                lastNode = parentNode;
                parentNode = parentNode.Parent;
            }

            this.Count--;

            return this;
        }

        /// <summary>
        /// Checks whether the trie contains the given word.
        ///
        /// Time complexity: O(K).
        ///
        /// Space complexity: O(1).
        ///
        /// K - length of word.
        /// </summary>
        /// <param name="word">The word to look for.</param>
        /// <returns>
        /// Returns true if the trie contains given word, otherwise false.
        /// </returns>
        public bool ContainsWord(string word)
        {
            if (word == null)
            {
                throw new ArgumentNullException(nameof(word));
            }

            var lastNode = this.FindLastNodeOfPrefix(word);

            return IsNodeMeansEndOfWord(lastNode);
        }

        /// <summary>
        /// Checks whether the trie contains the given prefix.
        ///
        /// Time complexity: O(K).
        ///
        /// Space complexity: O(1).
        ///
        /// K - length of word.
        /// </summary>
        /// <param name="prefix">The prefix to look for.</param>
        /// <returns>
        /// Returns true if the trie contains given prefix, otherwise false.
        /// </returns>
        public bool ContainsPrefix(string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            return this.FindLastNodeOfPrefix(prefix) != null;
        }

        /// <summary>
        /// Returns all words the trie contains.
        ///
        /// Time complexity: O(N * K).
        ///
        /// Space complexity: O(N * K).
        ///
        /// N - number of words, K - max length of word.
        /// </summary>
        /// <returns>
        /// Returns collection of all words in the trie.
        /// </returns>
        public IEnumerable<string> FindAllWords() =>
            this.FindWordsWithPrefix(string.Empty);

        /// <summary>
        /// Returns all words with given prefix the trie contains.
        ///
        /// Time complexity: O(N * K).
        ///
        /// Space complexity: O(N * K).
        ///
        /// N - number of words, K - max length of word.
        /// </summary>
        /// <param name="prefix">The prefix to look for.</param>
        /// <returns>Returns collection of founded words in the trie.</returns>
        public IEnumerable<string> FindWordsWithPrefix(string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(prefix);
            }

            var lastNode = this.FindLastNodeOfPrefix(prefix);

            return this.FindWordsContainingNode(lastNode, prefix);
        }

        /// <summary>
        /// Clears the trie.
        /// </summary>
        /// <returns>Returns modified empty trie.</returns>
        public Trie Clear()
        {
            this.Root = new Node();
            this.Count = 0;

            return this;
        }

        private static bool IsNodeMeansEndOfWord(Node node) =>
            node != null && node.IsWord;

        private Node FindLastNodeOfPrefix(string word)
        {
            Node currentNode = this.Root, nextNode;

            foreach (var letter in word)
            {
                if (!currentNode.Children.TryGetValue(letter, out nextNode))
                {
                    return null;
                }

                currentNode = nextNode;
            }

            return currentNode;
        }

        private IEnumerable<string> FindWordsContainingNode(Node node, string prefix)
        {
            if (node == null)
            {
                yield break;
            }

            if (node.IsWord)
            {
                yield return prefix;
            }

            var builder = new StringBuilder(prefix);
            var stack = new Stack<(int, char, Node)>();
            var lastVisitedLevel = 0;

            foreach (var pair in node.Children)
            {
                stack.Push((1, pair.Key, pair.Value));
            }

            while (stack.Count > 0)
            {
                (int level, char letter, Node currentNode) = stack.Pop();

                if (level > lastVisitedLevel)
                {
                    builder.Append(letter);
                }
                else
                {
                    var charsToRemove = lastVisitedLevel - level + 1;
                    builder.Remove(builder.Length - charsToRemove, charsToRemove);
                    builder.Append(letter);
                }

                lastVisitedLevel = level;

                if (currentNode.IsWord)
                {
                    yield return builder.ToString();
                }

                foreach (var pair in currentNode.Children)
                {
                    stack.Push((level + 1, pair.Key, pair.Value));
                }
            }
        }

        private class Node
        {
            public Node()
                : this(default(char), null)
            {
            }

            public Node(char letter, Node parent)
            {
                this.Parent = parent;
                this.Letter = letter;
                this.Children = new Dictionary<char, Node>();
                this.IsWord = false;
            }

            public Node Parent { get; set; }

            public char Letter { get; private set; }

            public Dictionary<char, Node> Children { get; }

            public bool IsWord { get; set; }
        }
    }
}
