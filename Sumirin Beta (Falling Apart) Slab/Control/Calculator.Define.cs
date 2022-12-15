﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sumirin_Beta__Falling_Apart__Slab.Control
{
    public partial class Calculator
    {
        private void InitItems()
        {
            InitCtrlAll();
            InitBtnAllRips();
            InitBtnBlks();
            InitBtnChns();
            InitBtnNs();
        }

        [DllImport("user32.dll", EntryPoint = "HideCaret")]
        public static extern long HideCaret(IntPtr hwnd);

        #region Ctrl
        private List<System.Windows.Forms.Control> _ctrlAll;
        // Initialize list ctrl all
        private void InitCtrlAll() => _ctrlAll = new List<System.Windows.Forms.Control>()
            {
                pnlBoard,
                lblResult,
                txtDetail,
                btnReturn,
                btnC,
                btnBksp,
                btnPlus,
                btnMinus,
                btnDot,
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

        #region Btn
        private List<Button> _btnAllRips;
        // Initialize list btn all rip
        private void InitBtnAllRips() => _btnAllRips = new List<Button>()
            {
                btnC,
                btnBksp,
                btnPlus,
                btnMinus,
                btnDot,
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
                btnBksp,
                btnPlus,
                btnMinus,
                btnDot,
                btnReturn
            };

        private List<Button> _btnChns;
        // Initialize list btnChn
        private void InitBtnChns() => _btnChns = new List<Button>()
            {
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
