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
        /// Process rebar horizontal.
        /// </summary>
        /// <param name="lRebarL">Rebar left length.</param>
        /// <param name="lRebarR">Rebar right length.</param>
        /// <param name="chidoriHorz">Chidori horizontal.</param>
        internal static void PrcsRebarHorz(ref int lRebarL, ref int lRebarR, int chidoriHorz)
        {
            while (lRebarL < lRebarR + chidoriHorz)
            {
                lRebarL += 500;
                lRebarR -= 500;
            }
        }
    }
}
