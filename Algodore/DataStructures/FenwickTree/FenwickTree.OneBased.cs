using System.Collections.Generic;
using System.Linq;

namespace Algodore.DataStructures.FenwickTree
{
    public sealed class FenwickTreeOneBased
    {
        private readonly int[] tree;

        public FenwickTreeOneBased(IEnumerable<int> items)
            : this(items.ToArray())
        {
        }

        private FenwickTreeOneBased(int[] array)
        {
            this.tree = new int[array.Length];
        }

        public void Update(int index, int difference)
        {
            index++;

            while (index <= tree.Length)
            {
                tree[index] += difference;
                index += index & (-index);
            }
        }

        public int Prefix(int index)
        {
            index++;
            int sum = 0;

            while (index > 0)
            {
                sum += tree[index];
                index -= index & (-index);
            }

            return sum;
        }

        public int Range(int from, int to)
        {
            return Prefix(to) - Prefix(from - 1);
        }
    }
}
