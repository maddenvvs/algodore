namespace Algodore.Algorithms.Pow
{
    using Algodore.Algorithms.Modulo;

    /// <summary>
    /// Power to integer extension method.
    /// </summary>
    public static class PowExtensions
    {
        /// <summary>
        /// Compute number in the power of pow.
        /// </summary>
        /// <param name="number">Number.</param>
        /// <param name="pow">Value of power.</param>
        /// <returns>Number in the power of pow.</returns>
        public static int Pow(this int number, int pow)
        {
            var res = 1;
            while (pow > 0)
            {
                if (pow % 2 == 1)
                {
                    res *= number;
                }
                number *= number;
                pow >>= 1;
            }
            return res;
        }

        /// <summary>
        /// Compute number in the power of pow (recursive implementation).
        /// </summary>
        /// <param name="number">Number.</param>
        /// <param name="pow">Value of power.</param>
        /// <returns>Number in the power of pow.</returns>
        public static int PowRecursive(this int number, int pow)
        {
            if (pow == 0) return 1;

            var temp = number.PowRecursive(pow / 2);
            temp *= temp;

            if (pow % 2 == 1)
            {
                temp *= number;
            }

            return temp;
        }

        /// <summary>
        /// Compute number in the power of pow modulo mod.
        /// </summary>
        /// <param name="number">Number.</param>
        /// <param name="pow">Value of power.</param>
        /// <param name="mod">Modulo value.</param>
        /// <returns>Number in the power of pow modulo mod.</returns>
        public static int PowMod(this int number, int pow, int mod)
        {
            var res = 1.Mod(mod);
            while (pow > 0)
            {
                if (pow % 2 == 1)
                {
                    res = (res * number).Mod(mod);
                }
                number = (number * number).Mod(mod);
                pow >>= 1;
            }
            return res;
        }
    }
}
