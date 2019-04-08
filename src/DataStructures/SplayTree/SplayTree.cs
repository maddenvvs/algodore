namespace DataStructures
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Splay tree implementation.
    /// </summary>
    /// <typeparam name="T">Type of elements to store in splay tree.</typeparam>
    public class SplayTree<T>
    {
        /// <summary>
        /// Creates empty splay tree.
        ///
        /// Time complexity: O(1).
        ///
        /// Space complexity: O(1).
        /// </summary>
        public SplayTree()
            : this(Comparer<T>.Default)
        {
        }

        /// <summary>
        /// Creates empty splay tree with custom element comparer.
        ///
        /// Time complexity: O(1).
        ///
        /// Space complexity O(1).
        /// </summary>
        /// <param name="comparer">
        /// The comparer used to determine element order in the tree.
        /// </param>
        public SplayTree(IComparer<T> comparer)
            : this(Array.Empty<T>(), comparer)
        {
        }

        /// <summary>
        /// Creates empty splay tree and populates it with values from
        /// the given collection.
        ///
        /// Time complexity: average - O(N * log(N)), worst - O(N^2).
        ///
        /// Space complexity: O(N).
        ///
        /// N - number of elements in the collection.
        /// </summary>
        /// <param name="collection">The collection of values.</param>
        public SplayTree(IEnumerable<T> collection)
            : this(collection, Comparer<T>.Default)
        {
        }

        /// <summary>
        /// Creates empty splay tree and populates it with values from
        /// the given collection with custom element comparer.
        ///
        /// Time complexity: average - O(N * log(N)), worst - O(N^2).
        ///
        /// Space complexity: O(N).
        ///
        /// N - number of elements in the collection.
        /// </summary>
        /// <param name="collection">The collection of values.</param>
        /// <param name="comparer">
        /// The comparer used to determine element order in the tree.
        /// </param>
        public SplayTree(IEnumerable<T> collection, IComparer<T> comparer)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            this.Count = 0;
            this.Comparer = comparer;
            this.Root = null;

            foreach (var value in collection)
            {
                this.Add(value);
            }
        }

        /// <summary>
        /// Gets the amount of elements stored in the tree.
        ///
        /// Time complexity: O(1).
        ///
        /// Space complexity: O(1).
        /// </summary>
        /// <value>The number of elements in the tree.</value>
        public int Count { get; private set; }

        /// <summary>
        /// Determines whether the tree is empty.
        ///
        /// Time complexity: O(1).
        ///
        /// Space complexity: O(1).
        /// </summary>
        public bool IsEmpty => this.Count == 0;

        private Node Root { get; set; }

        private IComparer<T> Comparer { get; }

        /// <summary>
        /// Gets minimal value stored in the tree.
        ///
        /// Time complexity: average - O(log(N)), worst - amortized O(log(N)).
        ///
        /// Space complexity: O(1).
        ///
        /// N - number of elements in the tree.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the trie is empty.
        /// </exception>
        /// <returns>Returns the minimal value in the tree.</returns>
        public T Min()
        {
            if (this.IsEmpty)
            {
                throw new InvalidOperationException("Cannot find min value in empty tree.");
            }

            var nodeWithMin = this.Root.FindNodeWithMinValue();

            this.Splay(nodeWithMin);

            return nodeWithMin.Value;
        }

        /// <summary>
        /// Gets maximal value stored in the tree.
        ///
        /// Time complexity: average - O(log(N)), worst - amortized O(log(N)).
        ///
        /// Space complexity: O(1).
        ///
        /// N - number of elements in the tree.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the trie is empty.
        /// </exception>
        /// <returns>Returns the maximal value in the tree.</returns>
        public T Max()
        {
            if (this.IsEmpty)
            {
                throw new InvalidOperationException("Cannot find max value in empty tree.");
            }

            var nodeWithMax = this.Root.FindNodeWithMaxValue();

            this.Splay(nodeWithMax);

            return nodeWithMax.Value;
        }

        /// <summary>
        /// Checks whether the tree contains given value.
        ///
        /// Time complexity: average - O(log(N)), worst - amortized O(log(N)).
        ///
        /// Space complexity: O(1).
        ///
        /// N - number of elements in the tree.
        /// </summary>
        /// <param name="value">The value to check for.</param>
        /// <returns>
        /// Returns true if the tree contains value, otherwise false.
        /// </returns>
        public bool Contains(T value)
        {
            var treeNode = this.Find(value);

            return treeNode != null && this.AreValuesEqual(value, treeNode.Value);
        }

        /// <summary>
        /// Adds given value to the tree.
        ///
        /// Time complexity: average - O(log(N)), worst - amortized O(log(N)).
        ///
        /// Time complexity: O(1).
        ///
        /// N - number of elements in the tree.
        /// </summary>
        /// <param name="value">The value to add.</param>
        /// <returns>
        /// Returns modified tree with added element.
        /// </returns>
        public SplayTree<T> Add(T value)
        {
            (Node left, Node right) = this.Split(value);

            this.Root = new Node(value)
            {
                Left = left,
                Right = right,
            };

            this.Root.RefreshChildrenParent();

            this.Count++;

            return this;
        }

        /// <summary>
        /// Removes the given value from the tree.
        ///
        /// Time complexity: average - O(log(N)), worst - amortized O(log(N)).
        ///
        /// Time complexity: O(1).
        ///
        /// N - number of elements in the tree.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        /// <returns>
        /// Returns modified tree with removed value.
        /// </returns>
        public SplayTree<T> Remove(T value)
        {
            (Node left, Node right) = this.Split(value);

            this.Root = this.Merge(left, right);

            return this;
        }

        /// <summary>
        /// Traverses the tree in-order.
        ///
        /// Time complexity: O(N).
        ///
        /// Space complexity: O(1).
        ///
        /// N - number of elements in the tree.
        /// </summary>
        /// <returns>
        /// Returns the collection of values as if they were traversed in-order.
        /// </returns>
        public IEnumerable<T> TraverseInOrder()
        {
            if (this.IsEmpty)
            {
                yield break;
            }

            var currentNode = this.Root.InOrderFirst();

            while (currentNode != null)
            {
                yield return currentNode.Value;

                currentNode = currentNode.InOrderNext();
            }
        }

        /// <summary>
        /// Traverses the tree pre-order.
        ///
        /// Time complexity: O(N).
        ///
        /// Space complexity: O(1).
        ///
        /// N - number of elements in the tree.
        /// </summary>
        /// <returns>
        /// Returns the collection of values as if they were traversed pre-order.
        /// </returns>
        public IEnumerable<T> TraversePreOrder()
        {
            if (this.IsEmpty)
            {
                yield break;
            }

            var currentNode = this.Root.PreOrderFirst();

            while (currentNode != null)
            {
                yield return currentNode.Value;

                currentNode = currentNode.PreOrderNext();
            }
        }

        /// <summary>
        /// Traverses the tree post-order.
        ///
        /// Time complexity: O(N).
        ///
        /// Space complexity: O(1).
        ///
        /// N - number of elements in the tree.
        /// </summary>
        /// <returns>
        /// Returns the collection of values as if they were traversed post-order.
        /// </returns>
        public IEnumerable<T> TraversePostOrder()
        {
            if (this.IsEmpty)
            {
                yield break;
            }

            var currentNode = this.Root.PostOrderFirst();

            while (currentNode != null)
            {
                yield return currentNode.Value;

                currentNode = currentNode.PostOrderNext();
            }
        }

        private static void SetParent(Node child, Node parent)
        {
            if (child != null)
            {
                child.Parent = parent;
            }
        }

        private Node FindNodeWithNearValue(T value)
        {
            Node currentNode = this.Root, prevNode = null;

            while (currentNode != null)
            {
                prevNode = currentNode;

                var compareValue = this.Comparer.Compare(value, currentNode.Value);

                if (compareValue == 0)
                {
                    return currentNode;
                }

                if (compareValue < 0)
                {
                    currentNode = currentNode.Left;
                }
                else
                {
                    currentNode = currentNode.Right;
                }
            }

            return prevNode;
        }

        private void Splay(Node node)
        {
            while (node.Parent != null)
            {
                var grandParent = node.Parent.Parent;

                if (grandParent == null)
                {
                    this.Rotate(node, node.Parent);
                }
                else
                {
                    var allChildsLeft = grandParent.Left == node.Parent
                        && node.Parent.Left == node;
                    var allChildsRight = grandParent.Right == node.Parent
                        && node.Parent.Right == node;
                    var isZigZig = allChildsLeft || allChildsRight;

                    if (isZigZig)
                    {
                        this.Rotate(node.Parent, grandParent);
                        this.Rotate(node, node.Parent);
                    }
                    else
                    {
                        this.Rotate(node, node.Parent);
                        this.Rotate(node, grandParent);
                    }
                }
            }
        }

        private void Rotate(Node child, Node parent)
        {
            var grandParent = parent.Parent;

            if (grandParent != null)
            {
                if (grandParent.Left == parent)
                {
                    grandParent.Left = child;
                }
                else
                {
                    grandParent.Right = child;
                }
            }
            else
            {
                this.Root = child;
            }

            if (parent.Left == child)
            {
                parent.Left = child.Right;
                child.Right = parent;
            }
            else
            {
                parent.Right = child.Left;
                child.Left = parent;
            }

            parent.RefreshChildrenParent();
            parent.Parent = child;
            child.Parent = grandParent;
        }

        private Node Find(T value)
        {
            var treeNode = this.FindNodeWithNearValue(value);

            if (treeNode != null)
            {
                this.Splay(treeNode);
            }

            return treeNode;
        }

        private (Node, Node) Split(T value)
        {
            if (this.Root == null)
            {
                return (null, null);
            }

            var root = this.Find(value);

            var compareValue = this.Comparer.Compare(value, root.Value);

            if (compareValue == 0)
            {
                Node left = root.Left,
                    right = root.Right;

                SetParent(right, null);
                SetParent(left, null);

                this.Count--;

                return (left, right);
            }

            if (compareValue < 0)
            {
                var left = root.Left;

                root.Left = null;
                SetParent(left, null);

                return (left, root);
            }
            else
            {
                var right = root.Right;

                root.Right = null;
                SetParent(right, null);

                return (root, right);
            }
        }

        private Node Merge(Node left, Node right)
        {
            if (left == null)
            {
                return right;
            }

            if (right == null)
            {
                return left;
            }

            var leftMaxNode = left.FindNodeWithMaxValue();
            this.Splay(leftMaxNode);

            leftMaxNode.Right = right;
            right.Parent = leftMaxNode;

            return leftMaxNode;
        }

        private bool AreValuesEqual(T left, T right) =>
            this.Comparer.Compare(left, right) == 0;

        private class Node
        {
            public Node(T value)
            {
                this.Value = value;

                this.Left = null;
                this.Right = null;

                this.Parent = null;
            }

            public T Value { get; set; }

            public Node Left { get; set; }

            public Node Right { get; set; }

            public Node Parent { get; set; }

            public Node InOrderFirst()
            {
                var currentNode = this;

                while (currentNode.Left != null)
                {
                    currentNode = currentNode.Left;
                }

                return currentNode;
            }

            public Node InOrderNext()
            {
                if (this.Right != null)
                {
                    return this.Right.FindNodeWithMinValue();
                }

                var currentNode = this;

                while (currentNode.Parent != null
                    && currentNode.Parent.Right == currentNode)
                {
                    currentNode = currentNode.Parent;
                }

                return currentNode.Parent;
            }

            public Node PreOrderFirst() => this;

            public Node PreOrderNext()
            {
                if (this.Left != null)
                {
                    return this.Left;
                }

                if (this.Right != null)
                {
                    return this.Right;
                }

                var currentNode = this;

                while (currentNode.Parent != null)
                {
                    if (currentNode.Parent.Left == currentNode
                        && currentNode.Parent.Right != null)
                    {
                        return currentNode.Parent.Right;
                    }

                    currentNode = currentNode.Parent;
                }

                return currentNode.Parent;
            }

            public Node PostOrderFirst()
            {
                var currentNode = this;

                while (currentNode.Left != null || currentNode.Right != null)
                {
                    if (currentNode.Left != null)
                    {
                        currentNode = currentNode.Left;
                    }
                    else
                    {
                        currentNode = currentNode.Right;
                    }
                }

                return currentNode;
            }

            public Node PostOrderNext()
            {
                if (this.Parent != null && this.Parent.Left == this &&
                    this.Parent.Right != null)
                {
                    return this.Parent.Right.PostOrderFirst();
                }

                return this.Parent;
            }

            public Node FindNodeWithMinValue() => this.InOrderFirst();

            public Node FindNodeWithMaxValue()
            {
                var currentNode = this;

                while (currentNode.Right != null)
                {
                    currentNode = currentNode.Right;
                }

                return currentNode;
            }

            public void RefreshChildrenParent()
            {
                if (this.Left != null)
                {
                    this.Left.Parent = this;
                }

                if (this.Right != null)
                {
                    this.Right.Parent = this;
                }
            }
        }
    }
}
