using Sumirin_Beta__Falling_Apart__Slab.Script.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using YANF.Control;
using YANF.Script;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant;
using static System.Windows.Forms.DockStyle;
using static System.Windows.Forms.HorizontalAlignment;
using static System.Windows.Forms.Keys;
using static YANF.Script.YANEvent;

namespace Sumirin_Beta__Falling_Apart__Slab.Screen
{
    public partial class FrmResult : Form
    {
        #region Fields
        private List<(string, int)> _smryS; // rebar, summary
        private List<(int, string, int)> _smryR; // D, rebar, sumary
        private List<(string, int)> _smryRD1;
        private List<(string, int)> _smryRD2;
        private List<(string, int)> _smryRD3;
        private List<(string, int)> _smryRD4;
        private List<(string, int)> _smryRD5;
        private List<(string, int)> _smryRD6;
        private List<AreaSlab> _areaSHs;
        private List<AreaSlab> _areaSVs;
        private List<AreaReinforcement> _areaRHs;
        private List<AreaReinforcement> _areaRVs;
        #endregion

        #region Constructors
        public FrmResult(List<AreaSlab> areaSHs, List<AreaSlab> areaSVs)
        {
            InitializeComponent();
            // move frm by pnl
            foreach (var pnl in this.GetAllObjs(typeof(Panel)))
            {
                pnl.MouseDown += MoveFrmMod_MouseDown;
                pnl.MouseMove += MoveFrm_MouseMove;
                pnl.MouseUp += MoveFrm_MouseUp;
                pnl.KeyDown += All_KeyDown;
            }
            // move frm by gradient pnl
            foreach (var gradPnl in this.GetAllObjs(typeof(YANGradPnl)))
            {
                gradPnl.MouseDown += MoveFrmMod_MouseDown;
                gradPnl.MouseMove += MoveFrm_MouseMove;
                gradPnl.MouseUp += MoveFrm_MouseUp;
                gradPnl.KeyDown += All_KeyDown;
            }
            // move frm by lbl
            foreach (var lbl in this.GetAllObjs(typeof(Label)))
            {
                lbl.MouseDown += MoveFrmMod_MouseDown;
                lbl.MouseMove += MoveFrm_MouseMove;
                lbl.MouseUp += MoveFrm_MouseUp;
                lbl.KeyDown += All_KeyDown;
            }
            // move frm by lbl
            foreach (var rtx in this.GetAllObjs(typeof(RichTextBox)).Cast<RichTextBox>())
            {
                rtx.KeyDown += All_KeyDown;
                rtx.ContentsResized += Rtx_ContentsResized;
            }
            KeyDown += All_KeyDown;
            // get list
            _smryS = new List<(string, int)>();
            _areaSHs = new List<AreaSlab>(areaSHs);
            _areaSVs = new List<AreaSlab>(areaSVs);
        }

        public FrmResult(List<AreaSlab> areaSHs, List<AreaSlab> areaSVs, List<AreaReinforcement> areaRHs, List<AreaReinforcement> areaRVs)
        {
            InitializeComponent();
            // move frm by pnl
            foreach (var pnl in this.GetAllObjs(typeof(Panel)))
            {
                pnl.MouseDown += MoveFrmMod_MouseDown;
                pnl.MouseMove += MoveFrm_MouseMove;
                pnl.MouseUp += MoveFrm_MouseUp;
                pnl.KeyDown += All_KeyDown;
            }
            // move frm by gradient pnl
            foreach (var gradPnl in this.GetAllObjs(typeof(YANGradPnl)))
            {
                gradPnl.MouseDown += MoveFrmMod_MouseDown;
                gradPnl.MouseMove += MoveFrm_MouseMove;
                gradPnl.MouseUp += MoveFrm_MouseUp;
                gradPnl.KeyDown += All_KeyDown;
            }
            // move frm by lbl
            foreach (var lbl in this.GetAllObjs(typeof(Label)))
            {
                lbl.MouseDown += MoveFrmMod_MouseDown;
                lbl.MouseMove += MoveFrm_MouseMove;
                lbl.MouseUp += MoveFrm_MouseUp;
                lbl.KeyDown += All_KeyDown;
            }
            // move frm by lbl
            foreach (var rtx in this.GetAllObjs(typeof(RichTextBox)).Cast<RichTextBox>())
            {
                rtx.KeyDown += All_KeyDown;
                rtx.ContentsResized += Rtx_ContentsResized;
            }
            KeyDown += All_KeyDown;
            // get list
            _smryS = new List<(string, int)>();
            _areaSHs = new List<AreaSlab>(areaSHs);
            _areaSVs = new List<AreaSlab>(areaSVs);
            _smryR = new List<(int, string, int)>();
            _areaRHs = new List<AreaReinforcement>(areaRHs);
            _areaRVs = new List<AreaReinforcement>(areaRVs);
        }
        #endregion

        #region Events
        // frm load
        private void FrmResult_Load(object sender, EventArgs e)
        {
            //
            SsForDisplay(_areaSHs, out var rsltRebarSH, out var rsltAmtSH);
            lblRsltRebarSH.Text = rsltRebarSH;
            lblRsltAmtSH.Text = rsltAmtSH;
            //
            SsForDisplay(_areaSVs, out var rsltRebarSV, out var rsltAmtSV);
            lblRsltRebarSV.Text = rsltRebarSV;
            lblRsltAmtSV.Text = rsltAmtSV;
            //
            if (_areaRHs != null && _areaRVs != null)
            {
                //
                RsForDisplay(_areaRHs, out var rsltDRH, out var rsltRebarRH, out var rsltAmtRH);
                lblRsltDRH.Text = rsltDRH;
                lblRsltRebarRH.Text = rsltRebarRH;
                lblRsltAmtRH.Text = rsltAmtRH;
                //
                RsForDisplay(_areaRHs, out var rsltDRV, out var rsltRebarRV, out var rsltAmtRV);
                lblRsltDRV.Text = rsltDRV;
                lblRsltRebarRV.Text = rsltRebarRV;
                lblRsltAmtRV.Text = rsltAmtRV;
            }
            //
            SmrySForDisplay();
            SmryRForDisplay();
        }

        // frm shown
        private void FrmResult_Shown(object sender, EventArgs e) => this.FadeIn();

        // frm closing
        private void FrmResult_FormClosing(object sender, FormClosingEventArgs e) => this.FadeOut();

        // Mod MoveFrm event
        private void MoveFrmMod_MouseDown(object sender, MouseEventArgs e)
        {
            // base
            MoveFrm_MouseDown(sender, e);
            // sound
            SND_CHG.Play();
        }

        // all key down
        private void All_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Escape)
            {
                // main
                Close();
                // sound
                SND_CHG.PlaySync();
            }
        }

        // rtx text align
        private void Rtx_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            var rtx = (RichTextBox)sender;
            // horizontal
            rtx.SelectAll();
            rtx.SelectionAlignment = Center;
            rtx.DeselectAll();
            // vertical
            rtx.Height = e.NewRectangle.Height;
            var cH = rtx.Height;
            var pH = rtx.Parent.Height;
            if (cH < pH - 20)
            {
                rtx.Top = (pH - cH) / 2 + 20;
            }
            else
            {
                rtx.Dock = Fill;
            }
        }
        #endregion

        #region Methods
        // Slab list for display
        private void SsForDisplay(List<AreaSlab> areaSs, out string rsltRebarSs, out string rsltAmtSs)
        {
            rsltRebarSs = "";
            rsltAmtSs = "";
            foreach (var area in areaSs)
            {
                foreach (var rebar in area.Rebars)
                {
                    // display
                    rsltRebarSs += $"{rebar} + ";
                    // summary
                    var item = _smryS.FirstOrDefault(s => s.Item1 == rebar);
                    var amt = area.Amount;
                    if (item == default)
                    {
                        _smryS.Add((rebar, amt));
                    }
                    else
                    {
                        item.Item2 += amt;
                        _smryS[_smryS.FindIndex(s => s.Item1 == rebar)] = item;
                    }
                }
                rsltRebarSs = rsltRebarSs.Substring(0, rsltRebarSs.Length - 3);
                rsltRebarSs += "\n";
                rsltAmtSs += $"{area.Amount}本\n";
            }
            rsltRebarSs.Substring(0, rsltRebarSs.Length - 1);
            rsltAmtSs.Substring(0, rsltAmtSs.Length - 1);
        }

        // Reinforcement list for display
        private void RsForDisplay(List<AreaReinforcement> areaRs, out string rsltDRs, out string rsltRebarRs, out string rsltAmtRs)
        {
            rsltDRs = "";
            rsltRebarRs = "";
            rsltAmtRs = "";
            foreach (var area in areaRs)
            {
                rsltDRs += $"D{area.D}\n";
                foreach (var rebar in area.Rebars)
                {
                    // display
                    rsltRebarRs += $"{rebar} + ";
                    // summary
                    var d = area.D;
                    var item = _smryR.FirstOrDefault(s => s.Item1 == d && s.Item2 == rebar);
                    var amt = area.Amount;
                    if (item == default)
                    {
                        _smryR.Add((d, rebar, amt));
                    }
                    else
                    {
                        item.Item3 += amt;
                        _smryR[_smryR.FindIndex(s => s.Item1 == d && s.Item2 == rebar)] = item;
                    }
                }
                rsltRebarRs = rsltRebarRs.Substring(0, rsltRebarRs.Length - 3);
                rsltRebarRs += "\n";
                rsltAmtRs += $"{area.Amount}本\n";
            }
            rsltDRs.Substring(0, rsltDRs.Length - 1);
            rsltRebarRs.Substring(0, rsltRebarRs.Length - 1);
            rsltAmtRs.Substring(0, rsltAmtRs.Length - 1);
        }

        // Slab summary for display
        private void SmrySForDisplay()
        {
            var rslt = "";
            foreach (var item in _smryS)
            {
                rslt += $"{item.Item1} = {item.Item2}本\n";
            }
            rslt.Substring(0, rslt.Length - 1);
            rtxSmryS.Text = rslt;
        }

        //
        private void SmryRForDisplay()
        {
            var cc = _smryR.OrderBy(g => g.Item1).GroupBy(r => r.Item1).SelectMany(x => x).ToList();
            var rslt = "";
            foreach (var item in cc)
            {
                rslt += $"D{item.Item1} {item.Item2} = {item.Item3}本\n";
            }
            rslt.Substring(0, rslt.Length - 1);
            rtxSmryRD1.Text = rslt;
        }
        #endregion
    }
}
