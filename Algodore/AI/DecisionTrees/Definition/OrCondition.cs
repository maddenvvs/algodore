namespace Algodore.AI.DecisionTrees.Definition
{
    /// <summary>
    /// OR-based decision.
    /// </summary>
    internal sealed class OrCondition : BinaryDecision<bool>
    {
        /// <summary>
        /// Create new OR condition decision tree.
        /// </summary>
        /// <param name="left">Left subtree.</param>
        /// <param name="right">Right subtree.</param>
        internal OrCondition(IDecisionTree<bool> left, IDecisionTree<bool> right)
            : base(left, right)
        {
        }

        /// <summary>
        /// Return true if one of the conditions is true. False otherwise.
        /// </summary>
        /// <returns>Boolean value.</returns>
        public override bool Resolve()
            => this.Left.Resolve() || this.Right.Resolve();
    }
}
