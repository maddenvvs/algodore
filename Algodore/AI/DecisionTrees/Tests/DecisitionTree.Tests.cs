#pragma warning disable SA1600
#pragma warning disable CS1591

namespace Algodore.AI.DecisionTrees.Tests
{
    using Xunit;

    public class DecisionTreeWithOneNode
    {
        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(-100)]
        public void CanBeConstructedWithPredefinedValue(int value)
        {
            var tree = DecisionTree.FromResult(value);

            Assert.Equal(value, tree.Resolve());
        }

        [Theory]
        [InlineData("some")]
        [InlineData("new")]
        [InlineData("data")]
        public void CanBeConstructedWithInlineFunction(string value)
        {
            var tree = DecisionTree.FromResult(() => value);

            Assert.Equal(value, tree.Resolve());
        }

        [Fact]
        public void WhenConstructedWithFunction_ThenFunctionInvokedOnEveryResolve()
        {
            var callCount = 0;
            var tree = DecisionTree.FromResult(() => callCount++);

            tree.Resolve();
            tree.Resolve();
            tree.Resolve();

            Assert.Equal(3, callCount);
        }
    }

    public class IfThenElseDecisitionTree
    {
        [Fact]
        public void WhenConditionIsTrue_ThenReturnConsequence()
        {
            var tree = DecisionTree.New()
                .If(() => true)
                .Then(1)
                .Else(0);

            Assert.Equal(1, tree.Resolve());
        }

        [Fact]
        public void WhenConditionIsFalse_ThenReturnAlternative()
        {
            var tree = DecisionTree.New()
                .If(() => false)
                .Then("yes")
                .Else("no");

            Assert.Equal("no", tree.Resolve());
        }

        [Fact]
        public void ConditionsCanBeChainedWithAnd()
        {
            var tree = DecisionTree.New()
                .If(() => true).And(() => false)
                .Then(1)
                .Else(0);

            Assert.Equal(0, tree.Resolve());
        }

        [Fact]
        public void ConditionsCanBeChainedWithOr()
        {
            var tree = DecisionTree.New()
                .If(() => false).Or(() => true)
                .Then(1)
                .Else(0);

            Assert.Equal(1, tree.Resolve());
        }
    }

    public class DecisitionTreeComposition
    {
        [Fact]
        public void DecisionTreeCanBeUsedInConsequence()
        {
            var consequenceTree = DecisionTree.New()
                .If(() => true).Then(1).Else(0);
            var tree = DecisionTree.New()
                .If(() => true).Then(consequenceTree).Else(100);

            Assert.Equal(1, tree.Resolve());
        }

        [Fact]
        public void DecisionTreeCanBeUsedInAlternatives()
        {
            var alternativeTree = DecisionTree.New()
                .If(() => true).Then(1).Else(0);
            var tree = DecisionTree.New()
                .If(() => false).Then(100).Else(alternativeTree);

            Assert.Equal(1, tree.Resolve());
        }

        [Fact]
        public void DecisionTreeCanBeUsedInCondition()
        {
            var conditionTree = DecisionTree.New()
                .If(() => true).Then(true).Else(false);
            var tree = DecisionTree.New()
                .If(conditionTree).Then(100).Else(-100);

            Assert.Equal(100, tree.Resolve());
        }
    }
}
