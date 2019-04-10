import UnionFind from "../../../src/DataStructures/UnionFind/UnionFind"

describe("Empty union-find structure", () => {
  let uf

  beforeEach(() => {
    uf = new UnionFind()
  })

  it("has length equal to 0", () => {
    expect(uf.length).toBe(0)
  })

  it("has disjoint sets count equal to 0", () => {
    expect(uf.disjointSetsCount).toBe(0)
  })

  it("throws error when ask to find element set", () => {
    expect(() => uf.find(1)).toThrowError()
  })

  it("throws error when ask to element's set size", () => {
    expect(() => uf.sizeOf(1)).toThrowError()
  })

  it("throws error when union two elements' sets", () => {
    expect(() => uf.union(1, 2)).toThrowError()
  })

  describe("making new set", () => {
    beforeEach(() => {
      uf.makeSet(1)
    })

    it("changes union-find length to 1", () => {
      expect(uf.length).toBe(1)
    })

    it("changes disjoint sets count to 1", () => {
      expect(uf.disjointSetsCount).toBe(1)
    })
  })
})

describe("Union-find with one element", () => {
  let uf
  const addedElement = 42

  beforeEach(() => {
    uf = new UnionFind()
    uf.makeSet(addedElement)
  })

  it("has length equal to 1", () => {
    expect(uf.length).toBe(1)
  })

  it("has disjoint sets count equal to 1", () => {
    expect(uf.disjointSetsCount).toBe(1)
  })

  it("find added element returns added element", () => {
    expect(uf.find(addedElement)).toBe(addedElement)
  })

  it.each([0, 1, -1, 1000, -1000])(
    "throws error when find non existing element %d",
    element => {
      expect(() => uf.find(element)).toThrowError()
    }
  )

  it("size of added element set should be equal to 1", () => {
    expect(uf.sizeOf(addedElement)).toBe(1)
  })

  it.each([0, 1, -1, 1000, -1000])(
    "throws error when finding set size for non existing element %d",
    element => {
      expect(() => uf.sizeOf(element)).toThrowError()
    }
  )

  it("union should return false when union added element with itself", () => {
    expect(uf.union(addedElement, addedElement)).toBeFalsy()
  })

  it.each([0, 1, -1, 1000, -1000])(
    "throws error when union non existing element %d",
    element => {
      expect(() => uf.union(addedElement, element)).toThrowError()
    }
  )

  it("throws error when make set for existing element", () => {
    expect(() => uf.makeSet(addedElement)).toThrowError()
  })

  it.each([0, 1, -1, 1000, -1000])(
    "making set changes length to 2 after adding non-existing element %d",
    element => {
      uf.makeSet(element)
      expect(uf.length).toBe(2)
    }
  )

  it.each([0, 1, -1, 1000, -1000])(
    "making set changes disjoint set count to 2 after adding non-existing element %d",
    element => {
      uf.makeSet(element)
      expect(uf.disjointSetsCount).toBe(2)
    }
  )
})

describe("Union-find with many elements", () => {
  let uf
  const addedElements = [17, 4, -1, 0, 19, -27]

  beforeEach(() => {
    uf = new UnionFind(addedElements)
  })

  it("has length equal to 6", () => {
    expect(uf.length).toBe(6)
  })

  it("has disjoint sets count equal to 6", () => {
    expect(uf.disjointSetsCount).toBe(6)
  })

  it.each(addedElements)(
    "find should return itself for added element %d",
    element => {
      expect(uf.find(element)).toBe(element)
    }
  )

  it.each([100, -99, 5])(
    "find should throw for non-existing element %d",
    element => {
      expect(() => uf.find(element)).toThrowError()
    }
  )

  it.each(addedElements)(
    "size of added element %d  should return 1",
    element => {
      expect(uf.sizeOf(element)).toBe(1)
    }
  )

  it.each([100, -99, 5])(
    "size of non-existing element %d should throw",
    element => {
      expect(() => uf.sizeOf(element)).toThrowError()
    }
  )

  it.each(addedElements)(
    "making set for added element %d should throw error",
    element => {
      expect(() => uf.makeSet(element)).toThrowError()
    }
  )

  it.each([100, -99, 5])(
    "making set for non-existing element %d should make length equal to 7",
    element => {
      uf.makeSet(element)

      expect(uf.length).toBe(7)
    }
  )

  it.each([100, -99, 5])(
    "making set for non-existing element %d should make disjoint sets equal to 7",
    element => {
      uf.makeSet(element)

      expect(uf.disjointSetsCount).toBe(7)
    }
  )

  it.each([[111, -1], [0, 5], [19, 7], [5, -27]])(
    "union throws error when union non-existing element",
    (l, r) => {
      expect(() => uf.union(l, r)).toThrowError()
    }
  )

  it.each([[4, -1], [0, 19], [19, 17], [-1, -27]])(
    "after union disjoint sets count equals to five",
    (l, r) => {
      uf.union(l, r)

      expect(uf.disjointSetsCount).toBe(5)
    }
  )

  it.each([[4, -1], [0, 19], [19, 17], [-1, -27]])(
    "find returns the same root element for two elements from the same set",
    (l, r) => {
      uf.union(l, r)

      expect(uf.find(l)).toBe(uf.find(r))
    }
  )

  it.each([[4, -1], [0, 19], [19, 17], [-1, -27]])(
    "size of united set of two separated elements should be equal to 2",
    (l, r) => {
      uf.union(l, r)

      expect(uf.sizeOf(l)).toBe(2)
      expect(uf.sizeOf(r)).toBe(2)
    }
  )

  it.each([[4, -1, 0], [0, 19, 17], [19, 17, -1], [4, -1, -27]])(
    "find returns the same root element for three elements from the same set",
    (a, b, c) => {
      uf.union(a, b)
      uf.union(c, b)

      expect(uf.find(a)).toBe(uf.find(b))
      expect(uf.find(b)).toBe(uf.find(c))
      expect(uf.find(c)).toBe(uf.find(a))
    }
  )

  it.each([[4, -1, 0], [0, 19, 17], [19, 17, -1], [4, -1, -27]])(
    "size of united set of three separeted elements should be equal to 3",
    (a, b, c) => {
      uf.union(a, b)
      uf.union(c, b)

      expect(uf.sizeOf(a)).toBe(3)
      expect(uf.sizeOf(b)).toBe(3)
      expect(uf.sizeOf(c)).toBe(3)
    }
  )
})
