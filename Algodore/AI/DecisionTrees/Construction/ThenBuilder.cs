namespace Algodore.AI.DecisionTrees.Construction
{
    using System;
    using Algodore.AI.DecisionTrees.Definition;

    /// <summary>
    /// Decision tree condition/consequence construction helper class.
    /// </summary>
    public sealed class ThenBuilder
    {
        private IDecisionTree<bool> condition;

        /// <summary>
        /// Create new builder instance.
        /// </summary>
        /// <param name="condition">Condition decision tree.</param>
        internal ThenBuilder(IDecisionTree<bool> condition)
        {
            this.condition = condition;
        }

        /// <summary>
        /// Provide consequent value to if/then/else decision tree.
        /// </summary>
        /// <param name="consequent">Consequent value.</param>
        /// <typeparam name="T">Resolved value type.</typeparam>
        /// <returns>Alternative builder.</returns>
        public ElseBuilder<T> Then<T>(T consequent)
            => this.Then(DecisionTree.FromResult(consequent));

        /// <summary>
        /// Provide consequent factory to if/then/else decision tree.
        /// </summary>
        /// <param name="consequent">Consequent value factory.</param>
        /// <typeparam name="T">Resolved value type.</typeparam>
        /// <returns>Alternative builder.</returns>
        public ElseBuilder<T> Then<T>(Func<T> consequent)
            => this.Then(DecisionTree.FromResult(consequent));

        /// <summary>
        /// Provide consequent decision tree to if/then/else decision tree.
        /// </summary>
        /// <param name="consequent">Decision tree as consequent.</param>
        /// <typeparam name="T">Resolved value type.</typeparam>
        /// <returns>Alternative builder.</returns>
        public ElseBuilder<T> Then<T>(IDecisionTree<T> consequent)
            => new ElseBuilder<T>(this.condition, consequent);

        /// <summary>
        /// Create new condition by using AND logical function.
        /// </summary>
        /// <param name="condition">Factory value condition.</param>
        /// <returns>Builder itself.</returns>
        public ThenBuilder And(Func<bool> condition)
            => this.And(DecisionTree.FromResult(condition));

        /// <summary>
        /// Create new condition by using AND logical function.
        /// </summary>
        /// <param name="condition">Decision tree condition.</param>
        /// <returns>Builder itself.</returns>
        public ThenBuilder And(IDecisionTree<bool> condition)
        {
            this.condition = new AndCondition(this.condition, condition);
            return this;
        }

        /// <summary>
        /// Create new condition by using OR logical function.
        /// </summary>
        /// <param name="condition">Factory value condition.</param>
        /// <returns>Builder itself.</returns>
        public ThenBuilder Or(Func<bool> condition)
            => this.Or(DecisionTree.FromResult(condition));

        /// <summary>
        /// Create new condition by using OR logical function.
        /// </summary>
        /// <param name="condition">Decision tree condition.</param>
        /// <returns>Builder itself.</returns>
        public ThenBuilder Or(IDecisionTree<bool> condition)
        {
            this.condition = new OrCondition(this.condition, condition);
            return this;
        }
    }
}
