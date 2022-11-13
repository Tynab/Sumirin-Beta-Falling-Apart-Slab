using System.Media;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Resources;

namespace Sumirin_Beta__Falling_Apart__Slab.Script
{
    internal static class Constant
    {
        // sound
        internal static readonly SoundPlayer SND_BACK = new(sBack);
        internal static readonly SoundPlayer SND_NEXT = new(sNext);
        internal static readonly SoundPlayer SND_HOV = new(sHover);
        internal static readonly SoundPlayer SND_PRS = new(sPress);
        internal static readonly SoundPlayer SND_CHG = new(sChange);

        // enum
        internal enum AreaBranch
        {
            Touhoku,
            Ibaraki
        }
    }
}
