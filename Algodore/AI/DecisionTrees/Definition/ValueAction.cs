namespace Algodore.AI.DecisionTrees.Definition
{
    /// <summary>
    /// Value-based decision tree.
    /// </summary>
    /// <typeparam name="T">Resolved value type.</typeparam>
    internal sealed class ValueAction<T> : IAction<T>
    {
        private readonly T value;

        /// <summary>
        /// Create new value-based decision tree action.
        /// </summary>
        /// <param name="value">Resolved value.</param>
        internal ValueAction(T value) => this.value = value;

        /// <summary>
        /// Return the stored value.
        /// </summary>
        /// <returns>Resolved value.</returns>
        public T Resolve() => this.value;
    }
}
