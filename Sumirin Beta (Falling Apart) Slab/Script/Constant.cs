using System.Media;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Resources;

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

        // enum
        public enum SumirinBranch
        {
            Touhoku,
            Ibaraki
        }
    }
}
