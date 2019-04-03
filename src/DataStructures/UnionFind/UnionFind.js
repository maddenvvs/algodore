class UnionFind {
    constructor() {
        this._parent = {};
        this._size = {};
        this._disjointSetsCount = 0;
    }

    get length() {
        return Object.keys(this._parent).length;
    }

    get disjointSetsCount() {
        return this._disjointSetsCount;
    }

    sizeOf(item) {
        return this._size[this.find(item)];
    }

    makeSet(item) {
        if (this._parent[item]) {
            throw new Error("Union-find already contains given item.");
        }

        this._parent[item] = item;
        this._size[item] = 1;
        this._disjointSetsCount++;
    }

    find(item) {
        if (!this._parent[item]) {
            throw new Error("Union-find doesn't contain provided item.");
        }

        return this._findRoot(item);
    }

    union(first, second) {
        const firstRoot = this.find(first),
            secondRoot = this.find(second);

        if (firstRoot === secondRoot) {
            return false;
        }

        const firstSize = this._size[firstRoot],
            secondSize = this._size[secondRoot];

        if (firstSize < secondSize) {
            tmp = firstRoot;
            firstRoot = secondRoot;
            secondRoot = tmp;
        }

        this._parent[secondRoot] = firstRoot;
        this._size[firstRoot] += this._size[secondRoot];
        this._disjointSetsCount--;

        return true;
    }

    _findRoot(item) {
        if (item === this._parent[item]) {
            return item;
        }

        return (this._parent[item] = this._findRoot(this._parent[item]));
    }
}
