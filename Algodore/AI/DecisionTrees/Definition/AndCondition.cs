namespace Algodore.AI.DecisionTrees.Definition
{
    /// <summary>
    /// AND-based decision.
    /// </summary>
    internal sealed class AndCondition : BinaryDecision<bool>
    {
        /// <summary>
        /// Create new AND condition decision tree.
        /// </summary>
        /// <param name="left">Left subtree.</param>
        /// <param name="right">Right subtree.</param>
        internal AndCondition(IDecisionTree<bool> left, IDecisionTree<bool> right)
            : base(left, right)
        {
        }

        /// <summary>
        /// Return true if all conditions are true. False otherwise.
        /// </summary>
        /// <returns>Boolean value.</returns>
        public override bool Resolve()
            => this.Left.Resolve() && this.Right.Resolve();
    }
}
