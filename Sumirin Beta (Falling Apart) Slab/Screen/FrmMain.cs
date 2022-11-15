using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Drawing.Color;
using static System.Math;
using static System.Threading.Tasks.Task;
using static System.Windows.Forms.Keys;
using static System.Windows.Forms.MessageBoxButtons;
using static System.Windows.Forms.MessageBoxIcon;
using static YANF.Script.YANEvent;
using static YANF.Script.YANConstant.MsgBoxLang;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Settings;
using Sumirin_Beta__Falling_Apart__Slab.Script.Model;
using YANF.Control;
using YANF.Script;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant.SumirinBranch;

namespace Sumirin_Beta__Falling_Apart__Slab.Screen
{
    public partial class FrmMain : Form
    {
        #region Fields
        private List<Area> _areaSHs;
        private List<Area> _areaSVs;
        private FrmResult _frmResult;
        private SumirinBranch _branch;
        private double _maxRawWood;
        private const int _maxAreaS = 10;
        private const int _maxAreaR = 6;
        #endregion

        #region Constructors
        public FrmMain()
        {
            InitializeComponent();
            // move frm by pnl
            foreach (var pnl in this.GetAllObjs(typeof(Panel)))
            {
                pnl.MouseDown += MoveFrmMod_MouseDown;
                pnl.MouseMove += MoveFrm_MouseMove;
                pnl.MouseUp += MoveFrm_MouseUp;
            }
            // move frm by gradient pnl
            foreach (var gradPnl in this.GetAllObjs(typeof(YANGradPnl)))
            {
                gradPnl.MouseDown += MoveFrmMod_MouseDown;
                gradPnl.MouseMove += MoveFrm_MouseMove;
                gradPnl.MouseUp += MoveFrm_MouseUp;
            }
            // move frm by lbl
            foreach (var lbl in this.GetAllObjs(typeof(Label)))
            {
                lbl.MouseDown += MoveFrmMod_MouseDown;
                lbl.MouseMove += MoveFrm_MouseMove;
                lbl.MouseUp += MoveFrm_MouseUp;
            }
            // nud event
            foreach (var nud in this.GetAllObjs(typeof(NumericUpDown)).Cast<NumericUpDown>())
            {
                nud.Enter += Nud_Enter;
                nud.Leave += Nud_Leave;
                nud.KeyDown += Nud_KeyDown;
                nud.KeyUp += Nud_KeyUp;
                nud.ValueChanged += NudG_ValueChanged;
            }
            // chk link pnl
            foreach (var chk in this.GetAllObjs(typeof(CheckBox)).Cast<CheckBox>())
            {
                chk.CheckedChanged += Chk_CheckedChanged;
            }
            // rdo fix tab stop
            foreach (var rdo in this.GetAllObjs(typeof(YANRdo)).Cast<YANRdo>())
            {
                rdo.CheckedChanged += Rdo_CheckedChanged;
                rdo.TabStop = false;
            }
        }
        #endregion

        #region Events
        // frm shown
        private void FrmMain_Shown(object sender, EventArgs e) => this.FadeIn();

        // frm closing
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e) => this.FadeOut();

        // Mod MoveFrm event
        private void MoveFrmMod_MouseDown(object sender, MouseEventArgs e)
        {
            // base
            MoveFrm_MouseDown(sender, e);
            // sound
            SND_CHG.Play();
        }

        // nud enter
        private void Nud_Enter(object sender, EventArgs e)
        {
            var nud = (NumericUpDown)sender;
            Run(() => nud.HighLightLblLinkByCtrl("nud", OrangeRed, true));
            nud.Select(0, nud.Text.Length);
        }

        // nud leave
        private void Nud_Leave(object sender, EventArgs e)
        {
            var nud = (NumericUpDown)sender;
            Run(() => nud.HighLightLblLinkByCtrl("nud", Black, false));
            if (string.IsNullOrWhiteSpace(nud.Text))
            {
                nud.Text = nud.Value.ToString();
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
                case O:
                {
                    e.SuppressKeyPress = true;
                    var nameNud = nud.Name;
                    var lenPref = "nudX".Length;
                    var id = int.TryParse(nameNud.Substring(lenPref + 2), out var num) ? num : 1;
                    var chkNext = (CheckBox)Controls.Find($"chk{nameNud.Substring(lenPref, 2)}{id + 1}", searchAllChildren: true).FirstOrDefault();
                    //
                    if (chkNext != null)
                    {
                        chkNext.Checked = true;
                    }
                    break;
                }
                case C:
                {
                    e.SuppressKeyPress = true;
                    var chk = (CheckBox)Controls.Find($"chk{nud.Name.Substring("nudX".Length)}", searchAllChildren: true).FirstOrDefault();
                    //
                    if (chk != null && chk.Enabled)
                    {
                        chk.Checked = false;
                    }
                    break;
                }
                case W:
                {
                    e.SuppressKeyPress = true;
                    var nudW = (NumericUpDown)Controls.Find($"nudW{nud.Name.Substring("nudX".Length)}", searchAllChildren: true).FirstOrDefault();
                    //
                    nudW?.Select();
                    break;
                }
                case H:
                {
                    e.SuppressKeyPress = true;
                    var nudH = (NumericUpDown)Controls.Find($"nudH{nud.Name.Substring("nudX".Length)}", searchAllChildren: true).FirstOrDefault();
                    //
                    nudH?.Select();
                    break;
                }
                case D:
                {
                    e.SuppressKeyPress = true;
                    var nudD = (NumericUpDown)Controls.Find($"nudD{nud.Name.Substring("nudX".Length)}", searchAllChildren: true).FirstOrDefault();
                    //
                    nudD?.Select();
                    break;
                }
                case L:
                {
                    e.SuppressKeyPress = true;
                    var chkL = (CheckBox)Controls.Find($"chkBL{nud.Name.Substring("nudX".Length)}", searchAllChildren: true).FirstOrDefault();
                    //
                    if (chkL != null && chkL.Enabled)
                    {
                        chkL.Checked = !chkL.Checked;
                    }
                    break;
                }
                case R:
                {
                    e.SuppressKeyPress = true;
                    var chkR = (CheckBox)Controls.Find($"chkBR{nud.Name.Substring("nudX".Length)}", searchAllChildren: true).FirstOrDefault();
                    //
                    if (chkR != null && chkR.Enabled)
                    {
                        chkR.Checked = !chkR.Checked;
                    }
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

        // nud W value changed
        private void NudG_ValueChanged(object sender, EventArgs e)
        {
            var nud = (NumericUpDown)sender;
            var val = nud.Value;
            if (nud.Name.Substring(0, "nudW".Length) is "nudW" or "nudH" && val < 30)
            {
                nud.Value = val * (decimal)Default.Span;
            }
        }

        // chk checked changed
        private void Chk_CheckedChanged(object sender, EventArgs e)
        {
            // sound
            SND_PRS.Play();
            // main
            var chk = (CheckBox)sender;
            //
            var pnl = (Panel)Controls.Find("pnl" + chk.Name.Substring("chk".Length), searchAllChildren: true).FirstOrDefault();
            if (pnl != null)
            {
                pnl.Enabled = chk.Checked;
                if (pnl.Enabled)
                {
                    SelectNextControl(pnl, true, true, true, true);
                }
            }
            //
            var lenPrefChk = "chkXX".Length;
            var hdr = chk.Name.Substring(0, lenPrefChk);
            var ftr = chk.Name.Substring(lenPrefChk);
            if (hdr is "chkSH" or "chkSV" && int.TryParse(ftr, out var no) && no < 10 || hdr is "chkRH" or "chkRV" && int.TryParse(ftr, out no) && no < 6)
            {
                var nextName = chk.Name.Substring("chk".Length, 2) + (no + 1).ToString();
                //
                var pnlPNext = (Panel)Controls.Find($"pnlA{nextName}", searchAllChildren: true).FirstOrDefault();
                if (pnlPNext != null)
                {
                    pnlPNext.Enabled = chk.Checked;
                }
                //
                if (!chk.Checked)
                {
                    //
                    var chkNext = (CheckBox)Controls.Find($"chk{nextName}", searchAllChildren: true).FirstOrDefault();
                    if (chkNext != null)
                    {
                        chkNext.Checked = chk.Checked;
                    }
                }
            }
        }

        // rdo checked changed
        private void Rdo_CheckedChanged(object sender, EventArgs e) => ((YANRdo)sender).TabStop = false;

        // btn slab select all click
        private void BtnSSelAll_Click(object sender, EventArgs e)
        {
            for (var i = 1; i <= _maxAreaS; i++)
            {
                ((CheckBox)Controls.Find($"chkSH{i}", searchAllChildren: true).FirstOrDefault()).Checked = true;
                ((CheckBox)Controls.Find($"chkSV{i}", searchAllChildren: true).FirstOrDefault()).Checked = true;
            }
        }

        // btn reinforcement select all click
        private void BtnRSelAll_Click(object sender, EventArgs e)
        {
            for (var i = 1; i <= _maxAreaR; i++)
            {
                ((CheckBox)Controls.Find($"chkRH{i}", searchAllChildren: true).FirstOrDefault()).Checked = true;
                ((CheckBox)Controls.Find($"chkRV{i}", searchAllChildren: true).FirstOrDefault()).Checked = true;
            }
        }

        // btn slab select half click
        private void BtnSSelHalf_Click(object sender, EventArgs e)
        {
            for (var i = 1; i <= _maxAreaS / 2; i++)
            {
                ((CheckBox)Controls.Find($"chkSH{i}", searchAllChildren: true).FirstOrDefault()).Checked = true;
                ((CheckBox)Controls.Find($"chkSV{i}", searchAllChildren: true).FirstOrDefault()).Checked = true;
            }
            var idOut = _maxAreaS / 2 + 1;
            ((CheckBox)Controls.Find($"chkSH{idOut}", searchAllChildren: true).FirstOrDefault()).Checked = false;
            ((CheckBox)Controls.Find($"chkSV{idOut}", searchAllChildren: true).FirstOrDefault()).Checked = false;
        }

        // btn reinforcement select half click
        private void BtnRSelHalf_Click(object sender, EventArgs e)
        {
            for (var i = 1; i <= _maxAreaR / 2; i++)
            {
                ((CheckBox)Controls.Find($"chkRH{i}", searchAllChildren: true).FirstOrDefault()).Checked = true;
                ((CheckBox)Controls.Find($"chkRV{i}", searchAllChildren: true).FirstOrDefault()).Checked = true;
            }
            var idOut = _maxAreaR / 2 + 1;
            ((CheckBox)Controls.Find($"chkRH{idOut}", searchAllChildren: true).FirstOrDefault()).Checked = false;
            ((CheckBox)Controls.Find($"chkRV{idOut}", searchAllChildren: true).FirstOrDefault()).Checked = false;
        }

        // Reset
        private void BtnRst_Click(object sender, EventArgs e)
        {
            foreach (var nud in this.GetAllObjs(typeof(NumericUpDown)).Cast<NumericUpDown>())
            {
                switch (nud.Name.Substring("nud".Length, 1))
                {
                    case "W":
                    case "H":
                    {
                        nud.Value = (decimal)Default.Span;
                        break;
                    }
                    case "D":
                    {
                        nud.Value = 10;
                        break;
                    }
                    case "B":
                    {
                        nud.Value = 2;
                        break;
                    }
                }
                chkSH2.Checked = false;
                chkSV2.Checked = false;
                chkRH1.Checked = false;
                chkRV1.Checked = false;
            }
        }

        // Calculate
        private void BtnCalc_Click(object sender, EventArgs e)
        {
            // sound
            SND_NEXT.Play();
            // main
            _branch = rdoBrIbaraki.Checked ? Ibaraki : Touhoku;
            _maxRawWood = tgTruck2Ton.Checked ? Default.Max_Raw_Wood_2t : Default.Max_Raw_Wood_Nml;
            _areaSHs = new();
            _areaSVs = new();
            GetAllChkSH();
            GetAllChkSV();
            _frmResult?.Close();
            _frmResult = new FrmResult(_areaSHs, _areaSVs);
            _frmResult.Show();
        }

        // Close app
        private void BtnCl_Click(object sender, EventArgs e)
        {
            Close();
            // sound
            SND_NEXT.PlaySync();
        }
        #endregion

        #region Methods
        // Get all chk slab horizontal
        private void GetAllChkSH()
        {
            //
            var lgRebar = 0d;
            for (var i = 1; i <= _maxAreaS; i++)
            {
                //
                var chk = (CheckBox)Controls.Find($"chkSH{i}", searchAllChildren: true).FirstOrDefault();
                if (chk != null)
                {
                    //
                    if (chk.Checked)
                    {
                        var taskW = Run(() =>
                        {
                            var nudW = (NumericUpDown)Controls.Find($"nudWSH{i}", searchAllChildren: true).FirstOrDefault();
                            return nudW != null ? (double)nudW.Value : 0;
                        });
                        var taskH = Run(() =>
                        {
                            var nudH = (NumericUpDown)Controls.Find($"nudHSH{i}", searchAllChildren: true).FirstOrDefault();
                            return nudH != null ? (double)nudH.Value : 0;
                        });
                        var taskBdngL = Run(() =>
                        {
                            var chkL = (CheckBox)Controls.Find($"chkBLSH{i}", searchAllChildren: true).FirstOrDefault();
                            return chkL == null || chkL.Checked;
                        });
                        var taskBdngR = Run(() =>
                        {
                            var chkR = (CheckBox)Controls.Find($"chkBRSH{i}", searchAllChildren: true).FirstOrDefault();
                            return chkR == null || chkR.Checked;
                        });
                        var area = new Area(_branch, _maxRawWood);
                        //
                        var w = taskW.Result;
                        if (w > 0)
                        {
                            area.W = w;
                        }
                        else
                        {
                            YANMessageBox.Show("エラー", $"「nudWSH{i}」探さない！", OK, Error, JAP);
                            return;
                        }
                        //
                        var h = taskH.Result;
                        if (h > 0)
                        {
                            area.H = h;
                        }
                        else
                        {
                            YANMessageBox.Show("エラー", $"「nudHSH{i}」探さない！", OK, Error, JAP);
                            return;
                        }
                        area.BendingL = taskBdngL.Result;
                        area.BendingR = taskBdngR.Result;
                        lgRebar = Max(lgRebar, w);
                        _areaSHs.Add(area);
                    }
                }
                else
                {
                    YANMessageBox.Show("エラー", $"「chkSH{i}」探さない！", OK, Error, JAP);
                    return;
                }
            }
            //
            var isLgFd = false;
            foreach (var area in _areaSHs)
            {
                if (!isLgFd && area.W == lgRebar)
                {
                    area.IsLongest = true;
                    isLgFd = true;
                }
                area.Prcs();
            }
        }

        // Get all chk slab vertical
        private void GetAllChkSV()
        {
            //
            var lgRebar = 0d;
            for (var i = 1; i <= _maxAreaS; i++)
            {
                //
                var chk = (CheckBox)Controls.Find($"chkSV{i}", searchAllChildren: true).FirstOrDefault();
                if (chk != null)
                {
                    //
                    if (chk.Checked)
                    {
                        var taskW = Run(() =>
                        {
                            var nudW = (NumericUpDown)Controls.Find($"nudWSV{i}", searchAllChildren: true).FirstOrDefault();
                            return nudW != null ? (double)nudW.Value : 0;
                        });
                        var taskH = Run(() =>
                        {
                            var nudH = (NumericUpDown)Controls.Find($"nudHSV{i}", searchAllChildren: true).FirstOrDefault();
                            return nudH != null ? (double)nudH.Value : 0;
                        });
                        var taskBdngL = Run(() =>
                        {
                            var chkL = (CheckBox)Controls.Find($"chkBLSV{i}", searchAllChildren: true).FirstOrDefault();
                            return chkL == null || chkL.Checked;
                        });
                        var taskBdngR = Run(() =>
                        {
                            var chkR = (CheckBox)Controls.Find($"chkBRSV{i}", searchAllChildren: true).FirstOrDefault();
                            return chkR == null || chkR.Checked;
                        });
                        var area = new Area(_branch, _maxRawWood);
                        //
                        var w = taskW.Result;
                        if (w > 0)
                        {
                            area.W = w;
                        }
                        else
                        {
                            YANMessageBox.Show("エラー", $"「nudWSV{i}」探さない！", OK, Error, JAP);
                            return;
                        }
                        //
                        var h = taskH.Result;
                        if (h > 0)
                        {
                            area.H = h;
                        }
                        else
                        {
                            YANMessageBox.Show("エラー", $"「nudHSV{i}」探さない！", OK, Error, JAP);
                            return;
                        }
                        area.BendingL = taskBdngL.Result;
                        area.BendingR = taskBdngR.Result;
                        lgRebar = Max(lgRebar, w);
                        _areaSVs.Add(area);
                    }
                }
                else
                {
                    YANMessageBox.Show("エラー", $"「chkSV{i}」探さない！", OK, Error, JAP);
                    return;
                }
            }
            //
            var isLgFd = false;
            foreach (var area in _areaSVs)
            {
                if (!isLgFd && area.W == lgRebar)
                {
                    area.IsLongest = true;
                    isLgFd = true;
                }
                area.Prcs();
            }
        }
        #endregion
    }
}
