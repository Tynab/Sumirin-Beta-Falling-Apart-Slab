﻿using Sumirin_Beta__Falling_Apart__Slab.Control;
using Sumirin_Beta__Falling_Apart__Slab.Script;
using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant;
using static System.Drawing.Color;
using static System.Drawing.FontStyle;
using static System.Math;
using static System.Windows.Forms.Keys;
using static YANF.Script.YANConstant;

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
            _nudASs[id].Enabled = chk.Checked;
            _nudASs[id].Value = id > 0 ? _nudASs[id - 1].Value : 1;
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
            _nudARs[id].Enabled = chk.Checked;
            _nudARs[id].Value = id > 0 ? _nudARs[id - 1].Value : 1;
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
                    e.SuppressKeyPress = true;
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
                    e.SuppressKeyPress = true;
                    // auto change location
                    var wNud = nud.Width;
                    var hNud = nud.Height;
                    var ptCalc = nud.FindForm().PointToClient(nud.Parent.PointToScreen(new Point(nud.Location.X, nud.Location.Y + hNud)));
                    var xCalc = ptCalc.X;
                    var yCalc = ptCalc.Y;
                    if (xCalc + W_CALC > Width)
                    {
                        ptCalc = new Point(xCalc + wNud - W_CALC, yCalc);
                        //Width = xCalc + W_CALC;
                    }
                    if (ptCalc.Y + H_CALC > Height)
                    {
                        ptCalc = new Point(xCalc, yCalc - hNud - H_CALC);
                        //Height = yCalc + H_CALC;
                    }
                    // re-init
                    if (_ctrlCalculator == null || _ctrlCalculator.IsDisposed)
                    {
                        _ctrlCalculator = new Calculator(nud)
                        {
                            Location = ptCalc
                        };
                        Controls.Add(_ctrlCalculator);
                        _ctrlCalculator.BringToFront();
                    }
                    _ctrlCalculator.txtDetail.Select();
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
            nud.Value = nud.Value.ToGSpan();
        }

        // nud slab area value changed
        private void NudAS_ValueChanged(object sender, EventArgs e)
        {
            var maxNudAS = Max(1, _nudASs.Max(z => z.Value)) + 1;
            _nudASs.ForEach(x =>
            {
                if (x != (NumericUpDown)sender)
                {
                    x.Maximum = maxNudAS;
                }
            });
        }

        // nud reinforcement area value changed
        private void NudAR_ValueChanged(object sender, EventArgs e)
        {
            var maxNudAS = Max(1, _nudARs.Max(z => z.Value)) + 1;
            _nudARs.ForEach(x =>
            {
                if (x != (NumericUpDown)sender)
                {
                    x.Maximum = maxNudAS;
                }
            });
        }
        #endregion

        #region Other
        // ctrl slab info key down
        private void CtrlIS_KeyDown(object sender, KeyEventArgs e)
        {
            var ctrl = (System.Windows.Forms.Control)sender;
            var id = GetIdFromCtrlIS(ctrl);
            if (id.HasValue)
            {
                switch (e.KeyCode)
                {
                    case A:
                    {
                        // mute nud
                        if (ctrl is NumericUpDown)
                        {
                            e.SuppressKeyPress = true;
                        }
                        // focus area
                        _nudASs[(int)id].Select();
                        break;
                    }
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
            if (id.HasValue)
            {
                switch (e.KeyCode)
                {
                    case A:
                    {
                        // mute nud
                        if (ctrl is NumericUpDown)
                        {
                            e.SuppressKeyPress = true;
                        }
                        // focus area
                        _nudARs[(int)id].Select();
                        break;
                    }
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
                        if (ModifierKeys == Shift)
                        {
                            _chkFLRs[(int)id].Checked = !_chkFLRs[(int)id].Checked;
                        }
                        else
                        {
                            _chkBLRs[(int)id].Checked = !_chkBLRs[(int)id].Checked;
                        }
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
                        if (ModifierKeys == Shift)
                        {
                            _chkFRRs[(int)id].Checked = !_chkFRRs[(int)id].Checked;
                        }
                        else
                        {
                            _chkBRRs[(int)id].Checked = !_chkBRRs[(int)id].Checked;
                        }
                        break;
                    }
                }
            }
        }

        // Update download progress changed
        private void Upd_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            _pct = e.ProgressPercentage;
            _ = Invoke((MethodInvoker)delegate
            {
                _dlvScrService.PublishValue(_pct, string.Format("{0} MB / {1} MB", (e.BytesReceived / 1024d / 1024d).ToString("0.00"), (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00")), (int)Ceiling(_pct * W_UPDATE_SCR / 100d));
            });
        }
        #endregion
    }
}
