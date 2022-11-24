using System;
using System.Windows.Forms;
using YANF.Control;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant;
using static YANF.Script.YANEvent;

namespace Sumirin_Beta__Falling_Apart__Slab.Script
{
    internal static class EventHandler
    {
        #region Rdo
        // rdo checked changed
        internal static void Rdo_CheckedChanged(object sender, EventArgs e) => ((YANRdo)sender).TabStop = false;
        #endregion

        #region Nud
        // nud enter
        internal static void Nud_Enter(object sender, EventArgs e)
        {
            var nud = (NumericUpDown)sender;
            nud.Select(0, nud.Text.Length);
        }

        // nud leave
        internal static void Nud_Leave(object sender, EventArgs e)
        {
            var nud = (NumericUpDown)sender;
            if (string.IsNullOrWhiteSpace(nud.Text))
            {
                nud.Text = nud.Value.ToString();
            }
        }
        #endregion

        #region Other
        // Mod MoveFrm event
        internal static void MoveFrmMod_MouseDown(object sender, MouseEventArgs e)
        {
            // base
            MoveFrm_MouseDown(sender, e);
            // sound
            SND_CHG.Play();
        }
        #endregion
    }
}
