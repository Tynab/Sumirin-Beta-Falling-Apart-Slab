using System.Media;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Resources;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Settings;

namespace Sumirin_Beta__Falling_Apart__Slab.Script
{
    public static class Constant
    {
        // sound
        public static readonly SoundPlayer SND_BACK = new(sBack);
        public static readonly SoundPlayer SND_NEXT = new(sNext);
        public static readonly SoundPlayer SND_HOV = new(sHover);
        public static readonly SoundPlayer SND_PRS = new(sPress);
        public static readonly SoundPlayer SND_CHG = new(sChange);

        // setting
        public const int MAX_XFMR_G = 30;
        public const int W_CALC = 210;
        public const int H_CALC = 230;
        public const int W_MIN_BDNG_LR = 910;

        // default
        public static readonly int L_BDNG = Default.L_Bdng;
        public static readonly int CHIDORI_HORZ = Default.Chidori_Horz;
        public static readonly int RATE_FIXN = Default.Rate_Fixn;
        public static readonly int D_SLAB = Default.D_Slab;
        public static readonly int FIXN_SLAB = Default.Fixn_D13;
        public static readonly int FIXN_D10 = Default.Fixn_D10;
        public static readonly int FIXN_D13 = Default.Fixn_D13;
        public static readonly int FIXN_D16 = Default.Fixn_D16;
        public static readonly int FIXN_D19 = Default.Fixn_D19;
        public static readonly int FIXN_D22 = Default.Fixn_D22;

        // enum
        public enum SumirinBranch
        {
            Touhoku,
            Ibaraki
        }
    }
}
