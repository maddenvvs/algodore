#pragma warning disable SA1600
#pragma warning disable CS1591

namespace Algodore.Algorithms.Pow.Tests
{
    using Xunit;

    public class PowTests
    {
        [Theory]
        [InlineData(0, 0, 1)]
        [InlineData(0, 1, 0)]
        [InlineData(2, 1, 2)]
        [InlineData(2, 8, 256)]
        [InlineData(3, 4, 81)]
        public void Pow_works_well_with_non_negative_numbers(int num, int pow, int res)
        {
            Assert.Equal(res, num.Pow(pow));
        }

        [Theory]
        [InlineData(-2, 1, -2)]
        [InlineData(-2, 8, 256)]
        [InlineData(-3, 5, -243)]
        public void Pow_works_well_with_negative_numbers(int num, int pow, int res)
        {
            Assert.Equal(res, num.Pow(pow));
        }
    }

    public class PowRecursiveTests
    {
        [Theory]
        [InlineData(0, 0, 1)]
        [InlineData(0, 1, 0)]
        [InlineData(2, 1, 2)]
        [InlineData(2, 8, 256)]
        [InlineData(3, 4, 81)]
        public void Pow_works_well_with_non_negative_numbers(int num, int pow, int res)
        {
            Assert.Equal(res, num.PowRecursive(pow));
        }

        [Theory]
        [InlineData(-2, 1, -2)]
        [InlineData(-2, 8, 256)]
        [InlineData(-3, 5, -243)]
        public void Pow_works_well_with_negative_numbers(int num, int pow, int res)
        {
            Assert.Equal(res, num.PowRecursive(pow));
        }
    }

    public class PowModTests
    {
        [Theory]
        [InlineData(0, 0, 1, 0)]
        [InlineData(58, 85, 17, 11)]
        [InlineData(-117, 344, 1789, 1766)]
        public void PowMod_passes_difficult_tests(int num, int pow, int mod, int res)
        {
            Assert.Equal(res, num.PowMod(pow, mod));
        }
    }
}
