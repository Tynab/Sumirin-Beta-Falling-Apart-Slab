using static Sumirin_Beta__Falling_Apart__Slab.Properties.Settings;
using static System.Math;

namespace Sumirin_Beta__Falling_Apart__Slab.Script
{
    internal static class Common
    {
        /// <summary>
        /// Round 10.
        /// </summary>
        /// <param name="num">Number.</param>
        /// <returns>Rounded number.</returns>
        internal static int Round10(this double num) => (int)Ceiling(num / 10) * 10;

        /// <summary>
        /// Round 500.
        /// </summary>
        /// <param name="num">Number.</param>
        /// <returns>Rounded number.</returns>
        internal static int Round500(this double num) => (int)Ceiling(num / 500) * 500;

        /// <summary>
        /// Fixation calculate.
        /// </summary>
        /// <param name="d">Diameter.</param>
        /// <returns>Fixation.</returns>
        internal static int Fixation(int d) => Default.Rate_Fixn * d;
    }
}
