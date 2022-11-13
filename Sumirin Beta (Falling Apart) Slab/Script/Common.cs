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
        internal static double Round10(this double num) => Ceiling(num / 10) * 10;

        /// <summary>
        /// Round 500.
        /// </summary>
        /// <param name="num">Number.</param>
        /// <returns>Rounded number.</returns>
        internal static double Round500(this double num) => Ceiling(num / 500) * 500;

        /// <summary>
        /// Fixation calculate.
        /// </summary>
        /// <param name="d">Diameter.</param>
        /// <returns>Fixation.</returns>
        internal static double Fixation(double d) => Default.Rate_Fixn * d;
    }
}
