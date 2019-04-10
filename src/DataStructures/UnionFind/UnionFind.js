/**
 * Union-Find (Disjoint Set Union) data structure implementation.
 */
export default class UnionFind {
  /**
   * Creates empty UF and populates it with values from the collection.
   *
   * Time complexity: O(N).
   *
   * Space complexity: O(N).
   *
   * N - length of the collection.
   *
   * @param {T[]} values The collection to populate values from.
   */
  constructor(values = []) {
    this._parent = Object.create(null)
    this._size = {}
    this._disjointSetsCount = 0
    this._length = 0

    for (const value of values) {
      this.makeSet(value)
    }
  }

  /**
   * Returns total number of values stored in the UF.
   *
   * Time complexity: O(1).
   *
   * Space complexity: O(1).
   *
   * @return {number}
   */
  get length() {
    return this._length
  }

  /**
   * Returns total number of disjoint sets in the UF.
   *
   * Time complexity: O(1).
   *
   * Space complexity: O(1).
   *
   * @return {number}
   */
  get disjointSetsCount() {
    return this._disjointSetsCount
  }

  /**
   * Returns the size of the set the given item belongs to.
   *
   * Time complexity: O(α(N)).
   *
   * Space complexity: O(α(N)).
   *
   * N - number of items in the UF. α(N) - inverse Ackermann function.
   *
   * @param {T} item
   * @return {number}
   * @throws {Error} Thrown when given item isn't contained in the UF.
   */
  sizeOf(item) {
    return this._size[this.find(item)]
  }

  /**
   * Creates new disjoint set with given item.
   *
   * Time complexity: O(1).
   *
   * Space complexity: O(1).
   *
   * @param {T} item
   * @throws {Error} Thrown when given item is already contained in the UF.
   */
  makeSet(item) {
    if (item in this._parent) {
      throw new Error("Union-find already contains given item.")
    }

    this._parent[item] = item
    this._size[item] = 1
    this._disjointSetsCount++
    this._length++
  }

  /**
   * Returns root item of the set the given item belongs to.
   *
   * Time complexity: O(α(N)).
   *
   * Space complexity: O(α(N)).
   *
   * N - number of items in the UF. α(N) - inverse Ackermann function.
   *
   * @param {T} item
   * @return {T}
   * @throws {Error} Thrown when given item isn't contained in the UF.
   */
  find(item) {
    if (!(item in this._parent)) {
      throw new Error("Union-find doesn't contain provided item.")
    }

    return this._findRoot(item)
  }

  /**
   * Unions two sets two items are belong to respectively.
   *
   * Time complexity: O(α(N)).
   *
   * Space complexity: O(α(N)).
   *
   * N - number of items in the UF. α(N) - inverse Ackermann function.
   *
   * @param {T} first
   * @param {T} second
   * @return {boolean} true if items united, false otherwise.
   */
  union(first, second) {
    let firstRoot = this.find(first)
    let secondRoot = this.find(second)

    if (firstRoot === secondRoot) {
      return false
    }

    const firstSize = this._size[firstRoot]
    const secondSize = this._size[secondRoot]

    if (firstSize < secondSize) {
      const tmp = firstRoot
      firstRoot = secondRoot
      secondRoot = tmp
    }

    this._parent[secondRoot] = firstRoot
    this._size[firstRoot] += this._size[secondRoot]
    this._disjointSetsCount--

    return true
  }

  /**
   * Returns root element of a set the given item belongs to.
   *
   * Time complexity: O(α(N)).
   *
   * Space complexity: O(α(N)).
   *
   * N - number of items in the UF. α(N) - inverse Ackermann function.
   *
   * @private
   * @param {T} item
   * @return {T}
   */
  _findRoot(item) {
    if (item === this._parent[item]) {
      return item
    }

    return (this._parent[item] = this._findRoot(this._parent[item]))
  }
}

/**
 * Type of values stored in the UF.
 *
 * @typedef T
 */
