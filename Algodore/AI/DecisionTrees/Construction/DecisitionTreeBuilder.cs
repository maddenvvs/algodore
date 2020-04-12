namespace Algodore.AI.DecisionTrees.Construction
{
    using System;
    using Algodore.AI.DecisionTrees.Definition;

    /// <summary>
    /// Decision tree construction helper class.
    /// </summary>
    public sealed class DecisionTreeBuilder
    {
        /// <summary>
        /// Create new builder instance.
        /// </summary>
        internal DecisionTreeBuilder()
        {
        }

        /// <summary>
        /// Start new if/then/else decision tree by providing a condition (or 'if') function.
        /// </summary>
        /// <param name="condition">Condition function.</param>
        /// <returns>Condition builder.</returns>
        public ThenBuilder If(Func<bool> condition)
            => this.If(DecisionTree.FromResult(condition));

        /// <summary>
        /// Start new if/then/else decision tree by providing a decision tree.
        /// Provided tree is used in place of 'if' part.
        /// </summary>
        /// <param name="condition">Condition decision tree.</param>
        /// <returns>Condition builder.</returns>
        public ThenBuilder If(IDecisionTree<bool> condition)
            => new ThenBuilder(condition);
    }
}
