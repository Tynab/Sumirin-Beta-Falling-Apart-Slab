using Sumirin_Beta__Falling_Apart__Slab.Script;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Drawing.Color;

namespace Sumirin_Beta__Falling_Apart__Slab.Control
{
    public partial class Calculator : UserControl
    {
        #region Fields
        private readonly NumericUpDown _nud;
        private string _detail = string.Empty;
        #endregion

        #region Constructors
        public Calculator(NumericUpDown nud)
        {
            InitializeComponent();
            InitItems();
            // ctrl all event
            foreach (var ctrl in _ctrlAll)
            {
                ctrl.KeyDown += Ctrl_KeyDown;
            }
            KeyDown += Ctrl_KeyDown;
            // btn all event
            foreach (var btn in _btnAllRips)
            {
                btn.Click += BtnAll_Click;
            }
            // btn num event
            foreach (var btnN in _btnNs)
            {
                btnN.Click += BtnN_Click;
            }
            txtDetail.GotFocus += Txt_GotFocus;
            Disposed += OnDispose;
            // option
            _nud = nud;
            _nud.BackColor = OrangeRed;
        }
        #endregion

        #region Events
        // ctrl leave
        private void Calculator_Leave(object sender, EventArgs e) => Dispose();

        // txt detail text changed
        private void TxtDetail_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDetail.Text))
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
        private void BtnDot_Click(object sender, EventArgs e)
        {
            _detail += ".";
            DsblChainBtn();
        }

        // btn reset click
        private void BtnC_Click(object sender, EventArgs e)
        {
            _detail = string.Empty;
            lblResult.Text = "0";
        }

        // btn backspace click
        private void BtnBksp_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_detail))
            {
                _detail = _detail.Remove(_detail.Length - 1);
                if (!string.IsNullOrWhiteSpace(_detail) && int.TryParse(_detail[_detail.Length - 1].ToString(), out var _))
                {
                    EnblChainBtn();
                }
                else
                {
                    DsblChainBtn();
                }
            }
        }

        // btn "+" click
        private void BtnPlus_Click(object sender, EventArgs e)
        {
            _detail += "+";
            DsblChainBtn();
        }

        // btn "-" click
        private void BtnMinus_Click(object sender, EventArgs e)
        {
            _detail += "-";
            DsblChainBtn();
        }

        // btn "=" click
        private void BtnReturn_Click(object sender, EventArgs e)
        {
            var rslt = MathGPrcsText(_detail);
            lblResult.Text = rslt.ToString("N0");
            _nud.Value = rslt;
            Dispose();
        }
        #endregion

        #region Methods
        // btn num active
        private void BtnNAct(Button btnN)
        {
            BtnN_Click(btnN, null);
            BtnAll_Click(btnN, null);
        }

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

        // Enable chain button
        private void EnblChainBtn()
        {
            foreach (var btnChn in _btnChns)
            {
                btnChn.Enabled = true;
            }
        }

        // Disable chain button
        private void DsblChainBtn()
        {
            foreach (var btnChn in _btnChns)
            {
                btnChn.Enabled = false;
            }
        }

        // Math (with G) process text
        private decimal MathGPrcsText(string text)
        {
            var rslt = 0m;
            var nums = new List<decimal>();
            var oprs = new List<string>();
            var strtPt = 0;
            var nChar = 0;
            // split number in text
            for (var i = 0; i < text.Length; i++)
            {
                nChar++;
                if (_detail[i] is '+' or '-')
                {
                    nums.Add(decimal.Parse(_detail.Substring(strtPt, nChar - 1)).ToGSpan());
                    oprs.Add(_detail[i].ToString());
                    strtPt = i + 1;
                    nChar = 0;
                }
                if (i == text.Length - 1)
                {
                    nums.Add(decimal.Parse(_detail.Substring(strtPt, nChar)).ToGSpan());
                }
            }
            // math process
            rslt += nums[0];
            for (var i = 0; i < oprs.Count; i++)
            {
                if (oprs[i] == "+")
                {
                    rslt += nums[i + 1];
                }
                else
                {
                    rslt -= nums[i + 1];
                }
            }
            return rslt;
        }
        #endregion
    }
}
