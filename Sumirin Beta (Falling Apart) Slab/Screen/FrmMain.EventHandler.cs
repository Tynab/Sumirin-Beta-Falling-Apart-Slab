using Sumirin_Beta__Falling_Apart__Slab.Control;
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
        // chkAS checked changed
        private void ChkAS_CheckedChanged(object sender, EventArgs e)
        {
            // sound
            SND_PRS.Play();
            // main
            var chk = (CheckBox)sender;
            var id = _chkASs.IndexOf(chk);
            _pnlISs[id].Enabled = chk.Checked;
            // foward focus
            if (_pnlISs[id].Enabled)
            {
                SelectNextControl(_pnlISs[id], true, true, true, true);
            }
            // for next side
            var idNext = id + 1;
            if (idNext < _pnlASs.Count && idNext is not (_id1stSH or _id1stSV))
            {
                _pnlASs[idNext].Enabled = chk.Checked;
                if (!chk.Checked)
                {
                    _chkASs[idNext].Checked = false;
                }
            }
        }

        // chkAR checked changed
        private void ChkAR_CheckedChanged(object sender, EventArgs e)
        {
            // sound
            SND_PRS.Play();
            // main
            var chk = (CheckBox)sender;
            var id = _chkARs.IndexOf(chk);
            _pnlIRs[id].Enabled = chk.Checked;
            // foward focus
            if (_pnlIRs[id].Enabled)
            {
                SelectNextControl(_pnlIRs[id], true, true, true, true);
            }
            // for next side
            var idNext = id + 1;
            if (idNext < _pnlARs.Count && idNext is not (_id1stRH or _id1stRV))
            {
                _pnlARs[idNext].Enabled = chk.Checked;
                if (!chk.Checked)
                {
                    _chkARs[idNext].Checked = false;
                }
            }
        }

        // chkROn checked changed
        private void ChkROn_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            if (chk.Checked)
            {
                _chkROffs[_chkROns.IndexOf(chk)].Checked = false;
            }
        }

        // chkROff checked changed
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
                case C:
                {
                    var hNud = nud.Height;
                    var ptCalc = nud.FindForm().PointToClient(nud.Parent.PointToScreen(new Point(nud.Location.X, nud.Location.Y + hNud)));
                    var wCalc = 210;
                    var xCalc = ptCalc.X;
                    var yCalc = ptCalc.Y;
                    if (xCalc + wCalc > Width)
                    {
                        ptCalc = new Point(xCalc - wCalc, yCalc);
                    }
                    var hCalc = 220;
                    if (ptCalc.Y + hCalc > Height)
                    {
                        ptCalc = new Point(xCalc, yCalc - hNud - hCalc);
                    }
                    if (_ctrlCalculator == null)
                    {
                        _ctrlCalculator = new Calculator
                        {
                            Location = ptCalc
                        };
                        Controls.Add(_ctrlCalculator);
                        _ctrlCalculator.BringToFront();
                    }
                    else
                    {
                        if (ptCalc != _ctrlCalculator.Location)
                        {
                            //_ctrlCalculator.Dispose();
                            _ctrlCalculator.Location = ptCalc;
                        }
                    }
                    _ctrlCalculator.Select();
                    //nud.Enabled = false;
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
        // ctrl slab info key down
        private void CtrlIS_KeyDown(object sender, KeyEventArgs e)
        {
            var ctrl = (System.Windows.Forms.Control)sender;
            var id = GetIdFromCtrlIS(ctrl);
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
                        if (id + 1 < _chkASs.Count)
                        {
                            _chkASs[(int)id + 1].Checked = true;
                        }
                        break;
                    }
                    case X:
                    {
                        // mute nud
                        if (ctrl is NumericUpDown)
                        {
                            e.SuppressKeyPress = true;
                        }
                        // close current area without first
                        if (id is not (_id1stSH or _id1stSV))
                        {
                            _chkASs[(int)id].Checked = false;
                        }
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
                        _nudWSs[(int)id].Select();
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
                        _nudHSs[(int)id].Select();
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
                        _chkBLSs[(int)id].Checked = !_chkBLSs[(int)id].Checked;
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
                        _chkBRSs[(int)id].Checked = !_chkBRSs[(int)id].Checked;
                        break;
                    }
                }
            }
        }

        // ctrl reinforcement info key down
        private void CtrlIR_KeyDown(object sender, KeyEventArgs e)
        {
            var ctrl = (System.Windows.Forms.Control)sender;
            var id = GetIdFromCtrlIR(ctrl);
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
                        if (id + 1 < _chkARs.Count)
                        {
                            _chkARs[(int)id + 1].Checked = true;
                        }
                        break;
                    }
                    case X:
                    {
                        // mute nud
                        if (ctrl is NumericUpDown)
                        {
                            e.SuppressKeyPress = true;
                        }
                        // close current area
                        _chkARs[(int)id].Checked = false;
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
                        _nudWRs[(int)id].Select();
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
                        _nudHRs[(int)id].Select();
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
                        _nudDRs[(int)id].Select();
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
                        _chkBLRs[(int)id].Checked = !_chkBLRs[(int)id].Checked;
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
                        _chkBRRs[(int)id].Checked = !_chkBRRs[(int)id].Checked;
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
                        _chkFLRs[(int)id].Checked = !_chkFLRs[(int)id].Checked;
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
                        _chkFRRs[(int)id].Checked = !_chkFRRs[(int)id].Checked;
                        break;
                    }
                }
            }
        }
        #endregion
    }
}
