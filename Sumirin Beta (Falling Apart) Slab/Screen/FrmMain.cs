using Sumirin_Beta__Falling_Apart__Slab.Script.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using YANF.Control;
using YANF.Script;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Settings;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant.SumirinBranch;
using static System.Drawing.Color;
using static System.Math;
using static System.Threading.Tasks.Task;
using static System.Windows.Forms.Keys;
using static System.Windows.Forms.MessageBoxButtons;
using static System.Windows.Forms.MessageBoxIcon;
using static YANF.Script.YANConstant.MsgBoxLang;
using static YANF.Script.YANEvent;

namespace Sumirin_Beta__Falling_Apart__Slab.Screen
{
    public partial class FrmMain : Form
    {
        #region Fields
        private List<AreaSlab> _areaSHs;
        private List<AreaSlab> _areaSVs;
        private List<AreaReinforcement> _areaRHs;
        private List<AreaReinforcement> _areaRVs;
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
            InitItems();
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
                nud.KeyDown += Child_KeyDown;
                nud.KeyDown += Nud_KeyDown;
                nud.KeyUp += Nud_KeyUp;
                nud.ValueChanged += NudG_ValueChanged;
            }
            // chk link pnl
            foreach (var chk in this.GetAllObjs(typeof(CheckBox)).Cast<CheckBox>())
            {
                chk.CheckedChanged += ChkA_CheckedChanged;
                if (chk.Name.Substring("chk".Length, "X".Length) is "L" or "R" or "T" or "P")
                {
                    chk.KeyDown += Child_KeyDown;
                }
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

        // btn close result click
        private void BtnClRslt_Click(object sender, EventArgs e) => _frmResult?.Close();

        // Reset
        private void BtnRst_Click(object sender, EventArgs e)
        {
            foreach (var nud in this.GetAllObjs(typeof(NumericUpDown)).Cast<NumericUpDown>())
            {
                // default value
                switch (nud.Name.Substring("nud".Length, 1))
                {
                    case "W":
                    case "H":
                    {
                        nud.Value = Default.Span;
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
                // default state
                foreach (var chk in this.GetAllObjs(typeof(CheckBox)).Cast<CheckBox>())
                {
                    if (chk.Name.Substring("chk".Length, "X".Length) is "L" or "R" or "T" or "P")
                    {
                        
                    }
                }
                chkASH2.Checked = false;
                chkASV2.Checked = false;
                chkARH1.Checked = false;
                chkARV1.Checked = false;
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
            // slab
            _areaSHs = new();
            _areaSVs = new();
            GetAllChkSH();
            GetAllChkSV();
            // reinforcement
            _areaRHs = new();
            _areaRVs = new();
            // result
            _frmResult?.Close();
            _frmResult = HasGetAllChkRH() && HasGetAllChkRV() ? new FrmResult(_areaSHs, _areaSVs, _areaRHs, _areaRVs) : new FrmResult(_areaSHs, _areaSVs);
            _frmResult.Show();
        }

        // Close app
        private void BtnCl_Click(object sender, EventArgs e)
        {
            // main
            Close();
            // sound
            SND_NEXT.PlaySync();
        }
        #endregion

        #region Methods
        //
        private void TracePnlAFromCtrlI(Control ctrl)
        {

        }

        // Get all chk slab horizontal
        private void GetAllChkSH()
        {
            var lgRebar = 0d;
            // scan control to list model
            for (var i = 1; i <= _maxAreaS; i++)
            {
                var chk = (CheckBox)Controls.Find($"chkSH{i}", searchAllChildren: true).FirstOrDefault();
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        // async
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
                            var chkL = (CheckBox)Controls.Find($"chkLSH{i}", searchAllChildren: true).FirstOrDefault();
                            return chkL == null || chkL.Checked;
                        });
                        var taskBdngR = Run(() =>
                        {
                            var chkR = (CheckBox)Controls.Find($"chkRSH{i}", searchAllChildren: true).FirstOrDefault();
                            return chkR == null || chkR.Checked;
                        });
                        // await
                        var areaS = new AreaSlab(_branch, _maxRawWood);
                        var w = taskW.Result;
                        if (w > 0)
                        {
                            areaS.W = w;
                        }
                        else
                        {
                            YANMessageBox.Show("エラー", $"「nudWSH{i}」探さない！", OK, Error, JAP);
                            return;
                        }
                        var h = taskH.Result;
                        if (h > 0)
                        {
                            areaS.H = h;
                        }
                        else
                        {
                            YANMessageBox.Show("エラー", $"「nudHSH{i}」探さない！", OK, Error, JAP);
                            return;
                        }
                        areaS.BendingL = taskBdngL.Result;
                        areaS.BendingR = taskBdngR.Result;
                        lgRebar = Max(lgRebar, w);
                        _areaSHs.Add(areaS);
                    }
                }
                else
                {
                    YANMessageBox.Show("エラー", $"「chkSH{i}」探さない！", OK, Error, JAP);
                    return;
                }
            }
            // find longest area
            var isLgFd = false;
            foreach (var areaSH in _areaSHs)
            {
                if (!isLgFd && areaSH.W == lgRebar)
                {
                    areaSH.IsLongest = true;
                    isLgFd = true;
                }
                areaSH.Prcs();
            }
        }

        // Get all chk slab vertical
        private void GetAllChkSV()
        {
            var lgRebar = 0d;
            // scan control to list model
            for (var i = 1; i <= _maxAreaS; i++)
            {
                var chk = (CheckBox)Controls.Find($"chkSV{i}", searchAllChildren: true).FirstOrDefault();
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        // async
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
                            var chkL = (CheckBox)Controls.Find($"chkLSV{i}", searchAllChildren: true).FirstOrDefault();
                            return chkL == null || chkL.Checked;
                        });
                        var taskBdngR = Run(() =>
                        {
                            var chkR = (CheckBox)Controls.Find($"chkRSV{i}", searchAllChildren: true).FirstOrDefault();
                            return chkR == null || chkR.Checked;
                        });
                        // await
                        var areaS = new AreaSlab(_branch, _maxRawWood);
                        var w = taskW.Result;
                        if (w > 0)
                        {
                            areaS.W = w;
                        }
                        else
                        {
                            YANMessageBox.Show("エラー", $"「nudWSV{i}」探さない！", OK, Error, JAP);
                            return;
                        }
                        var h = taskH.Result;
                        if (h > 0)
                        {
                            areaS.H = h;
                        }
                        else
                        {
                            YANMessageBox.Show("エラー", $"「nudHSV{i}」探さない！", OK, Error, JAP);
                            return;
                        }
                        areaS.BendingL = taskBdngL.Result;
                        areaS.BendingR = taskBdngR.Result;
                        lgRebar = Max(lgRebar, w);
                        _areaSVs.Add(areaS);
                    }
                }
                else
                {
                    YANMessageBox.Show("エラー", $"「chkSV{i}」探さない！", OK, Error, JAP);
                    return;
                }
            }
            // find longest area
            var isLgFd = false;
            foreach (var areaSV in _areaSVs)
            {
                if (!isLgFd && areaSV.W == lgRebar)
                {
                    areaSV.IsLongest = true;
                    isLgFd = true;
                }
                areaSV.Prcs();
            }
        }

        // Get all chk reinforcement horizontal
        private bool HasGetAllChkRH()
        {
            var rslt = false;
            for (var i = 1; i <= _maxAreaR; i++)
            {
                var chk = (CheckBox)Controls.Find($"chkRH{i}", searchAllChildren: true).FirstOrDefault();
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        rslt = true;
                        // async
                        var taskW = Run(() =>
                        {
                            var nudW = (NumericUpDown)Controls.Find($"nudWRH{i}", searchAllChildren: true).FirstOrDefault();
                            return nudW != null ? (double)nudW.Value : 0;
                        });
                        var taskH = Run(() =>
                        {
                            var nudH = (NumericUpDown)Controls.Find($"nudHRH{i}", searchAllChildren: true).FirstOrDefault();
                            return nudH != null ? (double)nudH.Value : 0;
                        });
                        var taskD = Run(() =>
                        {
                            var nudD = (NumericUpDown)Controls.Find($"nudDRH{i}", searchAllChildren: true).FirstOrDefault();
                            return nudD != null ? (int)nudD.Value : 0;
                        });
                        var taskBdngL = Run(() =>
                        {
                            var chkL = (CheckBox)Controls.Find($"chkLRH{i}", searchAllChildren: true).FirstOrDefault();
                            return chkL == null || chkL.Checked;
                        });
                        var taskBdngR = Run(() =>
                        {
                            var chkR = (CheckBox)Controls.Find($"chkRRH{i}", searchAllChildren: true).FirstOrDefault();
                            return chkR == null || chkR.Checked;
                        });
                        // await
                        var areaR = new AreaReinforcement(_branch, _maxRawWood);
                        var w = taskW.Result;
                        if (w > 0)
                        {
                            areaR.W = w;
                        }
                        else
                        {
                            YANMessageBox.Show("エラー", $"「nudWSH{i}」探さない！", OK, Error, JAP);
                            return false;
                        }
                        var h = taskH.Result;
                        if (h > 0)
                        {
                            areaR.H = h;
                        }
                        else
                        {
                            YANMessageBox.Show("エラー", $"「nudHSH{i}」探さない！", OK, Error, JAP);
                            return false;
                        }
                        var d = taskD.Result;
                        if (d > 0)
                        {
                            areaR.D = d;
                        }
                        else
                        {
                            YANMessageBox.Show("エラー", $"「nudDSH{i}」探さない！", OK, Error, JAP);
                            return false;
                        }
                        areaR.BendingL = taskBdngL.Result;
                        areaR.BendingR = taskBdngR.Result;
                        areaR.Prcs();
                        _areaRHs.Add(areaR);
                    }
                }
                else
                {
                    YANMessageBox.Show("エラー", $"「chkRH{i}」探さない！", OK, Error, JAP);
                    return false;
                }
            }
            return rslt;
        }

        // Get all chk reinforcement vertical
        private bool HasGetAllChkRV()
        {
            var rslt = false;
            for (var i = 1; i <= _maxAreaR; i++)
            {
                var chk = (CheckBox)Controls.Find($"chkRV{i}", searchAllChildren: true).FirstOrDefault();
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        rslt = true;
                        // async
                        var taskW = Run(() =>
                        {
                            var nudW = (NumericUpDown)Controls.Find($"nudWRV{i}", searchAllChildren: true).FirstOrDefault();
                            return nudW != null ? (double)nudW.Value : 0;
                        });
                        var taskH = Run(() =>
                        {
                            var nudH = (NumericUpDown)Controls.Find($"nudHRV{i}", searchAllChildren: true).FirstOrDefault();
                            return nudH != null ? (double)nudH.Value : 0;
                        });
                        var taskD = Run(() =>
                        {
                            var nudD = (NumericUpDown)Controls.Find($"nudDRV{i}", searchAllChildren: true).FirstOrDefault();
                            return nudD != null ? (int)nudD.Value : 0;
                        });
                        var taskBdngL = Run(() =>
                        {
                            var chkL = (CheckBox)Controls.Find($"chkLRV{i}", searchAllChildren: true).FirstOrDefault();
                            return chkL == null || chkL.Checked;
                        });
                        var taskBdngR = Run(() =>
                        {
                            var chkR = (CheckBox)Controls.Find($"chkRRV{i}", searchAllChildren: true).FirstOrDefault();
                            return chkR == null || chkR.Checked;
                        });
                        // await
                        var areaR = new AreaReinforcement(_branch, _maxRawWood);
                        var w = taskW.Result;
                        if (w > 0)
                        {
                            areaR.W = w;
                        }
                        else
                        {
                            YANMessageBox.Show("エラー", $"「nudWSV{i}」探さない！", OK, Error, JAP);
                            return false;
                        }
                        var h = taskH.Result;
                        if (h > 0)
                        {
                            areaR.H = h;
                        }
                        else
                        {
                            YANMessageBox.Show("エラー", $"「nudHSV{i}」探さない！", OK, Error, JAP);
                            return false;
                        }
                        var d = taskD.Result;
                        if (d > 0)
                        {
                            areaR.D = d;
                        }
                        else
                        {
                            YANMessageBox.Show("エラー", $"「nudDSH{i}」探さない！", OK, Error, JAP);
                            return false;
                        }
                        areaR.BendingL = taskBdngL.Result;
                        areaR.BendingR = taskBdngR.Result;
                        areaR.Prcs();
                        _areaRHs.Add(areaR);
                    }
                }
                else
                {
                    YANMessageBox.Show("エラー", $"「chkSV{i}」探さない！", OK, Error, JAP);
                    return false;
                }
            }
            return rslt;
        }
        #endregion
    }
}
