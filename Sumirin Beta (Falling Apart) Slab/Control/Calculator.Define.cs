using System.Collections.Generic;
using System.Windows.Forms;

namespace Sumirin_Beta__Falling_Apart__Slab.Control
{
    public partial class Calculator
    {
        private void InitItems()
        {
            InitBtnAlls();
            InitBtnBlks();
            InitBtnNs();
        }

        #region Btn
        private List<Button> _btnAlls;
        // Initialize list btns
        private void InitBtnAlls() => _btnAlls = new List<Button>()
            {
                btnC,
                btnBack,
                btnPlus,
                btnMinus,
                btnDot,
                btnReturn,
                btnN0,
                btnN1,
                btnN2,
                btnN3,
                btnN4,
                btnN5,
                btnN6,
                btnN7,
                btnN8,
                btnN9
            };

        private List<Button> _btnBlks;
        // Initialize list btnBlk
        private void InitBtnBlks() => _btnBlks = new List<Button>()
            {
                btnC,
                btnBack,
                btnPlus,
                btnMinus,
                btnDot,
                btnReturn
            };

        private List<Button> _btnNs;
        // Initialize list btnN
        private void InitBtnNs() => _btnNs = new List<Button>()
            {
                btnN0,
                btnN1,
                btnN2,
                btnN3,
                btnN4,
                btnN5,
                btnN6,
                btnN7,
                btnN8,
                btnN9
            };
        #endregion
    }
}
