namespace Algodore.AI.DecisionTrees.Definition
{
    /// <summary>
    /// Action definition of decision tree. It behaves like a leaf in decision tree.
    /// </summary>
    /// <typeparam name="T">Resolved value type.</typeparam>
    internal interface IAction<T> : IDecisionTree<T>
    {
    }
}
