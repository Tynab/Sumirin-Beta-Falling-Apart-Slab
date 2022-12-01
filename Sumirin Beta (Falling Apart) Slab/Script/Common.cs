using static Sumirin_Beta__Falling_Apart__Slab.Properties.Settings;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant;
using static System.Convert;
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
        internal static int Round10<T>(this T num) => (int)Ceiling(ToDouble(num) / 10) * 10;

        /// <summary>
        /// Round 500.
        /// </summary>
        /// <param name="num">Number.</param>
        /// <returns>Rounded number.</returns>
        internal static int Round500<T>(this T num) => (int)Ceiling(ToDouble(num) / 500) * 500;

        /// <summary>
        /// Ceiling 50.
        /// </summary>
        /// <param name="num">Number.</param>
        /// <returns>Ceiling number.</returns>
        internal static int Ceiling50<T>(this T num) => (int)Ceiling((ToDouble(num) + 1) / 50) * 50;

        /// <summary>
        /// Number to G span.
        /// </summary>
        /// <param name="num">Number.</param>
        /// <returns>Number to G span.</returns>
        internal static decimal ToGSpan(this decimal num) => num < MAX_XFMR_G ? num * Default.Span : num;
    }
}
