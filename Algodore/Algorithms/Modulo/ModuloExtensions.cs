namespace Algodore.Algorithms.Modulo
{
    /// <summary>
    /// Modulo function extension class for integers
    /// </summary>
    public static class ModuloExtensions
    {
        /// <summary>
        /// Compute modulo function returning value between 0 (inclusive) and modulo (exclusive).
        /// </summary>
        /// <param name="number">Number.</param>
        /// <param name="modulo">Modulo.</param>
        /// <returns>Number between 0 and modulo-1.</returns>
        public static int Mod(this int number, int modulo) =>
            ((number % modulo) + modulo) % modulo;
    }
}
