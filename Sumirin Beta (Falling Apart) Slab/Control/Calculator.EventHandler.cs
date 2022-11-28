using System;
using System.Windows.Forms;
using static System.Windows.Forms.Keys;

namespace Sumirin_Beta__Falling_Apart__Slab.Control
{
    public partial class Calculator
    {
        #region Ctrl
        // ctrl all key down
        private void Ctrl_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case D0:
                case NumPad0:
                {
                    BtnN0_Click(btnN0, null);
                    break;
                }
                case D1:
                case NumPad1:
                {
                    BtnN1_Click(btnN1, new EventArgs());
                    break;
                }
                case D2:
                case NumPad2:
                {
                    BtnN2_Click(btnN2, EventArgs.Empty);
                    break;
                }
                case D3:
                case NumPad3:
                {
                    btnN3.PerformClick();
                    break;
                }
                case D4:
                case NumPad4:
                {
                    BtnN4_Click(btnN4, null);
                    break;
                }
                case D5:
                case NumPad5:
                {
                    BtnN5_Click(btnN5, null);
                    break;
                }
                case D6:
                case NumPad6:
                {
                    BtnN6_Click(btnN6, null);
                    break;
                }
                case D7:
                case NumPad7:
                {
                    BtnN7_Click(btnN7, null);
                    break;
                }
                case D8:
                case NumPad8:
                {
                    BtnN8_Click(btnN8, null);
                    break;
                }
                case D9:
                case NumPad9:
                {
                    BtnN9_Click(btnN9, null);
                    break;
                }
                case Keys.Decimal:
                case OemPeriod:
                {
                    BtnDot_Click(btnDot, null);
                    break;
                }
                case Escape:
                {
                    BtnC_Click(btnC, null);
                    break;
                }
                case Back:
                {
                    BtnBksp_Click(btnBksp, null);
                    break;
                }
                case Add:
                case Oemplus:
                {
                    BtnPlus_Click(btnPlus, null);
                    break;
                }
                case Subtract:
                case OemMinus:
                {
                    BtnMinus_Click(btnMinus, null);
                    break;
                }
                case Keys.Enter:
                {
                    BtnReturn_Click(btnReturn, null);
                    break;
                }
            }
        }
        #endregion

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
