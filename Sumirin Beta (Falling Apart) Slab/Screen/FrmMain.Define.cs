using System.Collections.Generic;
using System.Windows.Forms;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Settings;

namespace Sumirin_Beta__Falling_Apart__Slab.Screen
{
    public partial class FrmMain
    {
        // define
        private readonly int _span = Default.Span;

        // Initialize items
        private void InitItems()
        {
            // pnl
            InitPnlAs();
            InitPnlIs();
            // lbl
            InitLblTits();
            // chk
            InitChkAs();
            InitChkASs();
            InitChkASHs();
            InitChkASVs();
            InitChkARs();
            InitChkIs();
            InitChkOns();
            InitChkOffs();
            InitChkBLs();
            InitChkBLSHs();
            InitChkBLSVs();
            InitChkBLRHs();
            InitChkBLRVs();
            InitChkBRs();
            InitChkBRSHs();
            InitChkBRSVs();
            InitChkBRRHs();
            InitChkBRRVs();
            InitChkFLs();
            InitChkFLRHs();
            InitChkFLRVs();
            InitChkFRs();
            InitChkFRRHs();
            InitChkFRRVs();
            // nud
            InitNudWs();
            InitNudWSHs();
            InitNudWSVs();
            InitNudWRHs();
            InitNudWRVs();
            InitNudHs();
            InitNudHSHs();
            InitNudHSVs();
            InitNudHRHs();
            InitNudHRVs();
            InitNudDs();
            InitNudDRs();
            InitNudDRHs();
            InitNudDRVs();
            InitNudTits();
            InitNudGs();
        }

        #region Pnl
        private List<Panel> _pnlAs;
        // Initialize list pnlA
        private void InitPnlAs() => _pnlAs = new List<Panel>
            {
                pnlASH1,
                pnlASH2,
                pnlASH3,
                pnlASH4,
                pnlASH5,
                pnlASH6,
                pnlASH7,
                pnlASH8,
                pnlASH9,
                pnlASH10,
                pnlASV1,
                pnlASV2,
                pnlASV3,
                pnlASV4,
                pnlASV5,
                pnlASV6,
                pnlASV7,
                pnlASV8,
                pnlASV9,
                pnlASV10,
                pnlARH1,
                pnlARH2,
                pnlARH3,
                pnlARH4,
                pnlARH5,
                pnlARH6,
                pnlARV1,
                pnlARV2,
                pnlARV3,
                pnlARV4,
                pnlARV5,
                pnlARV6
            };

        private List<Panel> _pnlIs;
        // Initialize list pnlI
        private void InitPnlIs() => _pnlIs = new List<Panel>
            {
                pnlISH1,
                pnlISH2,
                pnlISH3,
                pnlISH4,
                pnlISH5,
                pnlISH6,
                pnlISH7,
                pnlISH8,
                pnlISH9,
                pnlISH10,
                pnlISV1,
                pnlISV2,
                pnlISV3,
                pnlISV4,
                pnlISV5,
                pnlISV6,
                pnlISV7,
                pnlISV8,
                pnlISV9,
                pnlISV10,
                pnlIRH1,
                pnlIRH2,
                pnlIRH3,
                pnlIRH4,
                pnlIRH5,
                pnlIRH6,
                pnlIRV1,
                pnlIRV2,
                pnlIRV3,
                pnlIRV4,
                pnlIRV5,
                pnlIRV6
            };
        #endregion

        #region Lbl
        private List<Label> _lblTits;
        // Initialize list lblTit
        private void InitLblTits() => _lblTits = new List<Label>
            {
                lblWSH1,
                lblWSH2,
                lblWSH3,
                lblWSH4,
                lblWSH5,
                lblWSH6,
                lblWSH7,
                lblWSH8,
                lblWSH9,
                lblWSH10,
                lblWSV1,
                lblWSV2,
                lblWSV3,
                lblWSV4,
                lblWSV5,
                lblWSV6,
                lblWSV7,
                lblWSV8,
                lblWSV9,
                lblWSV10,
                lblWRH1,
                lblWRH2,
                lblWRH3,
                lblWRH4,
                lblWRH5,
                lblWRH6,
                lblWRV1,
                lblWRV2,
                lblWRV3,
                lblWRV4,
                lblWRV5,
                lblWRV6,
                lblHSH1,
                lblHSH2,
                lblHSH3,
                lblHSH4,
                lblHSH5,
                lblHSH6,
                lblHSH7,
                lblHSH8,
                lblHSH9,
                lblHSH10,
                lblHSV1,
                lblHSV2,
                lblHSV3,
                lblHSV4,
                lblHSV5,
                lblHSV6,
                lblHSV7,
                lblHSV8,
                lblHSV9,
                lblHSV10,
                lblHRH1,
                lblHRH2,
                lblHRH3,
                lblHRH4,
                lblHRH5,
                lblHRH6,
                lblHRV1,
                lblHRV2,
                lblHRV3,
                lblHRV4,
                lblHRV5,
                lblHRV6,
                lblDRH1,
                lblDRH2,
                lblDRH3,
                lblDRH4,
                lblDRH5,
                lblDRH6,
                lblDRV1,
                lblDRV2,
                lblDRV3,
                lblDRV4,
                lblDRV5,
                lblDRV6
            };
        #endregion

        #region Chk
        private List<CheckBox> _chkAs;
        // Initialize list chkA
        private void InitChkAs() => _chkAs = new List<CheckBox>
            {
                chkASH1,
                chkASH2,
                chkASH3,
                chkASH4,
                chkASH5,
                chkASH6,
                chkASH7,
                chkASH8,
                chkASH9,
                chkASH10,
                chkASV1,
                chkASV2,
                chkASV3,
                chkASV4,
                chkASV5,
                chkASV6,
                chkASV7,
                chkASV8,
                chkASV9,
                chkASV10,
                chkARH1,
                chkARH2,
                chkARH3,
                chkARH4,
                chkARH5,
                chkARH6,
                chkARV1,
                chkARV2,
                chkARV3,
                chkARV4,
                chkARV5,
                chkARV6
            };

        private List<CheckBox> _chkASs;
        // Initialize list chkAS
        private void InitChkASs() => _chkASs = new List<CheckBox>
            {
                chkASH1,
                chkASH2,
                chkASH3,
                chkASH4,
                chkASH5,
                chkASH6,
                chkASH7,
                chkASH8,
                chkASH9,
                chkASH10,
                chkASV1,
                chkASV2,
                chkASV3,
                chkASV4,
                chkASV5,
                chkASV6,
                chkASV7,
                chkASV8,
                chkASV9,
                chkASV10
            };

        private List<CheckBox> _chkASHs;
        // Initialize list chkASH
        private void InitChkASHs() => _chkASHs = new List<CheckBox>
            {
                chkASH1,
                chkASH2,
                chkASH3,
                chkASH4,
                chkASH5,
                chkASH6,
                chkASH7,
                chkASH8,
                chkASH9,
                chkASH10
            };

        private List<CheckBox> _chkASVs;
        // Initialize list chkAS
        private void InitChkASVs() => _chkASVs = new List<CheckBox>
            {
                chkASV1,
                chkASV2,
                chkASV3,
                chkASV4,
                chkASV5,
                chkASV6,
                chkASV7,
                chkASV8,
                chkASV9,
                chkASV10
            };

        private List<CheckBox> _chkARs;
        // Initialize list chkAR
        private void InitChkARs() => _chkARs = new List<CheckBox>
            {
                chkARH1,
                chkARH2,
                chkARH3,
                chkARH4,
                chkARH5,
                chkARH6,
                chkARV1,
                chkARV2,
                chkARV3,
                chkARV4,
                chkARV5,
                chkARV6
            };

        private List<CheckBox> _chkIs;
        // Initialize list chkI
        private void InitChkIs() => _chkIs = new List<CheckBox>
            {
                chkBLSH1,
                chkBRSH1,
                chkBLSH2,
                chkBRSH2,
                chkBLSH3,
                chkBRSH3,
                chkBLSH4,
                chkBRSH4,
                chkBLSH5,
                chkBRSH5,
                chkBLSH6,
                chkBRSH6,
                chkBLSH7,
                chkBRSH7,
                chkBLSH8,
                chkBRSH8,
                chkBLSH9,
                chkBRSH9,
                chkBLSH10,
                chkBRSH10,
                chkBLSV1,
                chkBRSV1,
                chkBLSV2,
                chkBRSV2,
                chkBLSV3,
                chkBRSV3,
                chkBLSV4,
                chkBRSV4,
                chkBLSV5,
                chkBRSV5,
                chkBLSV6,
                chkBRSV6,
                chkBLSV7,
                chkBRSV7,
                chkBLSV8,
                chkBRSV8,
                chkBLSV9,
                chkBRSV9,
                chkBLSV10,
                chkBRSV10,
                chkBLRH1,
                chkBRRH1,
                chkBLRH2,
                chkBRRH2,
                chkBLRH3,
                chkBRRH3,
                chkBLRH4,
                chkBRRH4,
                chkBLRH5,
                chkBRRH5,
                chkBLRH6,
                chkBRRH6,
                chkBLRV1,
                chkBRRV1,
                chkBLRV2,
                chkBRRV2,
                chkBLRV3,
                chkBRRV3,
                chkBLRV4,
                chkBRRV4,
                chkBLRV5,
                chkBRRV5,
                chkBLRV6,
                chkBRRV6,
                chkFLRH1,
                chkFRRH1,
                chkFLRH2,
                chkFRRH2,
                chkFLRH3,
                chkFRRH3,
                chkFLRH4,
                chkFRRH4,
                chkFLRH5,
                chkFRRH5,
                chkFLRH6,
                chkFRRH6,
                chkFLRV1,
                chkFRRV1,
                chkFLRV2,
                chkFRRV2,
                chkFLRV3,
                chkFRRV3,
                chkFLRV4,
                chkFRRV4,
                chkFLRV5,
                chkFRRV5,
                chkFLRV6,
                chkFRRV6
            };

        private List<CheckBox> _chkOns;
        // Initialize list chkOn
        private void InitChkOns() => _chkOns = new List<CheckBox>
            {
                chkBLSH1,
                chkBRSH1,
                chkBLSH2,
                chkBRSH2,
                chkBLSH3,
                chkBRSH3,
                chkBLSH4,
                chkBRSH4,
                chkBLSH5,
                chkBRSH5,
                chkBLSH6,
                chkBRSH6,
                chkBLSH7,
                chkBRSH7,
                chkBLSH8,
                chkBRSH8,
                chkBLSH9,
                chkBRSH9,
                chkBLSH10,
                chkBRSH10,
                chkBLSV1,
                chkBRSV1,
                chkBLSV2,
                chkBRSV2,
                chkBLSV3,
                chkBRSV3,
                chkBLSV4,
                chkBRSV4,
                chkBLSV5,
                chkBRSV5,
                chkBLSV6,
                chkBRSV6,
                chkBLSV7,
                chkBRSV7,
                chkBLSV8,
                chkBRSV8,
                chkBLSV9,
                chkBRSV9,
                chkBLSV10,
                chkBRSV10,
                chkFLRH1,
                chkFRRH1,
                chkFLRH2,
                chkFRRH2,
                chkFLRH3,
                chkFRRH3,
                chkFLRH4,
                chkFRRH4,
                chkFLRH5,
                chkFRRH5,
                chkFLRH6,
                chkFRRH6,
                chkFLRV1,
                chkFRRV1,
                chkFLRV2,
                chkFRRV2,
                chkFLRV3,
                chkFRRV3,
                chkFLRV4,
                chkFRRV4,
                chkFLRV5,
                chkFRRV5,
                chkFLRV6,
                chkFRRV6
            };

        private List<CheckBox> _chkOffs;
        // Initialize list chkOff
        private void InitChkOffs() => _chkOffs = new List<CheckBox>
            {
                chkBLRH1,
                chkBRRH1,
                chkBLRH2,
                chkBRRH2,
                chkBLRH3,
                chkBRRH3,
                chkBLRH4,
                chkBRRH4,
                chkBLRH5,
                chkBRRH5,
                chkBLRH6,
                chkBRRH6,
                chkBLRV1,
                chkBRRV1,
                chkBLRV2,
                chkBRRV2,
                chkBLRV3,
                chkBRRV3,
                chkBLRV4,
                chkBRRV4,
                chkBLRV5,
                chkBRRV5,
                chkBLRV6,
                chkBRRV6
            };

        private List<CheckBox> _chkBLs;
        // Initialize list chkBL
        private void InitChkBLs() => _chkBLs = new List<CheckBox>
            {
                chkBLSH1,
                chkBLSH2,
                chkBLSH3,
                chkBLSH4,
                chkBLSH5,
                chkBLSH6,
                chkBLSH7,
                chkBLSH8,
                chkBLSH9,
                chkBLSH10,
                chkBLSV1,
                chkBLSV2,
                chkBLSV3,
                chkBLSV4,
                chkBLSV5,
                chkBLSV6,
                chkBLSV7,
                chkBLSV8,
                chkBLSV9,
                chkBLSV10,
                chkBLRH1,
                chkBLRH2,
                chkBLRH3,
                chkBLRH4,
                chkBLRH5,
                chkBLRH6,
                chkBLRV1,
                chkBLRV2,
                chkBLRV3,
                chkBLRV4,
                chkBLRV5,
                chkBLRV6
            };

        private List<CheckBox> _chkBLSHs;
        // Initialize list chkBLSH
        private void InitChkBLSHs() => _chkBLSHs = new List<CheckBox>
            {
                chkBLSH1,
                chkBLSH2,
                chkBLSH3,
                chkBLSH4,
                chkBLSH5,
                chkBLSH6,
                chkBLSH7,
                chkBLSH8,
                chkBLSH9,
                chkBLSH10
            };

        private List<CheckBox> _chkBLSVs;
        // Initialize list chkBLSV
        private void InitChkBLSVs() => _chkBLSVs = new List<CheckBox>
            {
                chkBLSV1,
                chkBLSV2,
                chkBLSV3,
                chkBLSV4,
                chkBLSV5,
                chkBLSV6,
                chkBLSV7,
                chkBLSV8,
                chkBLSV9,
                chkBLSV10
            };

        private List<CheckBox> _chkBLRHs;
        // Initialize list chkBLRH
        private void InitChkBLRHs() => _chkBLRHs = new List<CheckBox>
            {
                chkBLRH1,
                chkBLRH2,
                chkBLRH3,
                chkBLRH4,
                chkBLRH5,
                chkBLRH6
            };

        private List<CheckBox> _chkBLRVs;
        // Initialize list chkBLRV
        private void InitChkBLRVs() => _chkBLRVs = new List<CheckBox>
            {
                chkBLRV1,
                chkBLRV2,
                chkBLRV3,
                chkBLRV4,
                chkBLRV5,
                chkBLRV6
            };

        private List<CheckBox> _chkBRs;
        // Initialize list chkBR
        private void InitChkBRs() => _chkBRs = new List<CheckBox>
            {
                chkBRSH1,
                chkBRSH2,
                chkBRSH3,
                chkBRSH4,
                chkBRSH5,
                chkBRSH6,
                chkBRSH7,
                chkBRSH8,
                chkBRSH9,
                chkBRSH10,
                chkBRSV1,
                chkBRSV2,
                chkBRSV3,
                chkBRSV4,
                chkBRSV5,
                chkBRSV6,
                chkBRSV7,
                chkBRSV8,
                chkBRSV9,
                chkBRSV10,
                chkBRRH1,
                chkBRRH2,
                chkBRRH3,
                chkBRRH4,
                chkBRRH5,
                chkBRRH6,
                chkBRRV1,
                chkBRRV2,
                chkBRRV3,
                chkBRRV4,
                chkBRRV5,
                chkBRRV6
            };

        private List<CheckBox> _chkBRSHs;
        // Initialize list chkBRSH
        private void InitChkBRSHs() => _chkBRSHs = new List<CheckBox>
            {
                chkBRSH1,
                chkBRSH2,
                chkBRSH3,
                chkBRSH4,
                chkBRSH5,
                chkBRSH6,
                chkBRSH7,
                chkBRSH8,
                chkBRSH9,
                chkBRSH10
            };

        private List<CheckBox> _chkBRSVs;
        // Initialize list chkBRSV
        private void InitChkBRSVs() => _chkBRSVs = new List<CheckBox>
            {
                chkBRSV1,
                chkBRSV2,
                chkBRSV3,
                chkBRSV4,
                chkBRSV5,
                chkBRSV6,
                chkBRSV7,
                chkBRSV8,
                chkBRSV9,
                chkBRSV10
            };

        private List<CheckBox> _chkBRRHs;
        // Initialize list chkBRRH
        private void InitChkBRRHs() => _chkBRRHs = new List<CheckBox>
            {
                chkBRRH1,
                chkBRRH2,
                chkBRRH3,
                chkBRRH4,
                chkBRRH5,
                chkBRRH6
            };

        private List<CheckBox> _chkBRRVs;
        // Initialize list chkBRRV
        private void InitChkBRRVs() => _chkBRRVs = new List<CheckBox>
            {
                chkBRRV1,
                chkBRRV2,
                chkBRRV3,
                chkBRRV4,
                chkBRRV5,
                chkBRRV6
            };

        private List<CheckBox> _chkFLs;
        // Initialize list chkFL
        private void InitChkFLs() => _chkFLs = new List<CheckBox>
            {
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                chkFLRH1,
                chkFLRH2,
                chkFLRH3,
                chkFLRH4,
                chkFLRH5,
                chkFLRH6,
                chkFLRV1,
                chkFLRV2,
                chkFLRV3,
                chkFLRV4,
                chkFLRV5,
                chkFLRV6
            };

        private List<CheckBox> _chkFLRHs;
        // Initialize list chkFLRH
        private void InitChkFLRHs() => _chkFLRHs = new List<CheckBox>
            {
                chkFLRH1,
                chkFLRH2,
                chkFLRH3,
                chkFLRH4,
                chkFLRH5,
                chkFLRH6
            };

        private List<CheckBox> _chkFLRVs;
        // Initialize list chkFLRV
        private void InitChkFLRVs() => _chkFLRVs = new List<CheckBox>
            {
                chkFLRV1,
                chkFLRV2,
                chkFLRV3,
                chkFLRV4,
                chkFLRV5,
                chkFLRV6
            };

        private List<CheckBox> _chkFRs;
        // Initialize list chkFL
        private void InitChkFRs() => _chkFRs = new List<CheckBox>
            {
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                chkFRRH1,
                chkFRRH2,
                chkFRRH3,
                chkFRRH4,
                chkFRRH5,
                chkFRRH6,
                chkFRRV1,
                chkFRRV2,
                chkFRRV3,
                chkFRRV4,
                chkFRRV5,
                chkFRRV6
            };

        private List<CheckBox> _chkFRRHs;
        // Initialize list chkFRRH
        private void InitChkFRRHs() => _chkFRRHs = new List<CheckBox>
            {
                chkFRRH1,
                chkFRRH2,
                chkFRRH3,
                chkFRRH4,
                chkFRRH5,
                chkFRRH6
            };

        private List<CheckBox> _chkFRRVs;
        // Initialize list chkFRRV
        private void InitChkFRRVs() => _chkFRRVs = new List<CheckBox>
            {
                chkFRRV1,
                chkFRRV2,
                chkFRRV3,
                chkFRRV4,
                chkFRRV5,
                chkFRRV6
            };
        #endregion

        #region Nud
        private List<NumericUpDown> _nudWs;
        // Initialize list nudW
        private void InitNudWs() => _nudWs = new List<NumericUpDown>
            {
                nudWSH1,
                nudWSH2,
                nudWSH3,
                nudWSH4,
                nudWSH5,
                nudWSH6,
                nudWSH7,
                nudWSH8,
                nudWSH9,
                nudWSH10,
                nudWSV1,
                nudWSV2,
                nudWSV3,
                nudWSV4,
                nudWSV5,
                nudWSV6,
                nudWSV7,
                nudWSV8,
                nudWSV9,
                nudWSV10,
                nudWRH1,
                nudWRH2,
                nudWRH3,
                nudWRH4,
                nudWRH5,
                nudWRH6,
                nudWRV1,
                nudWRV2,
                nudWRV3,
                nudWRV4,
                nudWRV5,
                nudWRV6
            };

        private List<NumericUpDown> _nudWSHs;
        // Initialize list nudWSH
        private void InitNudWSHs() => _nudWSHs = new List<NumericUpDown>
            {
                nudWSH1,
                nudWSH2,
                nudWSH3,
                nudWSH4,
                nudWSH5,
                nudWSH6,
                nudWSH7,
                nudWSH8,
                nudWSH9,
                nudWSH10
            };

        private List<NumericUpDown> _nudWSVs;
        // Initialize list nudWSV
        private void InitNudWSVs() => _nudWSVs = new List<NumericUpDown>
            {
                nudWSV1,
                nudWSV2,
                nudWSV3,
                nudWSV4,
                nudWSV5,
                nudWSV6,
                nudWSV7,
                nudWSV8,
                nudWSV9,
                nudWSV10
            };

        private List<NumericUpDown> _nudWRHs;
        // Initialize list nudWRH
        private void InitNudWRHs() => _nudWRHs = new List<NumericUpDown>
            {
                nudWRH1,
                nudWRH2,
                nudWRH3,
                nudWRH4,
                nudWRH5,
                nudWRH6
            };

        private List<NumericUpDown> _nudWRVs;
        // Initialize list nudWRV
        private void InitNudWRVs() => _nudWRVs = new List<NumericUpDown>
            {
                nudWRV1,
                nudWRV2,
                nudWRV3,
                nudWRV4,
                nudWRV5,
                nudWRV6
            };

        private List<NumericUpDown> _nudHs;
        // Initialize list nudH
        private void InitNudHs() => _nudHs = new List<NumericUpDown>
            {
                nudHSH1,
                nudHSH2,
                nudHSH3,
                nudHSH4,
                nudHSH5,
                nudHSH6,
                nudHSH7,
                nudHSH8,
                nudHSH9,
                nudHSH10,
                nudHSV1,
                nudHSV2,
                nudHSV3,
                nudHSV4,
                nudHSV5,
                nudHSV6,
                nudHSV7,
                nudHSV8,
                nudHSV9,
                nudHSV10,
                nudHRH1,
                nudHRH2,
                nudHRH3,
                nudHRH4,
                nudHRH5,
                nudHRH6,
                nudHRV1,
                nudHRV2,
                nudHRV3,
                nudHRV4,
                nudHRV5,
                nudHRV6
            };

        private List<NumericUpDown> _nudHSHs;
        // Initialize list nudHSH
        private void InitNudHSHs() => _nudHSHs = new List<NumericUpDown>
            {
                nudHSH1,
                nudHSH2,
                nudHSH3,
                nudHSH4,
                nudHSH5,
                nudHSH6,
                nudHSH7,
                nudHSH8,
                nudHSH9,
                nudHSH10
            };

        private List<NumericUpDown> _nudHSVs;
        // Initialize list nudHSV
        private void InitNudHSVs() => _nudHSVs = new List<NumericUpDown>
            {
                nudHSV1,
                nudHSV2,
                nudHSV3,
                nudHSV4,
                nudHSV5,
                nudHSV6,
                nudHSV7,
                nudHSV8,
                nudHSV9,
                nudHSV10
            };

        private List<NumericUpDown> _nudHRHs;
        // Initialize list nudHRH
        private void InitNudHRHs() => _nudHRHs = new List<NumericUpDown>
            {
                nudHRH1,
                nudHRH2,
                nudHRH3,
                nudHRH4,
                nudHRH5,
                nudHRH6
            };

        private List<NumericUpDown> _nudHRVs;
        // Initialize list nudHRV
        private void InitNudHRVs() => _nudHRVs = new List<NumericUpDown>
            {
                nudHRV1,
                nudHRV2,
                nudHRV3,
                nudHRV4,
                nudHRV5,
                nudHRV6
            };

        private List<NumericUpDown> _nudDs;
        // Initialize list nudD
        private void InitNudDs() => _nudDs = new List<NumericUpDown>
            {
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                nudDRH1,
                nudDRH2,
                nudDRH3,
                nudDRH4,
                nudDRH5,
                nudDRH6,
                nudDRV1,
                nudDRV2,
                nudDRV3,
                nudDRV4,
                nudDRV5,
                nudDRV6
            };

        private List<NumericUpDown> _nudDRs;
        // Initialize list nudDR
        private void InitNudDRs() => _nudDRs = new List<NumericUpDown>
            {
                nudDRH1,
                nudDRH2,
                nudDRH3,
                nudDRH4,
                nudDRH5,
                nudDRH6,
                nudDRV1,
                nudDRV2,
                nudDRV3,
                nudDRV4,
                nudDRV5,
                nudDRV6
            };

        private List<NumericUpDown> _nudDRHs;
        // Initialize list nudDRH
        private void InitNudDRHs() => _nudDRHs = new List<NumericUpDown>
            {
                nudDRH1,
                nudDRH2,
                nudDRH3,
                nudDRH4,
                nudDRH5,
                nudDRH6
            };

        private List<NumericUpDown> _nudDRVs;
        // Initialize list nudDRV
        private void InitNudDRVs() => _nudDRVs = new List<NumericUpDown>
            {
                nudDRV1,
                nudDRV2,
                nudDRV3,
                nudDRV4,
                nudDRV5,
                nudDRV6
            };

        private List<NumericUpDown> _nudTits;
        // Initialize list nudTit
        private void InitNudTits() => _nudTits = new List<NumericUpDown>
            {
                nudWSH1,
                nudWSH2,
                nudWSH3,
                nudWSH4,
                nudWSH5,
                nudWSH6,
                nudWSH7,
                nudWSH8,
                nudWSH9,
                nudWSH10,
                nudWSV1,
                nudWSV2,
                nudWSV3,
                nudWSV4,
                nudWSV5,
                nudWSV6,
                nudWSV7,
                nudWSV8,
                nudWSV9,
                nudWSV10,
                nudWRH1,
                nudWRH2,
                nudWRH3,
                nudWRH4,
                nudWRH5,
                nudWRH6,
                nudWRV1,
                nudWRV2,
                nudWRV3,
                nudWRV4,
                nudWRV5,
                nudWRV6,
                nudHSH1,
                nudHSH2,
                nudHSH3,
                nudHSH4,
                nudHSH5,
                nudHSH6,
                nudHSH7,
                nudHSH8,
                nudHSH9,
                nudHSH10,
                nudHSV1,
                nudHSV2,
                nudHSV3,
                nudHSV4,
                nudHSV5,
                nudHSV6,
                nudHSV7,
                nudHSV8,
                nudHSV9,
                nudHSV10,
                nudHRH1,
                nudHRH2,
                nudHRH3,
                nudHRH4,
                nudHRH5,
                nudHRH6,
                nudHRV1,
                nudHRV2,
                nudHRV3,
                nudHRV4,
                nudHRV5,
                nudHRV6,
                nudDRH1,
                nudDRH2,
                nudDRH3,
                nudDRH4,
                nudDRH5,
                nudDRH6,
                nudDRV1,
                nudDRV2,
                nudDRV3,
                nudDRV4,
                nudDRV5,
                nudDRV6
            };

        private List<NumericUpDown> _nudGs;
        // Initialize list nudG
        private void InitNudGs() => _nudGs = new List<NumericUpDown>
            {
                nudWSH1,
                nudWSH2,
                nudWSH3,
                nudWSH4,
                nudWSH5,
                nudWSH6,
                nudWSH7,
                nudWSH8,
                nudWSH9,
                nudWSH10,
                nudWSV1,
                nudWSV2,
                nudWSV3,
                nudWSV4,
                nudWSV5,
                nudWSV6,
                nudWSV7,
                nudWSV8,
                nudWSV9,
                nudWSV10,
                nudWRH1,
                nudWRH2,
                nudWRH3,
                nudWRH4,
                nudWRH5,
                nudWRH6,
                nudWRV1,
                nudWRV2,
                nudWRV3,
                nudWRV4,
                nudWRV5,
                nudWRV6,
                nudHSH1,
                nudHSH2,
                nudHSH3,
                nudHSH4,
                nudHSH5,
                nudHSH6,
                nudHSH7,
                nudHSH8,
                nudHSH9,
                nudHSH10,
                nudHSV1,
                nudHSV2,
                nudHSV3,
                nudHSV4,
                nudHSV5,
                nudHSV6,
                nudHSV7,
                nudHSV8,
                nudHSV9,
                nudHSV10,
                nudHRH1,
                nudHRH2,
                nudHRH3,
                nudHRH4,
                nudHRH5,
                nudHRH6,
                nudHRV1,
                nudHRV2,
                nudHRV3,
                nudHRV4,
                nudHRV5,
                nudHRV6
            };
        #endregion
    }
}
