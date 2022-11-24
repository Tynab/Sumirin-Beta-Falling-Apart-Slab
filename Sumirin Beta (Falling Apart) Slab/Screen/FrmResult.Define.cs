using System.Collections.Generic;
using System.Windows.Forms;

namespace Sumirin_Beta__Falling_Apart__Slab.Screen
{
    public partial class FrmResult
    {
        // Initialize items
        private void InitItems()
        {
            InitRtxSmryRs();
            InitLblSmryRs();
        }

        #region Rtx
        private List<RichTextBox> _rtxSmryRs;
        // Initialize list chkARH
        private void InitRtxSmryRs() => _rtxSmryRs = new List<RichTextBox>
            {
                rtxSmryDR1,
                rtxSmryDR2,
                rtxSmryDR3,
                rtxSmryDR4,
                rtxSmryDR5,
                rtxSmryDR6
            };
        #endregion

        #region Rtx
        private List<Label> _lblSmryRs;
        // Initialize list chkARH
        private void InitLblSmryRs() => _lblSmryRs = new List<Label>
            {
                lblSmryDR1,
                lblSmryDR2,
                lblSmryDR3,
                lblSmryDR4,
                lblSmryDR5,
                lblSmryDR6
            };
        #endregion
    }
}
