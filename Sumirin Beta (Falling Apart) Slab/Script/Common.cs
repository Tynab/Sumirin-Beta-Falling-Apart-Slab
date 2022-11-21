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
        /// Ceiling 50.
        /// </summary>
        /// <param name="num">Number.</param>
        /// <returns>Ceiling number.</returns>
        internal static int Ceiling50<T>(this T num) => (int)Ceiling((ToDouble(num) + 1) / 50) * 50;

        /// <summary>
        /// Round 500.
        /// </summary>
        /// <param name="num">Number.</param>
        /// <returns>Rounded number.</returns>
        internal static int Round500<T>(this T num) => (int)Ceiling(ToDouble(num) / 500) * 500;

        /// <summary>
        /// Joint count.
        /// </summary>
        /// <param name="w">Clone W.</param>
        /// <param name="wMinCpl">W minimum match maximum length couple head rebar.</param>
        /// <param name="lRawWoodRip">Length raw wood without fixation.</param>
        /// <returns>Joint.</returns>
        internal static int JtCnt(ref double w, double wMinCpl, double lRawWoodRip)
        {
            var jt = 1;
            while (w > wMinCpl)
            {
                w -= lRawWoodRip;
                jt++;
            }
            w = w.Round500();
            return jt;
        }
    }
}
