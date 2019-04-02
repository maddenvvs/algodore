class SinglyLinkedList {
  constructor() {
    this.clear();
  }

  get length() {
    return this.count;
  }

  get count() {
    return this._count;
  }

  get isEmpty() {
    return this.count === 0;
  }

  append(value) {
    const nodeToAppend = new SinglyLinkedList.Node(value);

    return this.appendNode(nodeToAppend);
  }

  appendNode(node) {
    if (!node) throw new Error("Incorrect node value");

    if (!this._head) {
      this._head = this._tail = node;
    } else {
      this._tail.next = node;
      this._tail = this._tail.next;
    }

    this._count++;

    return this;
  }

  prepend(value) {
    const nodeToPrepend = new SinglyLinkedList.Node(value);

    return this.prependNode(nodeToPrepend);
  }

  prependNode(node) {
    if (!node) throw new Error("Incorrect node value");

    if (!this._head) {
      this._head = this._tail = node;
    } else {
      node.next = this._head;
      this._head = node;
    }

    this._count++;

    return this;
  }

  insertAt(value, index) {
    const nodeToInsert = new SinglyLinkedList.Node(value);

    return this.insertNodeAt(nodeToInsert, index);
  }

  insertNodeAt(node, index) {
    if (!node) throw new Error("Incorrect node value");

    if (index === 0) {
      return this.prependNode(node);
    } else if (index === this.count) {
      return this.appendNode(node);
    } else if (index > 0 && index < this.count) {
      const nodeBefore = this.getNodeAt(index - 1);

      node.next = nodeBefore.next;
      nodeBefore.next = node;

      this._count++;

      return this;
    }

    throw new Error("Index out of range");
  }

  getAt(index) {
    return this.getNodeAt(index).value;
  }

  getNodeAt(index) {
    if (this.isEmpty || index < 0 || index >= this.count)
      throw new Error("Index out of range");

    if (index === 0) {
      return this._head;
    } else if (index === this.count - 1) {
      return this._tail;
    } else {
      let currentNode = this._head;
      while (index > 0) {
        currentNode = currentNode.next;
        index--;
      }

      return currentNode;
    }
  }

  removeFirst() {
    if (this.isEmpty) throw new Error("Cannot remove from empty list");

    if (this._head === this._tail) {
      this.clear();
      return this;
    } else {
      this._head = this._head.next;
      this._count--;

      return this;
    }
  }

  removeLast() {
    if (this.isEmpty) throw new Error("Cannot remove from empty list");

    if (this._head === this._tail) {
      this.clear();
      return this;
    } else {
      const nodeBefore = this.getNodeAt(this.count - 2);

      this._tail = nodeBefore;
      this._tail.next = null;

      this._count--;

      return this;
    }
  }

  removeAt(index) {
    if (this.isEmpty || index < 0 || index >= this.count)
      throw new Error("Index out of range");

    if (index === 0) {
      return this.removeFirst();
    } else if (index === this.count - 1) {
      return this.removeLast();
    } else {
      const nodeBefore = this.getNodeAt(index - 1);
      nodeBefore.next = nodeBefore.next.next;

      this._count--;

      return this;
    }
  }

  indexOf(value) {
    if (value instanceof SinglyLinkedList.Node) {
      return this.indexOfNode(value);
    }

    let foundIndex = 0,
      currentNode = this._head;

    while (currentNode != null && currentNode.value != value) {
      currentNode = currentNode.next;
      foundIndex++;
    }

    return currentNode == null ? -1 : foundIndex;
  }

  indexOfNode(node) {
    let foundIndex = 0,
      currentNode = this._head;

    while (currentNode != null && currentNode != node) {
      currentNode = currentNode.next;
      foundIndex++;
    }

    return currentNode == null ? -1 : foundIndex;
  }

  clear() {
    this._head = this._tail = null;
    this._count = 0;
  }

  *[Symbol.iterator]() {
    let currentNode = this._head;
    while (currentNode !== null) {
      yield currentNode.value;
      currentNode = currentNode.next;
    }
  }
}

SinglyLinkedList.Node = class Node {
  constructor(value) {
    this.value = value;
    this.next = null;
  }
};
