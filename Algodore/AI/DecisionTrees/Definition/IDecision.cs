namespace Algodore.AI.DecisionTrees.Definition
{
    /// <summary>
    /// Decision definition of a decision tree.
    /// Acts like intermediate level node in a decision tree.
    /// </summary>
    /// <typeparam name="T">Resolved value type.</typeparam>
    internal interface IDecision<T> : IDecisionTree<T>
    {
    }
}
