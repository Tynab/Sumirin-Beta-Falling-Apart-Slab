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
        internal static int Round500(this int num) => (int)Ceiling(num / 500d) * 500;

        /// <summary>
        /// Fixation calculate.
        /// </summary>
        /// <param name="d">Diameter.</param>
        /// <returns>Fixation.</returns>
        internal static int Fixation(int d) => Default.Rate_Fixn * d;

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
