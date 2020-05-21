namespace Algodore.DataStructures.FenwickTree
{
    public sealed class FenwickTreeZeroBased
    {
        private readonly int[] tree;

        public void Update(int index, int difference)
        {
            while (index < tree.Length)
            {
                tree[index] += difference;
                index = index | (index + 1);
            }
        }

        public int Prefix(int index)
        {
            int sum = 0;
            while (index >= 0)
            {
                sum += tree[index];
                index = index & (index + 1) - 1;
            }
            return sum;
        }

        public int Query(int from, int to)
        {
            return Prefix(to) - Prefix(from - 1);
        }
    }
}
