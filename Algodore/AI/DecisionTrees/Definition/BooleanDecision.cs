namespace Algodore.AI.DecisionTrees.Definition
{
    /// <summary>
    /// If/then/else decision tree.
    /// </summary>
    /// <typeparam name="T">Resolved value type.</typeparam>
    internal sealed class BooleanDecision<T> : BinaryDecision<T>
    {
        private IDecisionTree<bool> condition;

        /// <summary>
        /// Create new if/then/else decision tree.
        /// </summary>
        /// <param name="condition">Condition subtree.</param>
        /// <param name="consequent">Consequent (true) subtree.</param>
        /// <param name="alternative">Alternative (false) subtree.</param>
        internal BooleanDecision(
            IDecisionTree<bool> condition,
            IDecisionTree<T> consequent,
            IDecisionTree<T> alternative)
            : base(consequent, alternative)
        {
            this.condition = condition;
        }

        /// <summary>
        /// Decides which subtree to follow based on condition result.
        /// </summary>
        /// <returns>Decision tree resolved value.</returns>
        public override T Resolve()
        {
            if (this.condition.Resolve())
            {
                return this.Left.Resolve();
            }

            return this.Right.Resolve();
        }
    }
}
