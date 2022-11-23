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
using static System.Math;
using static System.Threading.Tasks.Task;
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
            // chk area event
            foreach (var chkA in _chkAs)
            {
                chkA.CheckedChanged += ChkA_CheckedChanged;
            }
            // chk info event
            foreach (var chkI in _chkIs)
            {
                chkI.KeyDown += Child_KeyDown;
            }
            // rdo fix tab stop
            foreach (var rdo in this.GetAllObjs(typeof(YANRdo)).Cast<YANRdo>())
            {
                rdo.CheckedChanged += Rdo_CheckedChanged;
                rdo.TabStop = false;
            }
            // nud event
            foreach (var nud in this.GetAllObjs(typeof(NumericUpDown)).Cast<NumericUpDown>())
            {
                nud.Enter += Nud_Enter;
                nud.Leave += Nud_Leave;
                nud.KeyDown += Nud_KeyDown;
                nud.KeyUp += Nud_KeyUp;
            }
            // nud title event
            foreach (var nudTit in _nudTits)
            {
                nudTit.Enter += NudTit_Enter;
                nudTit.Leave += NudTit_Leave;
                nudTit.KeyDown += Child_KeyDown;
            }
            // nud G event
            foreach (var nudG in _nudGs)
            {
                nudG.ValueChanged += NudG_ValueChanged;
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
            foreach(var chkAS in _chkASs)
            {
                chkAS.Checked= true;
            }
        }

        // btn reinforcement select all click
        private void BtnRSelAll_Click(object sender, EventArgs e)
        {
            foreach (var chkAR in _chkARs)
            {
                chkAR.Checked = true;
            }
        }

        // btn close result click
        private void BtnClRslt_Click(object sender, EventArgs e) => _frmResult?.Close();

        // Reset
        private void BtnRst_Click(object sender, EventArgs e)
        {
            // default value W, H
            foreach (var nudG in _nudGs)
            {
                nudG.Value = _span;
            }
            // default value D
            foreach (var nudD in _nudDRs)
            {
                nudD.Value = 10;
            }
            // default state on
            foreach (var chkOn in _chkOns)
            {
                chkOn.Checked = true;
            }
            // default state off
            foreach (var chkOff in _chkOffs)
            {
                chkOff.Checked = false;
            }
            // keep area slab 1
            chkASH2.Checked = false;
            chkASV2.Checked = false;
            // off all area reinforcement
            chkARH1.Checked = false;
            chkARV1.Checked = false;
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
        // Get Id from control info
        private int? GetIdFromCtrlI(Control ctrl) => _nudWs.Contains(ctrl)
                ? _nudWs.IndexOf((NumericUpDown)ctrl)
                : _nudHs.Contains(ctrl)
                    ? _nudHs.IndexOf((NumericUpDown)ctrl)
                    : _nudDs.Contains(ctrl)
                                    ? _nudDs.IndexOf((NumericUpDown)ctrl)
                                    : _chkBLs.Contains(ctrl)
                                                    ? _chkBLs.IndexOf((CheckBox)ctrl)
                                                    : _chkBRs.Contains(ctrl)
                                                                    ? _chkBRs.IndexOf((CheckBox)ctrl)
                                                                    : _chkFLs.Contains(ctrl) ? _chkFLs.IndexOf((CheckBox)ctrl) : _chkFRs.Contains(ctrl) ? _chkFRs.IndexOf((CheckBox)ctrl) : null;

        // Get all chk slab horizontal
        private void GetAllChkSH()
        {
            var lgRebar = 0d;
            // scan control to list model
            foreach(var chkASH in _chkASHs)
            {
                if (chkASH.Checked)
                {
                    var id = _chkASHs.IndexOf(chkASH);
                    var w = (double)_nudWSHs[id].Value;
                    var areaS = new AreaSlab(_branch, _maxRawWood)
                    {
                        W = w,
                        H = (double)_nudHSHs[id].Value,
                        BendingL = _chkBLSHs[id].Checked,
                        BendingR = _chkBRSHs[id].Checked
                    };
                    _areaSHs.Add(areaS);
                    lgRebar = Max(lgRebar, w);
                }
            }
            // reboot
            var isLgFd = false;
            foreach (var areaSH in _areaSHs)
            {
                // find longest area
                if (!isLgFd && areaSH.W == lgRebar)
                {
                    areaSH.IsLongest = true;
                    isLgFd = true;
                }
                // process all
                areaSH.Prcs();
            }
        }

        // Get all chk slab vertical
        private void GetAllChkSV()
        {
            var lgRebar = 0d;
            // scan control to list model
            foreach (var chkASH in _chkASVs)
            {
                if (chkASH.Checked)
                {
                    var id = _chkASVs.IndexOf(chkASH);
                    var w = (double)_nudWSVs[id].Value;
                    var areaS = new AreaSlab(_branch, _maxRawWood)
                    {
                        W = w,
                        H = (double)_nudHSVs[id].Value,
                        BendingL = _chkBLSVs[id].Checked,
                        BendingR = _chkBRSVs[id].Checked
                    };
                    _areaSVs.Add(areaS);
                    lgRebar = Max(lgRebar, w);
                }
            }
            // reboot
            var isLgFd = false;
            foreach (var areaSV in _areaSVs)
            {
                // find longest area
                if (!isLgFd && areaSV.W == lgRebar)
                {
                    areaSV.IsLongest = true;
                    isLgFd = true;
                }
                // process all
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
