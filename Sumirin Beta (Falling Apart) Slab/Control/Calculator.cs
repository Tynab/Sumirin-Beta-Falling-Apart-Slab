using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YANF.Control;

namespace Sumirin_Beta__Falling_Apart__Slab.Control
{
    public partial class Calculator : UserControl
    {
        #region Fields
        private string _detail = string.Empty;
        #endregion

        #region Constructors
        public Calculator()
        {
            InitializeComponent();
            InitItems();
            // btn all event
            foreach (var btn in _btnAlls)
            {
                btn.Click += BtnAll_Click;
            }
            // btn num event
            foreach (var btnN in _btnNs)
            {
                btnN.Click += BtnN_Click;
            }
        }
        #endregion

        #region Events
        // rtx detail text changed
        private void RtxDetail_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtxDetail.Text))
            {
                ReBlk();
            }
        }

        // btn "0" click
        private void BtnN0_Click(object sender, EventArgs e) => _detail += "0";

        // btn "1" click
        private void BtnN1_Click(object sender, EventArgs e) => _detail += "1";

        // btn "2" click
        private void BtnN2_Click(object sender, EventArgs e) => _detail += "2";

        // btn "3" click
        private void BtnN3_Click(object sender, EventArgs e) => _detail += "3";

        // btn "4" click
        private void BtnN4_Click(object sender, EventArgs e) => _detail += "4";

        // btn "5" click
        private void BtnN5_Click(object sender, EventArgs e) => _detail += "5";

        // btn "6" click
        private void BtnN6_Click(object sender, EventArgs e) => _detail += "6";

        // btn "7" click
        private void BtnN7_Click(object sender, EventArgs e) => _detail += "7";

        // btn "8" click
        private void BtnN8_Click(object sender, EventArgs e) => _detail += "8";

        // btn "9" click
        private void BtnN9_Click(object sender, EventArgs e) => _detail += "9";

        // btn "." click
        private void BtnDot_Click(object sender, EventArgs e) => _detail += ".";

        // btn reset click
        private void BtnC_Click(object sender, EventArgs e) => _detail = string.Empty;

        // btn backspace click
        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (_detail.Length > 0)
            {
                _detail.Remove(_detail.Length - 1);
            }
        }

        // btn "+" click
        private void BtnPlus_Click(object sender, EventArgs e) => _detail += "+";

        // btn "-" click
        private void BtnMinus_Click(object sender, EventArgs e) => _detail += "-";

        // btn "=" click
        private void BtnReturn_Click(object sender, EventArgs e)
        {
            // TODO:
        }
        #endregion

        #region Methods
        // Un-block
        private void UnBlk()
        {
            foreach (var btnBlk in _btnBlks)
            {
                btnBlk.Enabled = true;
            }
        }

        // Re-block
        private void ReBlk()
        {
            foreach (var btnBlk in _btnBlks)
            {
                btnBlk.Enabled = false;
            }
        }
        #endregion
    }
}
