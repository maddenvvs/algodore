import pytest

from src.DataStructures.UnionFind.UnionFind import *


class TestEmptyUnionFind(object):

    def setup_method(self):
        self.uf = UnionFind()

    def test_len_equal_zero(self):
        assert len(self.uf) == 0

    def test_disjoint_sets_count_equal_zero(self):
        assert self.uf.disjoint_sets_count == 0

    def test_find_should_raise_error(self):
        with pytest.raises(ValueError):
            self.uf.find(1)

    def test_size_of_should_raise_error(self):
        with pytest.raises(ValueError):
            self.uf.size_of(1)

    def test_union_should_raise_error(self):
        with pytest.raises(ValueError):
            self.uf.union(1, 2)

    def test_make_set_length_becomes_one(self):
        self.uf.make_set(1)

        assert len(self.uf) == 1

    def test_make_set_disjoint_sets_count_becomes_one(self):
        self.uf.make_set(1)

        assert self.uf.disjoint_sets_count == 1


class TestUnionFindWithOneElement(object):

    def setup_method(self):
        self.uf = UnionFind()
        self.addedElement = 42
        self.uf.make_set(self.addedElement)

    def test_length_equals_one(self):
        assert len(self.uf) == 1

    def test_disjoint_sets_count_equals_to_one(self):
        assert self.uf.disjoint_sets_count == 1

    def test_find_added_element_should_return_added_element(self):
        assert self.addedElement == self.uf.find(self.addedElement)

    @pytest.mark.parametrize("element", [0, 1, 1000, -1000])
    def test_find_non_existing_element_should_raise_error(self, element):
        with pytest.raises(ValueError):
            self.uf.find(element)

    def test_size_of_added_element_should_return_one(self):
        assert self.uf.size_of(self.addedElement) == 1

    @pytest.mark.parametrize("element", [0, 1, 1000, -1000])
    def test_size_of_non_existing_element_should_raise_error(self, element):
        with pytest.raises(ValueError):
            self.uf.size_of(element)

    def test_union_added_element_with_itself_should_return_false(self):
        assert not self.uf.union(self.addedElement, self.addedElement)

    @pytest.mark.parametrize("element", [0, 1, 1000, -1000])
    def test_union_with_non_existing_element_should_raise_error(self, element):
        with pytest.raises(ValueError):
            self.uf.union(self.addedElement, element)

    def test_make_set_added_element_raises_error(self):
        with pytest.raises(ValueError):
            self.uf.make_set(self.addedElement)

    @pytest.mark.parametrize("element", [0, 1, 1000, -1000])
    def test_make_set_non_existing_element_changes_length_to_two(self, element):
        self.uf.make_set(element)

        assert len(self.uf) == 2

    @pytest.mark.parametrize("element", [0, 1, 1000, -1000])
    def test_make_set_non_existing_element_changes_dsc_to_two(self, element):
        self.uf.make_set(element)

        assert self.uf.disjoint_sets_count == 2


class TestUnionFindWithManyElements(object):

    def setup_method(self):
        self.addedElements = [17, 4, -1, 0, 19, -27]
        self.uf = UnionFind(self.addedElements)

    def test_length_equals_six(self):
        assert len(self.uf) == 6

    def test_disjoint_sets_count_equal_six(self):
        assert self.uf.disjoint_sets_count == 6

    @pytest.mark.parametrize("element", [17, 4, -1, 0, 19, -27])
    def test_find_should_return_itself_for_added_element(self, element):
        assert element == self.uf.find(element)

    @pytest.mark.parametrize("element", [100, -99, 5])
    def test_find_should_raise_error_for_non_existing_element(self, element):
        with pytest.raises(ValueError):
            self.uf.find(element)

    @pytest.mark.parametrize("element", [17, 4, -1, 0, 19, -27])
    def test_size_of_should_return_one_for_added_element(self, element):
        assert self.uf.size_of(element) == 1

    @pytest.mark.parametrize("element", [100, -99, 5])
    def test_size_of_should_raise_error_for_non_existing_element(self, element):
        with pytest.raises(ValueError):
            self.uf.size_of(element)

    @pytest.mark.parametrize("element", [17, 4, -1, 0, 19, -27])
    def test_make_set_should_raise_for_existing_element(self, element):
        with pytest.raises(ValueError):
            self.uf.make_set(element)

    @pytest.mark.parametrize("element", [100, -99, 5])
    def test_make_set_length_to_7_for_non_existing_element(self, element):
        self.uf.make_set(element)

        assert len(self.uf) == 7

    @pytest.mark.parametrize("element", [100, -99, 5])
    def test_make_set_dsc_to_7_for_non_existing_element(self, element):
        self.uf.make_set(element)

        assert self.uf.disjoint_sets_count == 7

    @pytest.mark.parametrize("l,r", [(111, -1), (0, 5), (19, 7), (5, -27)])
    def test_union_with_non_existing_element_should_raise_error(self, l, r):
        with pytest.raises(ValueError):
            self.uf.union(l, r)

    @pytest.mark.parametrize("l,r", [(4, -1), (0, 19), (19, 17), (-1, -27)])
    def test_union_disjoint_sets_count_decreased_by_one(self, l, r):
        self.uf.union(l, r)

        assert self.uf.disjoint_sets_count == 5

    @pytest.mark.parametrize("l,r", [(4, -1), (0, 19), (19, 17), (-1, -27)])
    def test_find_return_same_root_for_two_elements_from_the_same_set(self, l, r):
        self.uf.union(l, r)

        assert self.uf.find(l) == self.uf.find(r)

    @pytest.mark.parametrize("l,r", [(4, -1), (0, 19), (19, 17), (-1, -27)])
    def test_size_of_returns_two_after_union_two_separated_elements(self, l, r):
        self.uf.union(l, r)

        assert self.uf.size_of(l) == 2
        assert self.uf.size_of(r) == 2

    @pytest.mark.parametrize("a,b,c", [(4, -1, 0), (0, 19, 17), (19, 17, -1), (4, -1, -27)])
    def test_find_return_same_root_for_three_elements_from_the_same_set(self, a, b, c):
        self.uf.union(a, b)
        self.uf.union(c, b)

        assert self.uf.find(a) == self.uf.find(b)
        assert self.uf.find(b) == self.uf.find(c)
        assert self.uf.find(c) == self.uf.find(a)

    @pytest.mark.parametrize("a,b,c", [(4, -1, 0), (0, 19, 17), (19, 17, -1), (4, -1, -27)])
    def test_size_of_return_three_for_three_elements_from_the_same_set(self, a, b, c):
        self.uf.union(a, b)
        self.uf.union(c, b)

        assert self.uf.size_of(a) == 3
        assert self.uf.size_of(b) == 3
        assert self.uf.size_of(c) == 3
