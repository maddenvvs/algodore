namespace DataStructures
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Radix tree (also radix trie or compact trie) implementation.
    /// </summary>
    public class RadixTree
    {
        /// <summary>
        /// Creates empty tree.
        ///
        /// Time complexity: O(1).
        ///
        /// Space complexity: O(1).
        /// </summary>
        public RadixTree()
            : this(Array.Empty<string>())
        {
        }

        /// <summary>
        /// Creates empty tree and populates it with the given words collection.
        ///
        /// Time complexity: O(N * K).
        ///
        /// Space complexity: O(N * K).
        ///
        /// N - number of words, K - max length of word.
        /// </summary>
        /// <param name="words">The collection of words to be added to the tree.</param>
        public RadixTree(IEnumerable<string> words)
        {
            if (words == null)
            {
                throw new ArgumentNullException(nameof(words));
            }

            this.Count = 0;
            this.Root = new Node();

            foreach (var word in words)
            {
                this.AddWord(word);
            }
        }

        /// <summary>
        /// Gets words count stored in the tree.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Determines whether the tree is empty.
        /// </summary>
        public bool IsEmpty => this.Count == 0;

        private Node Root { get; set; }

        /// <summary>
        /// Inserts given word in the tree.
        ///
        /// Time complexity: O(K).
        ///
        /// Space complexity: O(K).
        ///
        /// K - length of word.
        /// </summary>
        /// <param name="word">The word to add to the tree.</param>
        /// <returns>Returns modified tree with added word.</returns>
        public RadixTree AddWord(string word)
        {
            if (word == null)
            {
                throw new ArgumentNullException(nameof(word));
            }

            Node currentNode = this.Root;
            int wordIdx = 0, inNodeIdx = 0;

            while (wordIdx < word.Length)
            {
                var wordChar = word[wordIdx];

                if (inNodeIdx == currentNode.Label.Length)
                {
                    if (!currentNode.Children.TryGetValue(wordChar, out Node nextNode))
                    {
                        nextNode = new Node(word.Substring(wordIdx), currentNode);
                        currentNode.Children[wordChar] = nextNode;

                        currentNode = nextNode;

                        break;
                    }

                    currentNode = nextNode;
                    inNodeIdx = 0;
                }
                else
                {
                    if (wordChar != currentNode.Label[inNodeIdx])
                    {
                        var newParent = SplitNodeAtIndex(currentNode, inNodeIdx);

                        var wordNode = new Node(word.Substring(wordIdx), newParent);
                        newParent.Children[wordChar] = wordNode;

                        currentNode = wordNode;

                        break;
                    }

                    wordIdx++;
                    inNodeIdx++;
                }
            }

            if (wordIdx == word.Length && inNodeIdx < currentNode.Label.Length)
            {
                currentNode = SplitNodeAtIndex(currentNode, inNodeIdx);
            }

            if (!currentNode.IsWord)
            {
                currentNode.IsWord = true;
                this.Count++;
            }

            return this;
        }

        /// <summary>
        /// Checks whether the tree contains the given word.
        ///
        /// Time complexity: O(K).
        ///
        /// Space complexity: O(1).
        ///
        /// K - length of word.
        /// </summary>
        /// <param name="word">The word to look for.</param>
        /// <returns>
        /// Returns true if the tree contains given word, otherwise false.
        /// </returns>
        public bool ContainsWord(string word)
        {
            if (word == null)
            {
                throw new ArgumentNullException(nameof(word));
            }

            var (prefixNode, inNodeIdx) = this.FindLastNodeOfPrefix(word);

            return IsNodeMeansEndOfWord(prefixNode, inNodeIdx);
        }

        /// <summary>
        /// Checks whether the tree contains the given prefix.
        ///
        /// Time complexity: O(K).
        ///
        /// Space complexity: O(1).
        ///
        /// K - length of word.
        /// </summary>
        /// <param name="prefix">The prefix to look for.</param>
        /// <returns>
        /// Returns true if the tree contains given prefix, otherwise false.
        /// </returns>
        public bool ContainsPrefix(string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            var (prefixNode, _) = this.FindLastNodeOfPrefix(prefix);

            return prefixNode != null;
        }

        /// <summary>
        /// Removes given word from the tree.
        ///
        /// Time complexity: O(K).
        ///
        /// Space complexity: O(1).
        ///
        /// K - length of word.
        /// </summary>
        /// <param name="word">The word to delete from the tree.</param>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if the tree doesn't contain the given word.
        /// </exception>
        /// <returns>
        /// Returns modified tree without deleted word.
        /// </returns>
        public RadixTree DeleteWord(string word)
        {
            if (word == null)
            {
                throw new ArgumentNullException(nameof(word));
            }

            var (prefixNode, inNodeIdx) = this.FindLastNodeOfPrefix(word);

            if (!IsNodeMeansEndOfWord(prefixNode, inNodeIdx))
            {
                throw new InvalidOperationException(
                    $"There is no word '{word}' in tree.");
            }

            prefixNode.IsWord = false;

            if (prefixNode.Children.Count == 0)
            {
                prefixNode.Parent.Children.Remove(prefixNode.Label[0]);
            }

            this.Count--;

            return this;
        }

        /// <summary>
        /// Returns all words the tree contains.
        ///
        /// Time complexity: O(N * K).
        ///
        /// Space complexity: O(N * K).
        ///
        /// N - number of words, K - max length of word.
        /// </summary>
        /// <returns>
        /// Returns collection of all words in the tree.
        /// </returns>
        public IEnumerable<string> FindAllWords() =>
            this.FindWordsWithPrefix(string.Empty);

        /// <summary>
        /// Returns all words with given prefix the tree contains.
        ///
        /// Time complexity: O(N * K).
        ///
        /// Space complexity: O(N * K).
        ///
        /// N - number of words, K - max length of word.
        /// </summary>
        /// <param name="prefix">The prefix to look for.</param>
        /// <returns>Returns collection of founded words in the tree.</returns>
        public IEnumerable<string> FindWordsWithPrefix(string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(prefix);
            }

            var (prefixNode, inNodeIdx) = this.FindLastNodeOfPrefix(prefix);

            if (prefixNode == null)
            {
                return Array.Empty<string>();
            }

            if (inNodeIdx != prefixNode.Label.Length)
            {
                prefix += prefixNode.Label.Substring(inNodeIdx);
            }

            return this.FindWordsContainingNode(prefixNode, prefix);
        }

        /// <summary>
        /// Clears the tree.
        /// </summary>
        /// <returns>Returns modified empty tree.</returns>
        public RadixTree Clear()
        {
            this.Root = new Node();
            this.Count = 0;

            return this;
        }

        private static Node SplitNodeAtIndex(Node node, int index)
        {
            var newParent = new Node(node.Label.Substring(0, index), node.Parent);

            newParent.Children[node.Label[index]] = node;
            node.Parent.Children[node.Label[0]] = newParent;

            node.Label = node.Label.Substring(index);
            node.Parent = newParent;

            return newParent;
        }

        private static bool IsNodeMeansEndOfWord(Node node, int inNodeIdx) =>
            node != null
            && node.IsWord
            && inNodeIdx == node.Label.Length;

        private (Node LastNode, int IndexInNode) FindLastNodeOfPrefix(string prefix)
        {
            Node currentNode = this.Root;
            int prefixIdx = 0, inNodeIdx = 0;

            while (prefixIdx < prefix.Length)
            {
                var letter = prefix[prefixIdx];

                if (inNodeIdx == currentNode.Label.Length)
                {
                    if (!currentNode.Children.TryGetValue(letter, out Node nextNode))
                    {
                        return (null, inNodeIdx);
                    }

                    currentNode = nextNode;
                    inNodeIdx = 0;
                }
                else
                {
                    if (letter != currentNode.Label[inNodeIdx])
                    {
                        return (null, inNodeIdx);
                    }

                    prefixIdx++;
                    inNodeIdx++;
                }
            }

            return (currentNode, inNodeIdx);
        }

        private IEnumerable<string> FindWordsContainingNode(Node node, string prefix)
        {
            if (node.IsWord)
            {
                yield return prefix;
            }

            var builder = new StringBuilder(prefix);
            var stack = new Stack<(int, Node)>();
            var lastVisitedLevel = 0;

            foreach (var child in node.Children.Values)
            {
                stack.Push((child.Label.Length, child));
            }

            while (stack.Count > 0)
            {
                (int level, Node currentNode) = stack.Pop();

                if (level > lastVisitedLevel)
                {
                    builder.Append(currentNode.Label);
                }
                else
                {
                    var charsToRemove = lastVisitedLevel - level + 1;
                    builder.Remove(builder.Length - charsToRemove, charsToRemove);
                    builder.Append(currentNode.Label);
                }

                lastVisitedLevel = level;

                if (currentNode.IsWord)
                {
                    yield return builder.ToString();
                }

                foreach (var child in currentNode.Children.Values)
                {
                    stack.Push((level + child.Label.Length, child));
                }
            }
        }

        private class Node
        {
            public Node()
                : this(string.Empty, null)
            {
            }

            public Node(string label, Node parent)
            {
                this.Label = label;
                this.Children = new Dictionary<char, Node>();
                this.Parent = parent;
                this.IsWord = false;
            }

            public string Label { get; set; }

            public Node Parent { get; set; }

            public Dictionary<char, Node> Children { get; set; }

            public bool IsWord { get; set; }
        }
    }
}
