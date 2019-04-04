namespace DataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        public SinglyLinkedList()
        {
            this.Clear();
        }

        public int Count { get; private set; }

        public bool IsEmpty { get => this.Count == 0; }

        private Node Head { get; set; }

        private Node Tail { get; set; }

        public SinglyLinkedList<T> Append(T value)
        {
            var nodeToAppend = new Node(value);

            return this.AppendNode(nodeToAppend);
        }

        public SinglyLinkedList<T> AppendNode(Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (this.Head == null)
            {
                this.Head = this.Tail = node;
            }
            else
            {
                this.Tail.Next = node;
                this.Tail = this.Tail.Next;
            }

            this.Count++;

            return this;
        }

        public SinglyLinkedList<T> Prepend(T value)
        {
            var nodeToPrepend = new Node(value);

            return this.PrependNode(nodeToPrepend);
        }

        public SinglyLinkedList<T> PrependNode(Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (this.Head == null)
            {
                this.Head = this.Tail = node;
            }
            else
            {
                node.Next = this.Head;
                this.Head = node;
            }

            this.Count++;

            return this;
        }

        public SinglyLinkedList<T> InsertAt(T value, int index)
        {
            var nodeToInsert = new Node(value);

            return this.InsertNodeAt(nodeToInsert, index);
        }

        public SinglyLinkedList<T> InsertNodeAt(Node node, int index)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (index == 0)
            {
                return this.PrependNode(node);
            }
            else if (index == this.Count)
            {
                return this.AppendNode(node);
            }
            else if (index > 0 && index < this.Count)
            {
                var nodeBefore = this.GetNodeAt(index - 1);

                node.Next = nodeBefore.Next;
                nodeBefore.Next = node;

                this.Count++;

                return this;
            }

            throw new ArgumentOutOfRangeException(nameof(index));
        }

        public T GetAt(int index)
        {
            return this.GetNodeAt(index).Value;
        }

        public Node GetNodeAt(int index)
        {
            if (this.IsEmpty || index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (index == 0)
            {
                return this.Head;
            }
            else if (index == this.Count - 1)
            {
                return this.Tail;
            }
            else
            {
                var currentNode = this.Head;
                while (index > 0)
                {
                    currentNode = currentNode.Next;
                    index--;
                }

                return currentNode;
            }
        }

        public SinglyLinkedList<T> RemoveFirst()
        {
            if (this.IsEmpty)
            {
                throw new InvalidOperationException("Cannot remove from empty linked list.");
            }

            if (this.Head == this.Tail)
            {
                this.Head = this.Tail = null;
                this.Count = 0;
            }
            else
            {
                this.Head = this.Head.Next;
                this.Count--;
            }

            return this;
        }

        public SinglyLinkedList<T> RemoveLast()
        {
            if (this.IsEmpty)
            {
                throw new InvalidOperationException("Cannot remove from empty linked list.");
            }

            if (this.Head == this.Tail)
            {
                this.Head = this.Tail = null;
                this.Count = 0;
            }
            else
            {
                var nodeBeforeTail = this.GetNodeAt(this.Count - 2);
                this.Tail = nodeBeforeTail;
                this.Tail.Next = null;
                this.Count--;
            }

            return this;
        }

        public SinglyLinkedList<T> RemoveAt(int index)
        {
            if (this.IsEmpty || index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (index == 0)
            {
                return this.RemoveFirst();
            }
            else if (index == this.Count - 1)
            {
                return this.RemoveLast();
            }
            else
            {
                var nodeBefore = this.GetNodeAt(index - 1);
                nodeBefore.Next = nodeBefore.Next.Next;
                this.Count--;

                return this;
            }
        }

        /// <summary>
        /// Finds index of the given value in the list with custom comparer provided.
        ///
        /// Time complexity: O(N).
        ///
        /// Space complexity: O(1).
        ///
        /// N is the length of the list.
        /// </summary>
        /// <param name="value">The value to find in the list.</param>
        /// <param name="comparer">The comparer to use for equality checks.</param>
        /// <returns>Returns index of the value if exists, -1 otherwise.</returns>
        public int IndexOf(T value, IEqualityComparer<T> comparer)
        {
            var foundIndex = 0;
            var currentNode = this.Head;

            while (currentNode != null && !comparer.Equals(currentNode.Value, value))
            {
                currentNode = currentNode.Next;
                foundIndex++;
            }

            return currentNode == null ? -1 : foundIndex;
        }

        /// <summary>
        /// Finds index of the given value in the list.
        ///
        /// Time complexity: O(N).
        ///
        /// Space complexity: O(1).
        ///
        /// N is the length of the list.
        /// </summary>
        /// <param name="value">The value to find in the list.</param>
        /// <returns>Returns index of the value if exists, -1 otherwise.</returns>
        public int IndexOf(T value) =>
            this.IndexOf(value, EqualityComparer<T>.Default);

        /// <summary>
        /// Finds index of the given node in the list.
        ///
        /// Time complexity: O(N).
        ///
        /// Space complexity: O(1).
        ///
        /// N is the length of the list.
        /// </summary>
        /// <param name="node">The node to find in the list.</param>
        /// <returns>Returns index of the node if exists, -1 otherwise.</returns>
        public int IndexOf(Node node)
        {
            var foundIndex = 0;
            var currentNode = this.Head;

            while (currentNode != null && currentNode != node)
            {
                currentNode = currentNode.Next;
                foundIndex++;
            }

            return currentNode == null ? -1 : foundIndex;
        }

        /// <summary>
        /// Clears the chain of nodes in the list.
        ///
        /// Time complexity: O(1).
        ///
        /// Space complexity: O(1).
        /// </summary>
        /// <returns>Returns modified empty list.</returns>
        public SinglyLinkedList<T> Clear()
        {
            this.Head = this.Tail = null;
            this.Count = 0;

            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>Returns the iterator object.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = this.Head;

            while (currentNode != null)
            {
                yield return currentNode.Value;

                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        /// Node class representing node element in singly linked list.
        /// </summary>
        public class Node
        {
            /// <summary>
            /// Initializes list node instance with the given value.
            /// </summary>
            /// <param name="value">Value to store in the node.</param>
            public Node(T value)
            {
                this.Value = value;
                this.Next = null;
            }

            /// <summary>
            /// Gets or sets value of given node.
            /// </summary>
            /// <value>Value stored in the node.</value>
            public T Value { get; set; }

            /// <summary>
            /// Gets or sets next node in the chain of singly linked list.
            /// </summary>
            /// <value>Reference to the next node in chain.</value>
            public Node Next { get; set; }
        }
    }
}
