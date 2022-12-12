using System.Media;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Resources;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Settings;
using static System.Environment;
using static System.Environment.SpecialFolder;

namespace Sumirin_Beta__Falling_Apart__Slab.Script
{
    public static class Constant
    {
        // path
        public static readonly string BACK_PATH = GetFolderPath(ApplicationData);
        public static readonly string FRNT_PATH = $@"{BACK_PATH}\{co_name}";
        public static readonly string FILE_SETUP_NAME = $"{app_name} Setup.msi";
        public static readonly string FILE_SETUP_ADR = $@"{FRNT_PATH}\{FILE_SETUP_NAME}";

        // sound
        public static readonly SoundPlayer SND_BACK = new(sBack);
        public static readonly SoundPlayer SND_NEXT = new(sNext);
        public static readonly SoundPlayer SND_HOV = new(sHover);
        public static readonly SoundPlayer SND_PRS = new(sPress);
        public static readonly SoundPlayer SND_CHG = new(sChange);

        // setting
        public const int TIME_OUT = 7000;
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
