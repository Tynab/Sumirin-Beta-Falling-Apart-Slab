using Sumirin_Beta__Falling_Apart__Slab.Script.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using YANF.Control;
using YANF.Script;
using static Sumirin_Beta__Falling_Apart__Slab.Script.EventHandler;
using static YANF.Script.YANEvent;

namespace Sumirin_Beta__Falling_Apart__Slab.Screen
{
    public partial class FrmResult : Form
    {
        #region Fields
        private readonly List<(string, int)> _smryS; // rebar, summary
        private readonly List<(int, string, int)> _smryR; // D, rebar, sumary
        private readonly List<AreaSlab> _areaSHs;
        private readonly List<AreaSlab> _areaSVs;
        private readonly List<AreaReinforcement> _areaRHs;
        private readonly List<AreaReinforcement> _areaRVs;
        private readonly bool _hasR;
        #endregion

        #region Constructors
        public FrmResult(List<AreaSlab> areaSHs, List<AreaSlab> areaSVs)
        {
            InitializeComponent();
            InitItems();
            // move frm by pnl
            foreach (var pnl in this.GetAllObjs(typeof(Panel)))
            {
                pnl.MouseDown += MoveFrmMod_MouseDown;
                pnl.MouseMove += MoveFrm_MouseMove;
                pnl.MouseUp += MoveFrm_MouseUp;
                pnl.KeyDown += Ctrl_KeyDown;
            }
            // move frm by gradient pnl
            foreach (var gradPnl in this.GetAllObjs(typeof(YANGradPnl)))
            {
                gradPnl.MouseDown += MoveFrmMod_MouseDown;
                gradPnl.MouseMove += MoveFrm_MouseMove;
                gradPnl.MouseUp += MoveFrm_MouseUp;
                gradPnl.KeyDown += Ctrl_KeyDown;
            }
            // move frm by lbl
            foreach (var lbl in this.GetAllObjs(typeof(Label)))
            {
                lbl.MouseDown += MoveFrmMod_MouseDown;
                lbl.MouseMove += MoveFrm_MouseMove;
                lbl.MouseUp += MoveFrm_MouseUp;
                lbl.KeyDown += Ctrl_KeyDown;
            }
            // move frm by lbl
            foreach (var rtx in this.GetAllObjs(typeof(RichTextBox)).Cast<RichTextBox>())
            {
                rtx.KeyDown += Ctrl_KeyDown;
                rtx.ContentsResized += Rtx_ContentsResized;
            }
            // this
            KeyDown += Ctrl_KeyDown;
            // get list
            _smryS = new List<(string, int)>();
            _areaSHs = new List<AreaSlab>(areaSHs);
            _areaSVs = new List<AreaSlab>(areaSVs);
            _hasR = false;
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
                pnl.KeyDown += Ctrl_KeyDown;
            }
            // move frm by gradient pnl
            foreach (var gradPnl in this.GetAllObjs(typeof(YANGradPnl)))
            {
                gradPnl.MouseDown += MoveFrmMod_MouseDown;
                gradPnl.MouseMove += MoveFrm_MouseMove;
                gradPnl.MouseUp += MoveFrm_MouseUp;
                gradPnl.KeyDown += Ctrl_KeyDown;
            }
            // move frm by lbl
            foreach (var lbl in this.GetAllObjs(typeof(Label)))
            {
                lbl.MouseDown += MoveFrmMod_MouseDown;
                lbl.MouseMove += MoveFrm_MouseMove;
                lbl.MouseUp += MoveFrm_MouseUp;
                lbl.KeyDown += Ctrl_KeyDown;
            }
            // move frm by lbl
            foreach (var rtx in this.GetAllObjs(typeof(RichTextBox)).Cast<RichTextBox>())
            {
                rtx.KeyDown += Ctrl_KeyDown;
                rtx.ContentsResized += Rtx_ContentsResized;
            }
            // this
            KeyDown += Ctrl_KeyDown;
            // get list
            _smryS = new List<(string, int)>();
            _areaSHs = new List<AreaSlab>(areaSHs);
            _areaSVs = new List<AreaSlab>(areaSVs);
            _smryR = new List<(int, string, int)>();
            _areaRHs = new List<AreaReinforcement>(areaRHs);
            _areaRVs = new List<AreaReinforcement>(areaRVs);
            _hasR = true;
        }
        #endregion

        #region Events
        // frm load
        private void FrmResult_Load(object sender, EventArgs e)
        {
            // display result slab horizontal
            SsForDisplay(_areaSHs, out var rsltRebarSH, out var rsltAmtSH);
            lblRsltRebarSH.Text = rsltRebarSH;
            lblRsltAmtSH.Text = rsltAmtSH;
            // display result slab vertical
            SsForDisplay(_areaSVs, out var rsltRebarSV, out var rsltAmtSV);
            lblRsltRebarSV.Text = rsltRebarSV;
            lblRsltAmtSV.Text = rsltAmtSV;
            // display slab reinforcement
            SmrySForDisplay();
            // reinforcement exist
            if (_hasR)
            {
                // display result reinforcement horizontal
                RsForDisplay(_areaRHs, out var rsltDRH, out var rsltRebarRH, out var rsltAmtRH);
                lblRsltDRH.Text = rsltDRH;
                lblRsltRebarRH.Text = rsltRebarRH;
                lblRsltAmtRH.Text = rsltAmtRH;
                // display result reinforcement vertical
                RsForDisplay(_areaRVs, out var rsltDRV, out var rsltRebarRV, out var rsltAmtRV);
                lblRsltDRV.Text = rsltDRV;
                lblRsltRebarRV.Text = rsltRebarRV;
                lblRsltAmtRV.Text = rsltAmtRV;
                // display summary reinforcement
                SmryRForDisplay();
            }
        }

        // frm shown
        private void FrmResult_Shown(object sender, EventArgs e) => this.FadeIn();

        // frm closing
        private void FrmResult_FormClosing(object sender, FormClosingEventArgs e) => this.FadeOut();
        #endregion

        #region Methods
        // Slab list for display
        private void SsForDisplay(List<AreaSlab> areaSs, out string rsltRebarSs, out string rsltAmtSs)
        {
            rsltRebarSs = string.Empty;
            rsltAmtSs = string.Empty;
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
                rsltRebarSs = rsltRebarSs.Substring(0, rsltRebarSs.Length - " + ".Length);
                rsltRebarSs += "\n";
                rsltAmtSs += $"{area.Amount}本\n";
            }
            rsltRebarSs.Substring(0, rsltRebarSs.Length - "\n".Length);
            rsltAmtSs.Substring(0, rsltAmtSs.Length - "\n".Length);
        }

        // Reinforcement list for display
        private void RsForDisplay(List<AreaReinforcement> areaRs, out string rsltDRs, out string rsltRebarRs, out string rsltAmtRs)
        {
            rsltDRs = string.Empty;
            rsltRebarRs = string.Empty;
            rsltAmtRs = string.Empty;
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
                rsltRebarRs = rsltRebarRs.Substring(0, rsltRebarRs.Length - " + ".Length);
                rsltRebarRs += "\n";
                rsltAmtRs += $"{area.Amount}本\n";
            }
            rsltDRs.Substring(0, rsltDRs.Length - "\n".Length);
            rsltRebarRs.Substring(0, rsltRebarRs.Length - "\n".Length);
            rsltAmtRs.Substring(0, rsltAmtRs.Length - "\n".Length);
        }

        // Slab summary for display
        private void SmrySForDisplay()
        {
            var rslt = string.Empty;
            foreach (var item in _smryS)
            {
                rslt += $"{item.Item1} = {item.Item2}本\n";
            }
            rslt.Substring(0, rslt.Length - "\n".Length);
            rtxSmryS.Text = rslt;
        }

        // Reinforcement summary for display
        private void SmryRForDisplay()
        {
            var splitList = _smryR.GroupBy(x => x.Item1).Select(g => g.ToList()).ToList();
            for (var i = 0; i < splitList.Count; i++)
            {
                var rslt = string.Empty;
                foreach (var item in splitList[i])
                {
                    rslt += $"{item.Item2} = {item.Item3}本\n";
                }
                rslt.Substring(0, rslt.Length - "\n".Length);
                // tranfer to ctrl
                foreach (var rtxSmryR in _rtxSmryRs)
                {
                    rtxSmryR.Text = rslt;
                }
                foreach (var lblSmryR in _lblSmryRs)
                {
                    lblSmryR.Text = splitList[i].First().Item1.ToString();
                }
            }
        }
        #endregion
    }
}
