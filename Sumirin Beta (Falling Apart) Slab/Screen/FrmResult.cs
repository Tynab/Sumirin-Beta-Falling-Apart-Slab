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
        private readonly List<(string, int)> _smryS0; // rebar, summary
        private readonly List<(string, int)> _smryS1; // rebar, summary
        private readonly List<(string, int)> _smryS2; // rebar, summary
        private readonly List<(int, string, int)> _smryR; // D, rebar, sumary
        private readonly List<(int, string, int)> _smryR0; // D, rebar, sumary
        private readonly List<(int, string, int)> _smryR1; // D, rebar, sumary
        private readonly List<(int, string, int)> _smryR2; // D, rebar, sumary
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
            _smryS0 = new List<(string, int)>();
            _smryS1 = new List<(string, int)>();
            _smryS2 = new List<(string, int)>();
            _areaSHs = new List<AreaSlab>(areaSHs);
            _areaSVs = new List<AreaSlab>(areaSVs);
            _hasR = false;
        }

        public FrmResult(List<AreaSlab> areaSHs, List<AreaSlab> areaSVs, List<AreaReinforcement> areaRHs, List<AreaReinforcement> areaRVs)
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
            _smryS0 = new List<(string, int)>();
            _smryS1 = new List<(string, int)>();
            _smryS2 = new List<(string, int)>();
            _areaSHs = new List<AreaSlab>(areaSHs);
            _areaSVs = new List<AreaSlab>(areaSVs);
            _smryR = new List<(int, string, int)>();
            _smryR0 = new List<(int, string, int)>();
            _smryR1 = new List<(int, string, int)>();
            _smryR2 = new List<(int, string, int)>();
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
            SsForDisplay(_areaSHs, out var rsltAreaSH, out var rsltRebarSH, out var rsltAmtSH);
            lblRsltASH.Text = rsltAreaSH;
            lblRsltRebarSH.Text = rsltRebarSH;
            lblRsltAmtSH.Text = rsltAmtSH;
            // display result slab vertical
            SsForDisplay(_areaSVs, out var rsltAreaSV, out var rsltRebarSV, out var rsltAmtSV);
            lblRsltASV.Text = rsltAreaSV;
            lblRsltRebarSV.Text = rsltRebarSV;
            lblRsltAmtSV.Text = rsltAmtSV;
            // display slab reinforcement
            SmrySForDisplay();
            // reinforcement exist
            if (_hasR)
            {
                // display result reinforcement horizontal
                RsForDisplay(_areaRHs, out var rsltAreaRH, out var rsltDRH, out var rsltRebarRH, out var rsltAmtRH);
                lblRsltARH.Text = rsltAreaRH;
                lblRsltDRH.Text = rsltDRH;
                lblRsltRebarRH.Text = rsltRebarRH;
                lblRsltAmtRH.Text = rsltAmtRH;
                // display result reinforcement vertical
                RsForDisplay(_areaRVs, out var rsltAreaRV, out var rsltDRV, out var rsltRebarRV, out var rsltAmtRV);
                lblRsltARV.Text = rsltAreaRV;
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
        // Split slab list bending head
        private void BdngHeadSpltSs(List<(string, int)> smryS, (int?, string) rebar, int amt)
        {
            var item = smryS.FirstOrDefault(s => s.Item1 == rebar.Item2);
            if (item == default)
            {
                smryS.Add((rebar.Item2, amt));
            }
            else
            {
                item.Item2 += amt;
                smryS[smryS.FindIndex(s => s.Item1 == rebar.Item2)] = item;
            }
        }

        // Split reinforcement list bending head
        private void BdngHeadSpltRs(List<(int, string, int)> smryR, int d, (int?, string) rebar, int amt)
        {
            var item = smryR.FirstOrDefault(s => s.Item1 == d && s.Item2 == rebar.Item2);
            if (item == default)
            {
                smryR.Add((d, rebar.Item2, amt));
            }
            else
            {
                item.Item3 += amt;
                smryR[smryR.FindIndex(s => s.Item1 == d && s.Item2 == rebar.Item2)] = item;
            }
        }

        // Prepare slab process
        private void PrepSPrcs(List<AreaSlab> areaSs)
        {
            var strtIdx = 0;
            for (var i = 1; i < areaSs.Count; i++)
            {
                if (areaSs[i].BendingHead == 1)
                {
                    if (areaSs[strtIdx].MainRebars.SequenceEqual(areaSs[i].MainRebars) && areaSs[strtIdx].SubRebars.SequenceEqual(areaSs[i].SubRebars))
                    {
                        // main
                        areaSs[i].MainRebars = new List<(int?, string)>
                        {
                            (null, "↑")
                        };
                        areaSs[i].MainAmount = 0;
                        // sub
                        areaSs[i].SubRebars = new List<(int?, string)>
                        {
                            (null, "↑")
                        };
                        areaSs[i].SubAmount = 0;
                        // reboot
                        areaSs[strtIdx].H += areaSs[i].H;
                        areaSs[strtIdx].Prcs();
                    }
                    else
                    {
                        strtIdx = i;
                    }
                }
                else
                {
                    if (areaSs[strtIdx].MainRebars.SequenceEqual(areaSs[i].MainRebars))
                    {
                        // main
                        areaSs[i].MainRebars = new List<(int?, string)>
                        {
                            (null, "↑")
                        };
                        areaSs[i].MainAmount = 0;
                        // reboot
                        areaSs[strtIdx].H += areaSs[i].H;
                        areaSs[strtIdx].Prcs();
                    }
                    else
                    {
                        strtIdx = i;
                    }
                }
            }
        }

        // Slab list for display
        private void SsForDisplay(List<AreaSlab> areaSs, out string rsltAreaSs, out string rsltRebarSs, out string rsltAmtSs)
        {
            PrepSPrcs(areaSs);
            rsltAreaSs = string.Empty;
            rsltRebarSs = string.Empty;
            rsltAmtSs = string.Empty;
            foreach (var area in areaSs)
            {
                // main
                var mainAmt = area.MainAmount;
                foreach (var rebar in area.MainRebars)
                {
                    rsltRebarSs += $"{rebar.Item2} + ";
                    switch (rebar.Item1)
                    {
                        case 0:
                        {
                            BdngHeadSpltSs(_smryS0, rebar, mainAmt);
                            break;
                        }
                        case 1:
                        {
                            BdngHeadSpltSs(_smryS1, rebar, mainAmt);
                            break;
                        }
                        case 2:
                        {
                            BdngHeadSpltSs(_smryS2, rebar, mainAmt);
                            break;
                        }
                    }
                }
                rsltAreaSs += $"{area.Area}-\n";
                rsltRebarSs = rsltRebarSs.Substring(0, rsltRebarSs.Length - " + ".Length);
                rsltAmtSs += $"{area.MainAmount}本\n";
                // sub
                if (area.SubRebars != null)
                {
                    var subAmt = (int)area.SubAmount;
                    rsltRebarSs += "\n(";
                    foreach (var rebar in area.SubRebars)
                    {
                        rsltRebarSs += $"{rebar.Item2} + ";
                        switch (rebar.Item1)
                        {
                            case 0:
                            {
                                BdngHeadSpltSs(_smryS0, rebar, subAmt);
                                break;
                            }
                            case 1:
                            {
                                BdngHeadSpltSs(_smryS1, rebar, subAmt);
                                break;
                            }
                            case 2:
                            {
                                BdngHeadSpltSs(_smryS2, rebar, subAmt);
                                break;
                            }
                        }
                    }
                    rsltAreaSs += "↑\n";
                    rsltRebarSs = rsltRebarSs.Substring(0, rsltRebarSs.Length - " + ".Length) + ")";
                    rsltAmtSs += $"{area.SubAmount}本\n";
                }
                rsltRebarSs += "\n";
            }
            rsltAreaSs = rsltAreaSs.Substring(0, rsltAreaSs.Length - "\n".Length);
            rsltRebarSs = rsltRebarSs.Substring(0, rsltRebarSs.Length - "\n".Length);
            rsltAmtSs = rsltAmtSs.Substring(0, rsltAmtSs.Length - "\n".Length);
        }

        // Reinforcement list for display
        private void RsForDisplay(List<AreaReinforcement> areaRs, out string rsltAreaRs, out string rsltDRs, out string rsltRebarRs, out string rsltAmtRs)
        {
            rsltAreaRs = string.Empty;
            rsltDRs = string.Empty;
            rsltRebarRs = string.Empty;
            rsltAmtRs = string.Empty;
            foreach (var area in areaRs)
            {
                var d = area.D;
                var mainAmt = area.MainAmount;
                rsltDRs += $"D{area.D}\n";
                foreach (var rebar in area.MainRebars)
                {
                    rsltRebarRs += $"{rebar.Item2} + ";
                    switch (rebar.Item1)
                    {
                        case 0:
                        {
                            BdngHeadSpltRs(_smryR0, d, rebar, mainAmt);
                            break;
                        }
                        case 1:
                        {
                            BdngHeadSpltRs(_smryR1, d, rebar, mainAmt);
                            break;
                        }
                        case 2:
                        {
                            BdngHeadSpltRs(_smryR2, d, rebar, mainAmt);
                            break;
                        }
                    }
                }
                rsltAreaRs += $"{area.Area}-\n";
                rsltRebarRs = rsltRebarRs.Substring(0, rsltRebarRs.Length - " + ".Length);
                rsltAmtRs += $"{area.MainAmount}本\n";
                if (area.SubRebars != null)
                {
                    var subAmt = (int)area.SubAmount;
                    rsltDRs += $"\nD{area.D}\n";
                    rsltRebarRs += "\n(";
                    foreach (var rebar in area.MainRebars)
                    {
                        rsltRebarRs += $"{rebar.Item2} + ";
                        switch (rebar.Item1)
                        {
                            case 0:
                            {
                                BdngHeadSpltRs(_smryR0, d, rebar, subAmt);
                                break;
                            }
                            case 1:
                            {
                                BdngHeadSpltRs(_smryR1, d, rebar, subAmt);
                                break;
                            }
                            case 2:
                            {
                                BdngHeadSpltRs(_smryR2, d, rebar, subAmt);
                                break;
                            }
                        }
                    }
                    rsltAreaRs += "↑\n";
                    rsltRebarRs = rsltRebarRs.Substring(0, rsltRebarRs.Length - " + ".Length) + ")";
                    rsltAmtRs += $"{area.SubAmount}本\n";
                }
                rsltRebarRs += "\n";
            }
            rsltAreaRs = rsltAreaRs.Substring(0, rsltAreaRs.Length - "\n".Length);
            rsltDRs = rsltDRs.Substring(0, rsltDRs.Length - "\n".Length);
            rsltRebarRs = rsltRebarRs.Substring(0, rsltRebarRs.Length - "\n".Length);
            rsltAmtRs = rsltAmtRs.Substring(0, rsltAmtRs.Length - "\n".Length);
        }

        // Slab summary for display
        private void SmrySForDisplay()
        {
            var rslt = string.Empty;
            _smryS.AddRange(_smryS2.OrderByDescending(x => int.Parse(x.Item1.Split('×')[1])).ToList());
            _smryS.AddRange(_smryS1.OrderByDescending(x => int.Parse(x.Item1.Split('×')[1])).ToList());
            _smryS.AddRange(_smryS0.OrderByDescending(x => int.Parse(x.Item1)).ToList());
            _smryS.ForEach(x => rslt += $"{x.Item1} = {x.Item2}本\n");
            rtxSmryS.Text = rslt.Substring(0, rslt.Length - "\n".Length);
        }

        // Reinforcement summary for display
        private void SmryRForDisplay()
        {
            _smryR.AddRange(_smryR2.OrderByDescending(x => int.Parse(x.Item2.Split('×')[1])).ToList());
            _smryR.AddRange(_smryR1.OrderByDescending(x => int.Parse(x.Item2.Split('×')[1])).ToList());
            _smryR.AddRange(_smryR0.OrderByDescending(x => int.Parse(x.Item2)).ToList());
            // display
            var dSmrys = _smryR.GroupBy(x => x.Item1).Select(g => g.ToList()).ToList();
            for (var i = 0; i < dSmrys.Count; i++)
            {
                var rslt = string.Empty;
                foreach (var item in dSmrys[i])
                {
                    rslt += $"{item.Item2} = {item.Item3}本\n";
                }
                // tranfer to ctrl
                _rtxSmryRs[i].Text = rslt.Substring(0, rslt.Length - "\n".Length);
                _lblSmryRs[i].Text = dSmrys[i].First().Item1.ToString();
            }
        }
        #endregion
    }
}
