using System;

namespace Sumirin_Beta__Falling_Apart__Slab.Control
{
    public partial class Calculator
    {
        #region Btn
        // btn num click
        private void BtnN_Click(object sender, EventArgs e) => UnBlk();

        // btn all click
        private void BtnAll_Click(object sender, EventArgs e)
        {
            rtxDetail.Text = _detail;
            lblResult.Text = "0";
        }
        #endregion
    }
}
