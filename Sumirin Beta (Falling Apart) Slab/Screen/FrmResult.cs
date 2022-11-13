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

namespace Sumirin_Beta__Falling_Apart__Slab.Screen
{
    public partial class FrmResult : Form
    {
        #region Constructors
        public FrmResult()
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
        }
        #endregion

        #region Events
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
