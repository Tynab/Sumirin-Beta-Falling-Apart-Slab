using Sumirin_Beta__Falling_Apart__Slab.Control;
using Sumirin_Beta__Falling_Apart__Slab.Script.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using YANF.Control;
using YANF.Script;
using YANF.Script.Service;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Resources;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Settings;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Common;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant.SumirinBranch;
using static Sumirin_Beta__Falling_Apart__Slab.Script.EventHandler;
using static System.Diagnostics.Process;
using static System.Windows.Forms.Application;
using static System.Windows.Forms.MessageBoxButtons;
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
        private Calculator _ctrlCalculator;
        private IYANDlvScrService _dlvScrService;
        private SumirinBranch _branch;
        private double _maxRawWood;
        private int _pct;
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
            _ctrlISs.ForEach(x => x.KeyDown += CtrlIS_KeyDown);
            // ctrl reinforcement info event
            _ctrlIRs.ForEach(x => x.KeyDown += CtrlIR_KeyDown);
            // chk slab area event
            _chkASs.ForEach(x => x.CheckedChanged += ChkAS_CheckedChanged);
            // chk reinforcement area event
            _chkARs.ForEach(x => x.CheckedChanged += ChkAR_CheckedChanged);
            // chk reinforcement on event
            _chkROns.ForEach(x => x.CheckedChanged += ChkROn_CheckedChanged);
            // chk reinforcement off event
            _chkROffs.ForEach(x => x.CheckedChanged += ChkROff_CheckedChanged);
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
            _nudTits.ForEach(x =>
            {
                x.Enter += NudTit_Enter;
                x.Leave += NudTit_Leave;
            });
            // nud slab area event
            _nudASs.ForEach(x => x.ValueChanged += NudAS_ValueChanged);
            // nud reinforcement area event
            _nudARs.ForEach(x => x.ValueChanged += NudAR_ValueChanged);
            // nud G event
            _nudGs.ForEach(x => x.ValueChanged += NudG_ValueChanged);
        }
        #endregion

        #region Events
        // frm shown
        private void FrmMain_Shown(object sender, EventArgs e)
        {
            this.FadeIn();
            ChkUpd();
        }

        // frm closing
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e) => this.FadeOut();

        // sound effect
        private void TgTruck2Ton_CheckedChanged(object sender, EventArgs e) => SND_CHG.Play();

        // btn slab select all click
        private void BtnSSelAll_Click(object sender, EventArgs e) => _chkASs.ForEach(x => x.Checked = true);

        // btn reinforcement select all click
        private void BtnRSelAll_Click(object sender, EventArgs e) => _chkARs.ForEach(x => x.Checked = true);

        // btn close result click
        private void BtnClRslt_Click(object sender, EventArgs e) => _frmResult?.Close();

        // Reset
        private void BtnRst_Click(object sender, EventArgs e)
        {
            // default value W, H
            _nudGs.ForEach(x => x.Value = _span);
            // default value D
            _nudDRs.ForEach(x => x.Value = 10);
            // default value A
            _nudAs.ForEach(x => x.Value = 1);
            // default state on
            _chkOns.ForEach(x => x.Checked = true);
            // default state off
            _chkROffs.ForEach(x => x.Checked = false);
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

        // Timer update
        private void TmrUpd_Tick(object sender, EventArgs e)
        {
            if (_pct >= 100)
            {
                _dlvScrService.OffLoader();
                tmrUpd.StopAdv();
                Start(FILE_SETUP_ADR);
                Exit();
            }
        }
        #endregion

        #region Methods
        // Check update
        private void ChkUpd()
        {
            if (IsNetAvail())
            {
                using var wc = new WebClient();
                if (!wc.DownloadString(link_ver).Contains(app_ver))
                {
                    _ = YANMessageBox.Show("更新", "ライセンスが間違っています！", OK, MessageBoxIcon.Information, JAP);
                    _dlvScrService = new YANUpdScrService();
                    _dlvScrService.OnLoader(this);
                    _pct = 0;
                    tmrUpd.StartAdv();
                    CrtDirAdv(FRNT_PATH);
                    DelFileAdv(FILE_SETUP_ADR);
                    wc.DownloadFileAsync(new Uri(wc.DownloadString(link_app)), FILE_SETUP_ADR);
                    wc.DownloadProgressChanged += Upd_DownloadProgressChanged;
                }
            }
        }

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
            // scan control to list model
            _chkASHs.ForEach(x =>
            {
                if (x.Checked)
                {
                    var id = _chkASHs.IndexOf(x);
                    var w = (double)_nudWSHs[id].Value;
                    var areaS = new AreaSlab(_branch, _maxRawWood)
                    {
                        Id = id,
                        Area = (int)_nudASHs[id].Value,
                        W = w,
                        H = (double)_nudHSHs[id].Value,
                        BendingL = _chkBLSHs[id].Checked,
                        BendingR = _chkBRSHs[id].Checked
                    };
                    _areaSHs.Add(areaS);
                }
            });
            // reboot
            var ids = _areaSHs.GroupBy(x => x.Area).Select(g => g.ToList()).ToList().Select(y => y.OrderByDescending(x => x.W).First().Id).ToList();
            _areaSHs.ForEach(x =>
            {
                if (ids.Contains(x.Id))
                {
                    x.IsLongest = true;
                }
                x.Prcs();
            });
        }

        // Get all chk slab vertical
        private void GetAllChkSV()
        {
            // scan control to list model
            _chkASVs.ForEach(x =>
            {
                if (x.Checked)
                {
                    var id = _chkASVs.IndexOf(x);
                    var w = (double)_nudWSVs[id].Value;
                    var areaS = new AreaSlab(_branch, _maxRawWood)
                    {
                        Id = id,
                        Area = (int)_nudASVs[id].Value,
                        W = w,
                        H = (double)_nudHSVs[id].Value,
                        BendingL = _chkBLSVs[id].Checked,
                        BendingR = _chkBRSVs[id].Checked
                    };
                    _areaSVs.Add(areaS);
                }
            });
            // reboot
            var ids = _areaSVs.GroupBy(x => x.Area).Select(g => g.ToList()).ToList().Select(y => y.OrderByDescending(x => x.W).First().Id).ToList();
            _areaSVs.ForEach(x =>
            {
                if (ids.Contains(x.Id))
                {
                    x.IsLongest = true;
                }
                x.Prcs();
            });
        }

        // Get all chk reinforcement horizontal
        private bool HasGetAllChkRH()
        {
            var rslt = false;
            _chkARHs.ForEach(x =>
            {
                if (x.Checked)
                {
                    rslt = true;
                    var id = _chkARHs.IndexOf(x);
                    var areaR = new AreaReinforcement(_branch, _maxRawWood)
                    {
                        Id = id,
                        Area = (int)_nudARHs[id].Value,
                        W = (double)_nudWRHs[id].Value,
                        H = (double)_nudHRHs[id].Value,
                        D = (int)_nudDRHs[id].Value,
                        BendingL = _chkBLRHs[id].Checked,
                        BendingR = _chkBRRHs[id].Checked,
                        FixationL = _chkFLRHs[id].Checked,
                        FixationR = _chkFRRHs[id].Checked
                    };
                    _areaRHs.Add(areaR);
                }
            });
            if (rslt)
            {
                // reboot
                var ids = _areaRHs.GroupBy(x => x.Area).Select(g => g.ToList()).ToList().Select(y => y.OrderByDescending(x => x.W).First().Id).ToList();
                _areaRHs.ForEach(x =>
                {
                    if (ids.Contains(x.Id))
                    {
                        x.IsLongest = true;
                    }
                    x.Prcs();
                });
            }
            return rslt;
        }

        // Get all chk reinforcement vertical
        private bool HasGetAllChkRV()
        {
            var rslt = false;
            _chkARVs.ForEach(x =>
            {
                if (x.Checked)
                {
                    rslt = true;
                    var id = _chkARVs.IndexOf(x);
                    var areaR = new AreaReinforcement(_branch, _maxRawWood)
                    {
                        Id = id,
                        Area = (int)_nudARVs[id].Value,
                        W = (double)_nudWRVs[id].Value,
                        H = (double)_nudHRVs[id].Value,
                        D = (int)_nudDRVs[id].Value,
                        BendingL = _chkBLRVs[id].Checked,
                        BendingR = _chkBRRVs[id].Checked,
                        FixationL = _chkFLRVs[id].Checked,
                        FixationR = _chkFRRVs[id].Checked
                    };
                    _areaRVs.Add(areaR);
                }
            });
            if (rslt)
            {
                // reboot
                var ids = _areaRVs.GroupBy(x => x.Area).Select(g => g.ToList()).ToList().Select(y => y.OrderByDescending(x => x.W).First().Id).ToList();
                _areaRVs.ForEach(x =>
                {
                    if (ids.Contains(x.Id))
                    {
                        x.IsLongest = true;
                    }
                    x.Prcs();
                });
            }
            return rslt;
        }
        #endregion
    }
}
