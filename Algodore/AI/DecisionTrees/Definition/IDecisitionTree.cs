namespace Algodore.AI.DecisionTrees.Definition
{
    /// <summary>
    /// Decision tree definition.
    /// </summary>
    /// <typeparam name="T">Type the tree resolves to.</typeparam>
    public interface IDecisionTree<T>
    {
        /// <summary>
        /// Resolve value of the tree based on it's structure/definition.
        ///
        /// Time complexity: O(H*TC).
        ///
        /// Space complexity: O(H*SC).
        ///
        /// H - height of a decision tree.
        /// TC - max time complexity of decision tree node resolution.
        /// SC - max space complexity of decision tree node resolution.
        /// </summary>
        /// <returns>Resolved value of the tree.</returns>
        T Resolve();
    }
}
