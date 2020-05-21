#pragma warning disable SA1600
#pragma warning disable CS1591

namespace Algodore.Algorithms.Modulo.Tests
{
    using Xunit;

    public class ModuloTests
    {
        [Theory]
        [InlineData(0, 1, 0)]
        [InlineData(7, 4, 3)]
        [InlineData(8, 4, 0)]
        [InlineData(57, 22, 13)]
        public void Modulo_works_well_with_non_negative_numbers(int num, int mod, int res)
        {
            Assert.Equal(res, num.Mod(mod));
        }

        [Theory]
        [InlineData(-7, 6, 5)]
        [InlineData(-18, 6, 0)]
        [InlineData(-100, 23, 15)]
        public void Modulo_works_well_with_negative_numbers(int num, int mod, int res)
        {
            Assert.Equal(res, num.Mod(mod));
        }
    }
}
