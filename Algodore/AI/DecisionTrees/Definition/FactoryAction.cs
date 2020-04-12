namespace Algodore.AI.DecisionTrees.Definition
{
    using System;

    /// <summary>
    /// Factory-based decision tree.
    /// </summary>
    /// <typeparam name="T">Resolved value type.</typeparam>
    internal sealed class FactoryAction<T> : IAction<T>
    {
        private readonly Func<T> factory;

        /// <summary>
        /// Create new factory-based decision tree.
        /// </summary>
        /// <param name="factory">Factory function.</param>
        internal FactoryAction(Func<T> factory) => this.factory = factory;

        /// <summary>
        /// Invoke factory function and return its value.
        /// </summary>
        /// <returns>Resolved value.</returns>
        public T Resolve() => this.factory();
    }
}
