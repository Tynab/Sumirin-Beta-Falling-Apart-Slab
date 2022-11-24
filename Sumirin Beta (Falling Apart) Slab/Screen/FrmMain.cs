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
using static Sumirin_Beta__Falling_Apart__Slab.Script.EventHandler;
using static System.Math;
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
                chkI.KeyDown += CtrlI_KeyDown;
            }
            // chk reinforcement off event
            foreach (var chkROff in _chkROffs)
            {
                chkROff.CheckedChanged += ChkROff_CheckedChanged;
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
                nudTit.KeyDown += CtrlI_KeyDown;
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

        // sound effect
        private void TgTruck2Ton_CheckedChanged(object sender, EventArgs e) => SND_CHG.Play();

        // btn slab select all click
        private void BtnSSelAll_Click(object sender, EventArgs e)
        {
            foreach (var chkAS in _chkASs)
            {
                chkAS.Checked = true;
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
            foreach (var chkOff in _chkROffs)
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
        private int? GetIdFromCtrlI(System.Windows.Forms.Control ctrl) => _nudWs.Contains(ctrl)
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
            foreach (var chkASH in _chkASHs)
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
            foreach (var chkARH in _chkARHs)
            {
                if (chkARH.Checked)
                {
                    rslt = true;
                    var id = _chkARHs.IndexOf(chkARH);
                    var areaR = new AreaReinforcement(_branch, _maxRawWood)
                    {
                        W = (double)_nudWRHs[id].Value,
                        H = (double)_nudHRHs[id].Value,
                        D = (int)_nudDRHs[id].Value,
                        BendingL = _chkBLRHs[id].Checked,
                        BendingR = _chkBRRHs[id].Checked,
                        FixationL = _chkFLRHs[id].Checked,
                        FixationR = _chkFRRHs[id].Checked
                    };
                    areaR.Prcs();
                    _areaRHs.Add(areaR);
                }
            }
            return rslt;
        }

        // Get all chk reinforcement vertical
        private bool HasGetAllChkRV()
        {
            var rslt = false;
            foreach (var chkARV in _chkARVs)
            {
                if (chkARV.Checked)
                {
                    rslt = true;
                    var id = _chkARVs.IndexOf(chkARV);
                    var areaR = new AreaReinforcement(_branch, _maxRawWood)
                    {
                        W = (double)_nudWRVs[id].Value,
                        H = (double)_nudHRVs[id].Value,
                        D = (int)_nudDRVs[id].Value,
                        BendingL = _chkBLRVs[id].Checked,
                        BendingR = _chkBRRVs[id].Checked,
                        FixationL = _chkFLRVs[id].Checked,
                        FixationR = _chkFRRVs[id].Checked
                    };
                    areaR.Prcs();
                    _areaRHs.Add(areaR);
                }
            }
            return rslt;
        }
        #endregion
    }
}
