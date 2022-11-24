using System;
using System.Drawing;
using System.Windows.Forms;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant;
using static System.Drawing.Color;
using static System.Drawing.FontStyle;
using static System.Windows.Forms.Keys;

namespace Sumirin_Beta__Falling_Apart__Slab.Screen
{
    public partial class FrmMain
    {
        #region Chk
        // chkA checked changed
        private void ChkA_CheckedChanged(object sender, EventArgs e)
        {
            // sound
            SND_PRS.Play();
            // main
            var chk = (CheckBox)sender;
            var id = _chkAs.IndexOf(chk);
            _pnlIs[id].Enabled = chk.Checked;
            // foward focus
            if (_pnlIs[id].Enabled)
            {
                SelectNextControl(_pnlIs[id], true, true, true, true);
            }
            // for next side
            _pnlAs[id + 1].Enabled = chk.Checked;
            if (!chk.Checked)
            {
                _chkAs[id + 1].Checked = false;
            }
        }

        // chkROFf checked changed
        private void ChkROff_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            if (chk.Checked)
            {
                _chkROns[_chkROffs.IndexOf(chk)].Checked = false;
            }
        }
        #endregion

        #region Nud
        // nud title enter
        private void NudTit_Enter(object sender, EventArgs e)
        {
            var id = _nudTits.IndexOf((NumericUpDown)sender);
            _lblTits[id].ForeColor = OrangeRed;
            _lblTits[id].Font = new Font(_lblTits[id].Font, Bold);
        }

        // nud title leave
        private void NudTit_Leave(object sender, EventArgs e)
        {
            var id = _nudTits.IndexOf((NumericUpDown)sender);
            _lblTits[id].ForeColor = Black;
            _lblTits[id].Font = new Font(_lblTits[id].Font, Regular);
        }

        // nud key down
        private void Nud_KeyDown(object sender, KeyEventArgs e)
        {
            var nud = (NumericUpDown)sender;
            switch (e.KeyCode)
            {
                case Space:
                {
                    nud.ResetText();
                    break;
                }
                case Tab:
                {
                    nud.Value = decimal.Parse(nud.Text);
                    break;
                }
                case Keys.Enter:
                {
                    e.SuppressKeyPress = true;
                    break;
                }
            }
        }

        // nud key up
        private void Nud_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        // nud G value changed
        private void NudG_ValueChanged(object sender, EventArgs e)
        {
            var nud = (NumericUpDown)sender;
            var val = nud.Value;
            if (val < MAX_XFMR_G)
            {
                nud.Value = val * _span;
            }
        }
        #endregion

        #region Other
        // ctrl info key down
        private void CtrlI_KeyDown(object sender, KeyEventArgs e)
        {
            var ctrl = (System.Windows.Forms.Control)sender;
            var id = GetIdFromCtrlI(ctrl);
            if (id != null)
            {
                switch (e.KeyCode)
                {
                    case O:
                    {
                        // mute nud
                        if (ctrl is NumericUpDown)
                        {
                            e.SuppressKeyPress = true;
                        }
                        // open next area
                        _chkAs[(int)id + 1].Checked = true;
                        break;
                    }
                    case C:
                    {
                        // mute nud
                        if (ctrl is NumericUpDown)
                        {
                            e.SuppressKeyPress = true;
                        }
                        // close current area
                        _chkAs[(int)id].Checked = false;
                        break;
                    }
                    case W:
                    {
                        // mute nud
                        if (ctrl is NumericUpDown)
                        {
                            e.SuppressKeyPress = true;
                        }
                        // focus W
                        _nudWs[(int)id].Select();
                        break;
                    }
                    case H:
                    {
                        // mute nud
                        if (ctrl is NumericUpDown)
                        {
                            e.SuppressKeyPress = true;
                        }
                        // focus H
                        _nudHs[(int)id].Select();
                        break;
                    }
                    case D:
                    {
                        // mute nud
                        if (ctrl is NumericUpDown)
                        {
                            e.SuppressKeyPress = true;
                        }
                        // focus D
                        _nudDs[(int)id].Select();
                        break;
                    }
                    case L:
                    {
                        // mute nud
                        if (ctrl is NumericUpDown)
                        {
                            e.SuppressKeyPress = true;
                        }
                        // change state bending left
                        _chkBLs[(int)id].Checked = !_chkBLs[(int)id].Checked;
                        break;
                    }
                    case R:
                    {
                        // mute nud
                        if (ctrl is NumericUpDown)
                        {
                            e.SuppressKeyPress = true;
                        }
                        // change state bending right
                        _chkBRs[(int)id].Checked = !_chkBRs[(int)id].Checked;
                        break;
                    }
                    case T:
                    {
                        // mute nud
                        if (ctrl is NumericUpDown)
                        {
                            e.SuppressKeyPress = true;
                        }
                        // change state bending left
                        _chkFLs[(int)id].Checked = !_chkFLs[(int)id].Checked;
                        break;
                    }
                    case P:
                    {
                        // mute nud
                        if (ctrl is NumericUpDown)
                        {
                            e.SuppressKeyPress = true;
                        }
                        // change state bending right
                        _chkFRs[(int)id].Checked = !_chkFRs[(int)id].Checked;
                        break;
                    }
                }
            }
        }
        #endregion
    }
}
