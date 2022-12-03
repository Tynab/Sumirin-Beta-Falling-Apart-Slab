using System;
using System.Windows.Forms;
using static System.Drawing.Color;
using static System.Windows.Forms.Keys;

namespace Sumirin_Beta__Falling_Apart__Slab.Control
{
    public partial class Calculator
    {
        #region Ctrl
        // ctrl all key down
        private void Ctrl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case D0:
                case NumPad0:
                {
                    e.SuppressKeyPress = true;
                    BtnN0_Click(btnN0, null);
                    BtnNAct(btnN0);
                    break;
                }
                case D1:
                case NumPad1:
                {
                    e.SuppressKeyPress = true;
                    BtnN1_Click(btnN1, null);
                    BtnNAct(btnN1);
                    break;
                }
                case D2:
                case NumPad2:
                {
                    e.SuppressKeyPress = true;
                    BtnN2_Click(btnN2, null);
                    BtnNAct(btnN2);
                    break;
                }
                case D3:
                case NumPad3:
                {
                    e.SuppressKeyPress = true;
                    BtnN3_Click(btnN3, null);
                    BtnNAct(btnN3);
                    break;
                }
                case D4:
                case NumPad4:
                {
                    e.SuppressKeyPress = true;
                    BtnN4_Click(btnN4, null);
                    BtnNAct(btnN4);
                    break;
                }
                case D5:
                case NumPad5:
                {
                    e.SuppressKeyPress = true;
                    BtnN5_Click(btnN5, null);
                    BtnNAct(btnN5);
                    break;
                }
                case D6:
                case NumPad6:
                {
                    e.SuppressKeyPress = true;
                    BtnN6_Click(btnN6, null);
                    BtnNAct(btnN6);
                    break;
                }
                case D7:
                case NumPad7:
                {
                    e.SuppressKeyPress = true;
                    BtnN7_Click(btnN7, null);
                    BtnNAct(btnN7);
                    break;
                }
                case D8:
                case NumPad8:
                {
                    e.SuppressKeyPress = true;
                    BtnN8_Click(btnN8, null);
                    BtnNAct(btnN8);
                    break;
                }
                case D9:
                case NumPad9:
                {
                    e.SuppressKeyPress = true;
                    BtnN9_Click(btnN9, null);
                    BtnNAct(btnN9);
                    break;
                }
                case Keys.Decimal:
                case OemPeriod:
                {
                    e.SuppressKeyPress = true;
                    if (btnDot.Enabled)
                    {
                        BtnDot_Click(btnDot, null);
                        BtnAll_Click(btnDot, null);
                    }
                    break;
                }
                case Escape:
                {
                    e.SuppressKeyPress = true;
                    if (btnC.Enabled)
                    {
                        BtnC_Click(btnC, null);
                        BtnAll_Click(btnC, null);
                    }
                    break;
                }
                case Back:
                {
                    e.SuppressKeyPress = true;
                    if (btnBksp.Enabled)
                    {
                        BtnBksp_Click(btnBksp, null);
                        BtnAll_Click(btnBksp, null);
                    }
                    break;
                }
                case Add:
                case Oemplus:
                {
                    e.SuppressKeyPress = true;
                    if (btnPlus.Enabled)
                    {
                        BtnPlus_Click(btnPlus, null);
                        BtnAll_Click(btnPlus, null);
                    }
                    break;
                }
                case Subtract:
                case OemMinus:
                {
                    e.SuppressKeyPress = true;
                    if (btnMinus.Enabled)
                    {
                        BtnMinus_Click(btnMinus, null);
                        BtnAll_Click(btnMinus, null);
                    }
                    break;
                }
                case Keys.Enter:
                {
                    e.SuppressKeyPress = true;
                    if (btnReturn.Enabled)
                    {
                        BtnReturn_Click(btnReturn, null);
                        BtnAll_Click(btnReturn, null);
                    }
                    break;
                }
            }
        }

        // ctrl dispose
        private void OnDispose(object sender, EventArgs e)
        {
            _nud.BackColor = White;
            _nud.Select();
        }
        #endregion

        #region Btn
        // btn num click
        private void BtnN_Click(object sender, EventArgs e) => UnBlk();

        // btn all click
        private void BtnAll_Click(object sender, EventArgs e) => rtxDetail.Text = _detail;
        #endregion
    }
}
