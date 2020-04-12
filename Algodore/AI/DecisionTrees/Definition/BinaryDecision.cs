namespace Algodore.AI.DecisionTrees.Definition
{
    /// <summary>
    /// Decision tree with two subtrees.
    /// </summary>
    /// <typeparam name="T">Resolved value type.</typeparam>
    internal abstract class BinaryDecision<T> : IDecision<T>
    {
        /// <summary>
        /// Create new binary decision.
        /// </summary>
        /// <param name="left">Left decision subtree.</param>
        /// <param name="right">Right decision subtree.</param>
        protected BinaryDecision(IDecisionTree<T> left, IDecisionTree<T> right)
            => (this.Left, this.Right) = (left, right);

        /// <summary>
        /// Left decision subtree.
        /// </summary>
        /// <value>Decision tree.</value>
        protected IDecisionTree<T> Left { get; }

        /// <summary>
        /// Right decision subtree.
        /// </summary>
        /// <value>Decision tree.</value>
        protected IDecisionTree<T> Right { get; }

        /// <inheritdoc/>
        public abstract T Resolve();
    }
}
