using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static YANF.Script.YANEvent;
using static System.Windows.Forms.Keys;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant;
using YANF.Control;
using YANF.Script;
using Sumirin_Beta__Falling_Apart__Slab.Script.Model;

namespace Sumirin_Beta__Falling_Apart__Slab.Screen
{
    public partial class FrmResult : Form
    {
        #region Fields
        private List<Area> _areaSHs;
        private List<Area> _areaSVs;
        #endregion

        #region Constructors
        public FrmResult(List<Area> areaSHs, List<Area> areaSVs)
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
            foreach (var rtx in this.GetAllObjs(typeof(RichTextBox)))
            {
                rtx.KeyDown += All_KeyDown;
            }
            KeyDown += All_KeyDown;
            // get list
            _areaSHs = new List<Area>(areaSHs);
            _areaSVs = new List<Area>(areaSVs);
        }
        #endregion

        #region Events
        // frm load
        private void FrmResult_Load(object sender, EventArgs e)
        {
            //
            var rsltRebarSH = "";
            var rsltAmtSH = "";
            foreach (var area in _areaSHs)
            {
                foreach (var rebar in area.Rebars)
                {
                    rsltRebarSH += $"{rebar} + ";
                }
                rsltRebarSH = rsltRebarSH.Substring(0, rsltRebarSH.Length - 3);
                rsltRebarSH += "\n";
                rsltAmtSH += $"{area.Amount}本\n";
            }
            rsltAmtSH.Substring(0, rsltAmtSH.Length - 1);
            lblRsltRebarSH.Text = rsltRebarSH;
            lblRsltAmtSH.Text = rsltAmtSH;
            //
            var rsltRebarSV = "";
            var rsltAmtSV = "";
            foreach (var area in _areaSVs)
            {
                foreach (var rebar in area.Rebars)
                {
                    rsltRebarSV += $"{rebar} + ";
                }
                rsltRebarSV = rsltRebarSV.Substring(0, rsltRebarSV.Length - 3);
                rsltRebarSV += "\n";
                rsltAmtSV += $"{area.Amount}本\n";
            }
            rsltAmtSV.Substring(0, rsltAmtSV.Length - 1);
            lblRsltRebarSV.Text = rsltRebarSV;
            lblRsltAmtSV.Text = rsltAmtSV;
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
                Close();
                // sound
                SND_CHG.PlaySync();
            }
        }
        #endregion

        private void richTextBox1_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox1.DeselectAll();
            richTextBox1.Height = e.NewRectangle.Height;
            richTextBox1.Top = 20 + richTextBox1.Height / 2;
        }
    }
}
