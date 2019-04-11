"""
Union-Find (Disjoint Set Union) data structure implementation.
"""

__all__ = ['UnionFind']


class UnionFind:
    """
    Union-find data structure.
    """

    def __init__(self, values=[]):
        self._parent = {}
        self._size = {}
        self._disjoint_sets_count = 0

        for value in values:
            self.make_set(value)

    def __len__(self):
        return len(self._parent)

    @property
    def disjoint_sets_count(self):
        """
        Returns total number of disjoint sets in the UF.

        Time complexity: O(1).

        Space complexity: O(1).
        """

        return self._disjoint_sets_count

    def size_of(self, item):
        """
        Returns the size of the set the given item belongs to.

        Time complexity: O(α(N)).

        Space complexity: O(α(N)).

        N - number of items in the UF. α(N) - inverse Ackermann function.
        """

        return self._size[self.find(item)]

    def make_set(self, item):
        """
        Creates new disjoint set with given item.

        Time complexity: O(1).

        Space complexity: O(1).
        """

        if item in self._parent:
            raise ValueError()

        self._parent[item] = item
        self._size[item] = 1
        self._disjoint_sets_count += 1

    def find(self, item):
        """
        Returns root item of the set the given item belongs to.

        Time complexity: O(α(N)).

        Space complexity: O(α(N)).

        N - number of items in the UF. α(N) - inverse Ackermann function.
        """

        if item not in self._parent:
            raise ValueError()

        return self._find_root(item)

    def union(self, first, second):
        """
        Unions two sets two items are belong to respectively.

        Time complexity: O(α(N)).

        Space complexity: O(α(N)).

        N - number of items in the UF. α(N) - inverse Ackermann function.
        """
        first_root, second_root = self.find(first), self.find(second)

        if first_root == second_root:
            return False

        first_size, second_size = self._size[first_root], self._size[second_root]
        if first_size < second_size:
            first_root, second_root = second_root, first_root

        self._parent[second_root] = first_root
        self._size[first_root] += self._size[second_root]
        self._disjoint_sets_count -= 1

        return True

    def _find_root(self, item):
        if item == self._parent[item]:
            return item

        self._parent[item] = self._find_root(self._parent[item])
        return self._parent[item]
