using System;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using YANF.Control;
using YANF.Script;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Settings;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant;
using static System.Drawing.Color;
using static System.Drawing.FontStyle;
using static System.Threading.Tasks.Task;
using static System.Windows.Forms.Keys;
using static YANF.Script.YANEvent;

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
            // link pnlI/A state
            if (_chkAs.Contains(chk))
            {
                var id = _chkAs.IndexOf(chk);
                _pnlIs[id].Enabled = chk.Checked;
                // foward focus
                if (_pnlIs[id].Enabled)
                {
                    SelectNextControl(_pnlIs[id], true, true, true, true);
                }
                _pnlAs[id + 1].Enabled = chk.Checked;
                if (!chk.Checked)
                {
                    _chkAs[id + 1].Checked = false;
                }
            }
        }
        #endregion

        #region Rdo
        // rdo checked changed
        private void Rdo_CheckedChanged(object sender, EventArgs e) => ((YANRdo)sender).TabStop = false;
        #endregion

        #region Nud
        // nud enter
        private void Nud_Enter(object sender, EventArgs e)
        {
            var nud = (NumericUpDown)sender;
            // select text
            nud.Select(0, nud.Text.Length);
            // nudTit highlight
            if (_nudTits.Contains(nud))
            {
                var id = _nudTits.IndexOf(nud);
                _lblTits[id].ForeColor = OrangeRed;
                _lblTits[id].Font = new Font(_lblTits[id].Font, Bold);
            }
        }

        // nud leave
        private void Nud_Leave(object sender, EventArgs e)
        {
            var nud = (NumericUpDown)sender;
            // fix display
            if (string.IsNullOrWhiteSpace(nud.Text))
            {
                nud.Text = nud.Value.ToString();
            }
            // nudTit highlight
            if (_nudTits.Contains(nud))
            {
                var id = _nudTits.IndexOf(nud);
                _lblTits[id].ForeColor = Black;
                _lblTits[id].Font = new Font(_lblTits[id].Font, Regular);
            }
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

        // nudG value changed
        private void NudG_ValueChanged(object sender, EventArgs e)
        {
            var nud = (NumericUpDown)sender;
            var val = nud.Value;
            if (_nudGs.Contains(nud) && val < MAX_XFMR_G)
            {
                nud.Value = val * _span;
            }
        }
        #endregion

        #region Other
        // children key down
        private void Child_KeyDown(object sender, KeyEventArgs e)
        {
            var ctrl = (Control)sender;
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
                    var nameCtrl = ctrl.Name;
                    var lenPref = "xxxX".Length;
                    var id = int.TryParse(nameCtrl.Substring(lenPref + "YY".Length), out var num) ? num : 1;
                    var chkNext = (CheckBox)Controls.Find($"chk{nameCtrl.Substring(lenPref, "YY".Length)}{id + 1}", searchAllChildren: true).FirstOrDefault();
                    if (chkNext != null)
                    {
                        chkNext.Checked = true;
                    }
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
                    var chk = (CheckBox)Controls.Find($"chk{ctrl.Name.Substring("xxxX".Length)}", searchAllChildren: true).FirstOrDefault();
                    if (chk != null && chk.Enabled)
                    {
                        chk.Checked = false;
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
                    var nudW = (NumericUpDown)Controls.Find($"nudW{ctrl.Name.Substring("xxxX".Length)}", searchAllChildren: true).FirstOrDefault();
                    nudW?.Select();
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
                    var nudH = (NumericUpDown)Controls.Find($"nudH{ctrl.Name.Substring("xxxX".Length)}", searchAllChildren: true).FirstOrDefault();
                    nudH?.Select();
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
                    var nudD = (NumericUpDown)Controls.Find($"nudD{ctrl.Name.Substring("xxxX".Length)}", searchAllChildren: true).FirstOrDefault();
                    nudD?.Select();
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
                    var chkL = (CheckBox)Controls.Find($"chkL{ctrl.Name.Substring("xxxX".Length)}", searchAllChildren: true).FirstOrDefault();
                    if (chkL != null && chkL.Enabled)
                    {
                        chkL.Checked = !chkL.Checked;
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
                    var chkR = (CheckBox)Controls.Find($"chkR{ctrl.Name.Substring("xxxX".Length)}", searchAllChildren: true).FirstOrDefault();
                    if (chkR != null && chkR.Enabled)
                    {
                        chkR.Checked = !chkR.Checked;
                    }
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
                    var chkT = (CheckBox)Controls.Find($"chkT{ctrl.Name.Substring("xxxX".Length)}", searchAllChildren: true).FirstOrDefault();
                    if (chkT != null && chkT.Enabled)
                    {
                        chkT.Checked = !chkT.Checked;
                    }
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
                    var chkP = (CheckBox)Controls.Find($"chkP{ctrl.Name.Substring("xxxX".Length)}", searchAllChildren: true).FirstOrDefault();
                    if (chkP != null && chkP.Enabled)
                    {
                        chkP.Checked = !chkP.Checked;
                    }
                    break;
                }
            }
        }

        // Mod MoveFrm event
        private void MoveFrmMod_MouseDown(object sender, MouseEventArgs e)
        {
            // base
            MoveFrm_MouseDown(sender, e);
            // sound
            SND_CHG.Play();
        }
        #endregion
    }
}
