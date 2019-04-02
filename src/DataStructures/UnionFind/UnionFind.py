class UnionFind:
    def __init__(self):
        self._parent = {}
        self._size = {}
        self._disjointSetsCount = 0

    def __len__(self):
        return len(self._parent)

    @property
    def disjointSetsCount(self):
        return self._disjointSetsCount

    def sizeOf(self, item):
        return self._size[self.find(item)]

    def makeSet(self, item):
        if item in self._parent:
            raise ValueError()

        self._parent[item] = item
        self._size[item] = 1
        self._disjointSetsCount += 1

    def find(self, item):
        if item not in self._parent:
            raise ValueError()

        return self._findRoot(item)

    def union(self, first, second):
        firstRoot, secondRoot = self.find(first), self.find(second)

        if firstRoot == secondRoot:
            return False

        firstSize, secondSize = self._size[firstRoot], self._size[secondRoot]
        if firstSize < secondSize:
            firstRoot, secondRoot = secondRoot, firstRoot

        self._parent[secondRoot] = firstRoot
        self._size[firstRoot] += self._size[secondRoot]
        self._disjointSetsCount -= 1

        return True

    def _findRoot(self, item):
        if item == self._parent[item]:
            return item

        self._parent[item] = self._findRoot(self._parent[item])
        return self._parent[item]
