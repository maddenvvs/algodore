using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        public class Node<T>
        {
            public Node(T value)
            {
                Value = value;
                Next = null;
            }

            public T Value { get; set; }

            public Node<T> Next { get; set; }
        }

        public SinglyLinkedList()
        {
            Clear();
        }

        private Node<T> Head { get; set; }

        private Node<T> Tail { get; set; }

        public int Count { get; private set; }

        public bool IsEmpty { get; } => Count == 0;

        public SinglyLinkedList<T> Append(T value)
        {
            var nodeToAppend = new Node(value);

            return AppendNode(nodeToAppend);
        }

        public SinglyLinkedList<T> AppendNode(Node<T> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            if (Head == null)
            {
                Head = Tail = node;
            }
            else
            {
                Tail.Next = node;
                Tail = Tail.Next;
            }

            Count++;

            return this;
        }

        public SinglyLinkedList<T> Prepend(T value)
        {
            var nodeToPrepend = new Node(value);

            return PrependNode(nodeToPrepend);
        }

        public SinglyLinkedList<T> PrependNode(Node<T> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            if (Head == null)
            {
                Head = Tail = node;
            }
            else
            {
                node.Next = Head;
                Head = node;
            }

            Count++;

            return this;
        }

        public SinglyLinkedList<T> InsertAt(T value, int index)
        {
            var nodeToInsert = new Node(value);

            return InsertNodeAt(nodeToInsert, index);
        }

        public SinglyLinkedList<T> InsertNodeAt(Node<T> node, int index)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            if (index == 0)
            {
                return PrependNode(node);
            }
            else if (index == Count)
            {
                return AppendNode(node);
            }
            else if (index > 0 && index < Count)
            {
                var nodeBefore = GetNodeAt(index - 1);

                node.Next = nodeBefore.Next;
                nodeBefore.Next = node;

                Count++;

                return this;
            }

            throw new ArgumentOutOfRangeException(nameof(index));
        }

        public T GetAt(int index)
        {
            return GetNodeAt(index).Value;
        }

        public Node<T> GetNodeAt(int index)
        {
            if (IsEmpty || index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (index == 0)
            {
                return Head;
            }
            else if (index == Count - 1)
            {
                return Tail;
            }
            else
            {
                var currentNode = Head;
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
            if (IsEmpty) throw new InvalidOperationException("Cannot remove from empty linked list.");

            if (Head == Tail)
            {
                Head = Tail = null;
                Count = 0;
            }
            else
            {
                Head = Head.Next;
                Count--;
            }

            return this;
        }

        public SinglyLinkedList<T> RemoveLast()
        {
            if (IsEmpty) throw new InvalidOperationException("Cannot remove from empty linked list.");

            if (Head == Tail)
            {
                Head = Tail = null;
                Count = 0;
            }
            else
            {
                var nodeBeforeTail = GetNodeAt(Count - 2);
                Tail = nodeBeforeTail;
                Tail.Next = null;
                Count--;
            }

            return this;
        }

        public SinglyLinkedList<T> RemoveAt(int index)
        {
            if (IsEmpty || index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (index == 0)
            {
                return RemoveFirst();
            }
            else if (index == Count - 1)
            {
                return RemoveLast();
            }
            else
            {
                var nodeBefore = GetNodeAt(index - 1);
                nodeBefore.Next = nodeBefore.Next.Next;
                Count--;

                return this;
            }
        }

        public int IndexOf(T value)
        {
            var foundIndex = 0;
            var currentNode = Head;

            while (currentNode != null && currentNode.Value != value)
            {
                currentNode = currentNode.Next;
                foundIndex++;
            }

            return currentNode == null ? -1 : foundIndex;
        }

        public int IndexOf(Node<T> node)
        {
            var foundIndex = 0;
            var currentNode = Head;

            while (currentNode != null && currentNode != node)
            {
                currentNode = currentNode.Next;
                foundIndex++;
            }

            return currentNode == null ? -1 : foundIndex;
        }

        public SinglyLinkedList<T> Clear()
        {
            Head = Tail = null;
            Count = 0;
            return this;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = Head;

            while (currentNode != null)
            {
                yield return currentNode.Value;

                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
