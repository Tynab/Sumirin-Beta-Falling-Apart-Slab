using Sumirin_Beta__Falling_Apart__Slab.Control;
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
        private Calculator _ctrlCalculator;
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
            // ctrl slab info event
            var ctrlISs = new List<System.Windows.Forms.Control>();
            ctrlISs.AddRange(_nudWSs);
            ctrlISs.AddRange(_nudHSs);
            ctrlISs.AddRange(_chkBLSs);
            ctrlISs.AddRange(_chkBRSs);
            foreach (var ctrlS in ctrlISs)
            {
                ctrlS.KeyDown += CtrlIS_KeyDown;
            }
            // ctrl reinforcement info event
            var ctrlIRs = new List<System.Windows.Forms.Control>();
            ctrlIRs.AddRange(_nudWRs);
            ctrlIRs.AddRange(_nudHRs);
            ctrlIRs.AddRange(_nudDRs);
            ctrlIRs.AddRange(_chkBLRs);
            ctrlIRs.AddRange(_chkBRRs);
            ctrlIRs.AddRange(_chkFLRs);
            ctrlIRs.AddRange(_chkFRRs);
            foreach (var ctrlR in ctrlIRs)
            {
                ctrlR.KeyDown += CtrlIR_KeyDown;
            }
            // chk slab area event
            foreach (var chkAS in _chkASs)
            {
                chkAS.CheckedChanged += ChkAS_CheckedChanged;
            }
            // chk reinforcement area event
            foreach (var chkAR in _chkARs)
            {
                chkAR.CheckedChanged += ChkAR_CheckedChanged;
            }
            // chk reinforcement on event
            foreach (var chkROn in _chkROns)
            {
                chkROn.CheckedChanged += ChkROn_CheckedChanged;
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
        // Get Id from control slab info
        private int? GetIdFromCtrlIS(System.Windows.Forms.Control ctrl) => _nudWSs.Contains(ctrl)
                ? _nudWSs.IndexOf((NumericUpDown)ctrl)
                : _nudHSs.Contains(ctrl)
                    ? _nudHSs.IndexOf((NumericUpDown)ctrl)
                    : _chkBLSs.Contains(ctrl) ? _chkBLSs.IndexOf((CheckBox)ctrl) : _chkBRSs.Contains(ctrl) ? _chkBRSs.IndexOf((CheckBox)ctrl) : null;

        // Get Id from control reinforcement info
        private int? GetIdFromCtrlIR(System.Windows.Forms.Control ctrl) => _nudWRs.Contains(ctrl)
                ? _nudWRs.IndexOf((NumericUpDown)ctrl)
                : _nudHRs.Contains(ctrl)
                    ? _nudHRs.IndexOf((NumericUpDown)ctrl)
                    : _nudDRs.Contains(ctrl)
                                    ? _nudDRs.IndexOf((NumericUpDown)ctrl)
                                    : _chkBLRs.Contains(ctrl)
                                                    ? _chkBLRs.IndexOf((CheckBox)ctrl)
                                                    : _chkBRRs.Contains(ctrl)
                                                                    ? _chkBRRs.IndexOf((CheckBox)ctrl)
                                                                    : _chkFLRs.Contains(ctrl) ? _chkFLRs.IndexOf((CheckBox)ctrl) : _chkFRRs.Contains(ctrl) ? _chkFRRs.IndexOf((CheckBox)ctrl) : null;

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
