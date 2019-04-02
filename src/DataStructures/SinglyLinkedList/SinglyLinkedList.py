class SinglyLinkedList:

    class Node:
        def __init__(self, value):
            self.value = value
            self.next = None

    def __init__(self):
        self.clear()

    @property
    def count(self):
        return self._count

    @property
    def isEmpty(self):
        return self.count == 0

    def append(self, value):
        nodeToAppend = SinglyLinkedList.Node(value)

        return self.appendNode(nodeToAppend)

    def appendNode(self, node):
        if not isinstance(node, SinglyLinkedList.Node):
            raise TypeError()

        if self._head is None:
            self._head = self._tail = node
        else:
            self._tail.next = node
            self._tail = self._tail.next

        self._count += 1

        return self

    def prepend(self, value):
        nodeToPrepend = SinglyLinkedList.Node(value)

        return self.prependNode(nodeToPrepend)

    def prependNode(self, node):
        if not isinstance(node, SinglyLinkedList.Node):
            raise TypeError()

        if self._head is None:
            self._head = self._tail = node
        else:
            node.next = self._head
            self._head = node

        self._count += 1

        return self

    def insertAt(self, value, index):
        nodeToInsert = SinglyLinkedList.Node(value)

        return self.insertNodeAt(nodeToInsert, index)

    def insertNodeAt(self, node, index):
        if not isinstance(node, SinglyLinkedList.Node):
            raise TypeError()

        if index == 0:
            return self.prependNode(node)
        elif index == self.count:
            return self.appendNode(node)
        elif 0 < index < self.count:
            nodeBefore = self.getNodeAt(index - 1)

            node.next = nodeBefore.next
            nodeBefore.next = node

            self._count += 1

            return self

        raise IndexError()

    def getAt(self, index):
        return self.getNodeAt(index).value

    def getNodeAt(self, index):
        if self.isEmpty or index < 0 or index >= self.count:
            raise IndexError()

        if index == 0:
            return self._head
        elif index == self.count - 1:
            return self._tail
        else:
            currentNode = self._head

            while index > 0:
                currentNode = currentNode.next
                index -= 1

            return currentNode

    def removeFirst(self):
        if self.isEmpty:
            raise AssertionError()

        if self._head == self._tail:
            self._head = self._tail = None
            self._count = 0
        else:
            self._head = self._head.next
            self._count -= 1

        return self

    def removeLast(self):
        if self.isEmpty:
            raise AssertionError()

        if self._head == self._tail:
            self._head = self._tail = None
            self._count = 0
        else:
            nodeBeforeTail = self.getNodeAt(self.count - 2)
            self._tail = nodeBeforeTail
            self._tail.next = None
            self._count -= 1

        return self

    def removeAt(self, index):
        if self.isEmpty or index < 0 or index >= self.count:
            raise IndexError()

        if index == 0:
            return self.removeFirst()
        elif index == self.count - 1:
            return self.removeLast()
        else:
            nodeBefore = self.getNodeAt(index - 1)
            nodeBefore.next = nodeBefore.next.next

            self._count -= 1

            return self

    def indexOf(self, value):
        if isinstance(value, SinglyLinkedList.Node):
            return self.indexOfNode(value)

        foundIndex = 0
        currentNode = self._head

        while currentNode is not None and currentNode.value != value:
            currentNode = currentNode.next
            foundIndex += 1

        return -1 if currentNode is None else foundIndex

    def indexOfNode(self, node):
        if not isinstance(node, SinglyLinkedList.Node):
            raise TypeError()

        foundIndex = 0
        currentNode = self._head

        while currentNode is not None and currentNode != node:
            currentNode = currentNode.next
            foundIndex += 1

        return -1 if currentNode is None else foundIndex

    def __iter__(self):
        currentNode = self._head

        while currentNode is not None:
            yield currentNode.value

            currentNode = currentNode.next

    def clear(self):
        self._head = self._tail = None
        self._count = 0
