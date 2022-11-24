using System.Windows.Forms;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant;
using static System.Windows.Forms.DockStyle;
using static System.Windows.Forms.HorizontalAlignment;
using static System.Windows.Forms.Keys;

namespace Sumirin_Beta__Falling_Apart__Slab.Screen
{
    public partial class FrmResult
    {
        #region Rtx
        // rtx text align
        private void Rtx_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            var rtx = (RichTextBox)sender;
            // horizontal
            rtx.SelectAll();
            rtx.SelectionAlignment = Center;
            rtx.DeselectAll();
            // vertical
            rtx.Height = e.NewRectangle.Height;
            var cH = rtx.Height;
            var pH = rtx.Parent.Height;
            if (cH < pH - 20)
            {
                rtx.Top = (pH - cH) / 2 + 20;
            }
            else
            {
                rtx.Dock = Fill;
            }
        }
        #endregion

        #region Other
        // all key down
        private void Ctrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Escape)
            {
                // main
                Close();
                // sound
                SND_CHG.PlaySync();
            }
        }
        #endregion
    }
}
