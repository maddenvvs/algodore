namespace Algodore.AI.DecisionTrees
{
    using System;
    using Algodore.AI.DecisionTrees.Construction;
    using Algodore.AI.DecisionTrees.Definition;

    /// <summary>
    /// Entry point to construct new decision trees.
    /// </summary>
    public static class DecisionTree
    {
        /// <summary>
        /// Return new decision tree which resolves to the specified value.
        ///
        /// Time complexity: O(1).
        ///
        /// Space complexity: O(1).
        /// </summary>
        /// <param name="value">Value to resolve from the tree.</param>
        /// <typeparam name="T">Resolved value type.</typeparam>
        /// <returns>Decision tree consisting of one node.</returns>
        public static IDecisionTree<T> FromResult<T>(T value)
            => new ValueAction<T>(value);

        /// <summary>
        /// Return new decision tree which resolves to the result of the factory.
        /// The factory function is executed every time the tree tries to resolve the value.
        ///
        /// Time complexity: O(1).
        ///
        /// Space complexity: O(1).
        /// </summary>
        /// <param name="factory">Factory to invoke.</param>
        /// <typeparam name="T">Resolved value type.</typeparam>
        /// <returns>Decision tree consisting of one node.</returns>
        public static IDecisionTree<T> FromResult<T>(Func<T> factory)
            => new FactoryAction<T>(factory);

        /// <summary>
        /// Starts new decision tree definition.
        ///
        /// Time complexity: O(1).
        ///
        /// Space complexity: O(1).
        /// </summary>
        /// <returns>Decision tree builder.</returns>
        public static DecisionTreeBuilder New()
            => new DecisionTreeBuilder();
    }
}
