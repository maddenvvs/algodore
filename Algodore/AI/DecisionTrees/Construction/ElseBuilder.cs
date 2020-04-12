namespace Algodore.AI.DecisionTrees.Construction
{
    using System;
    using Algodore.AI.DecisionTrees.Definition;

    /// <summary>
    /// If/then/else decision tree alternative construction helper class.
    /// </summary>
    /// <typeparam name="T">Resolved value type.</typeparam>
    public sealed class ElseBuilder<T>
    {
        private IDecisionTree<bool> condition;

        private IDecisionTree<T> consequent;

        /// <summary>
        /// Create new instance of the builder.
        /// </summary>
        /// <param name="condition">Condition decision tree.</param>
        /// <param name="consequent">Consequent decision tree.</param>
        internal ElseBuilder(IDecisionTree<bool> condition, IDecisionTree<T> consequent)
        {
            this.condition = condition;
            this.consequent = consequent;
        }

        /// <summary>
        /// Finish decision tree construction by providing alternative value.
        /// </summary>
        /// <param name="alternative">Alternative value.</param>
        /// <returns>Constructed decision tree.</returns>
        public IDecisionTree<T> Else(T alternative)
            => this.Else(DecisionTree.FromResult(alternative));

        /// <summary>
        /// Finish decision tree construction by providing alternative value factory.
        /// </summary>
        /// <param name="alternative">Alternative value factory.</param>
        /// <returns>Constructed decision tree.</returns>
        public IDecisionTree<T> Else(Func<T> alternative)
            => this.Else(DecisionTree.FromResult(alternative));

        /// <summary>
        /// Finish decision tree construction by providing decision tree as alternative.
        /// </summary>
        /// <param name="alternative">Decision tree as alternative.</param>
        /// <returns>Constructed decision tree.</returns>
        public IDecisionTree<T> Else(IDecisionTree<T> alternative)
            => new BooleanDecision<T>(
                    this.condition,
                    this.consequent,
                    alternative);
    }
}
