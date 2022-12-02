using System.Collections.Generic;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Settings;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant.SumirinBranch;
using static System.Math;

namespace Sumirin_Beta__Falling_Apart__Slab.Script.Model
{
    public class AreaSlab
    {
        #region Fields
        protected const int _wMinBdngLR = 910;
        protected readonly int _lBdng = Default.L_Bdng;
        protected readonly int _chidoriHorz = Default.Chidori_Horz;
        protected readonly int _rateFixn = Default.Rate_Fixn;
        private readonly double _lMaxRawWood = Default.Max_Raw_Wood_Nml;
        private readonly int _dSlab = Default.D_Slab;
        private readonly int _fixnSlab = Default.Fixn_D13;
        private readonly SumirinBranch _branch = Touhoku;
        private int _lFixn;
        private int _lBdngL;
        private int _lBdngR;
        private int _bdngHead;
        private int _jt = 0;
        #endregion

        #region Constructors
        public AreaSlab(SumirinBranch branch, double lMaxRawWood)
        {
            _branch = branch;
            _lMaxRawWood = lMaxRawWood;
        }
        #endregion

        #region Properties
        public bool IsLongest { get; set; } = false;
        public double W { get; set; } = 910;
        public double H { get; set; } = 910;
        public bool BendingL { get; set; } = true;
        public bool BendingR { get; set; } = true;
        public List<string> MainRebars { get; protected set; }
        public List<string> SubRebars { get; protected set; }
        public int MainAmt { get; protected set; }
        public int? SubAmt { get; protected set; }
        #endregion

        #region Methods
        /// <summary>
        /// Process.
        /// </summary>
        public void Prcs()
        {
            FillFlds();
            if (BendingL)
            {
                if (BendingR)
                {
                    CalcRebarBdngLR();
                }
                else
                {
                    CalcRebarBdngL();
                }
            }
            else
            {
                if (BendingR)
                {
                    CalcRebarBdngR();
                }
                else
                {
                    CalcRebarSt();
                }
            }
            CalcAmt();
        }

        // Fill fields
        protected void FillFlds()
        {
            _lFixn = FixnLen();
            _bdngHead = BendingL ? BendingR ? 2 : 1 : BendingR ? 1 : 0;
            _lBdngL = BendingL ? _lBdng : 0;
            _lBdngR = BendingR ? _lBdng : 0;
        }

        // Fixation length
        protected int FixnLen() => _branch == Touhoku ? _rateFixn * _dSlab : _fixnSlab;

        // Rebar calculate bending left
        protected void CalcRebarBdngL()
        {
            var w = W;
            if (w <= _lMaxRawWood - _lBdngL)
            {
                MainRebars = new List<string>
                {
                    string.Format("{0}×{1,4}", _lBdngL, w.Round10())
                };
            }
            else
            {
                // main
                _jt = PrcsHdrMainRebar(w, out var lRddRebarLMain, out var lRddRebarRMain);
                MainRebars = new List<string>
                {
                    string.Format("{0}×{1,4}", _lBdngL, lRddRebarLMain)
                };
                for (var i = 1; i < _jt; i++)
                {
                    MainRebars.Add(_lMaxRawWood.ToString());
                }
                MainRebars.Add(string.Format("{0,4}", lRddRebarRMain));
                // sub
                _jt = PrcsHdrSubRebar(w, out var lRddRebarLSub, out var lRddRebarRSub);
                SubRebars = new List<string>
                {
                    string.Format("{0}×{1,4}", _lBdngL, lRddRebarLSub)
                };
                for (var i = 1; i < _jt; i++)
                {
                    SubRebars.Add(_lMaxRawWood.ToString());
                }
                SubRebars.Add(string.Format("{0,4}", lRddRebarRSub));
            }
        }

        // Rebar calculate bending right
        protected void CalcRebarBdngR()
        {
            var w = W;
            if (w <= _lMaxRawWood - _lBdngR)
            {
                MainRebars = new List<string>
                {
                    string.Format("{0}×{1,4}", _lBdngR, w.Round10())
                };
            }
            else
            {
                // main
                _jt = PrcsHdrMainRebar(w, out var lRddRebarL, out var lRddRebarR);
                MainRebars = new List<string>
                {
                    string.Format("{0,4}", lRddRebarL)
                };
                for (var i = 1; i < _jt; i++)
                {
                    MainRebars.Add(_lMaxRawWood.ToString());
                }
                MainRebars.Add(string.Format("{0}×{1,4}", _lBdngR, lRddRebarR));
                // sub
                _jt = PrcsHdrSubRebar(w, out lRddRebarL, out lRddRebarR);
                SubRebars = new List<string>
                {
                    string.Format("{0}×{1,4}", _lBdngR, lRddRebarL)
                };
                for (var i = 1; i < _jt; i++)
                {
                    SubRebars.Add(_lMaxRawWood.ToString());
                }
                SubRebars.Add(string.Format("{0,4}", lRddRebarR));
            }
        }

        // Rebar calculate bending couple head
        protected void CalcRebarBdngLR()
        {
            var w = W;
            if (w <= _wMinBdngLR)
            {
                MainRebars = new List<string>
                {
                    $"{_lBdngL}×{(w + 2 * _dSlab).Round10()}×{_lBdngR}"
                };
            }
            else
            {
                _jt = PrcsHdrMainRebar(w, out var lRddRebarL, out var lRddRebarR);
                MainRebars = new List<string>
                {
                    string.Format("{0}×{1,4}", _lBdngL, lRddRebarL)
                };
                for (var i = 1; i < _jt; i++)
                {
                    MainRebars.Add(_lMaxRawWood.ToString());
                }
                MainRebars.Add(string.Format("{0}×{1,4}", _lBdngR, lRddRebarR));
            }
        }

        // Rebar calculate straight
        protected void CalcRebarSt()
        {
            var w = W;
            if (w <= _lMaxRawWood)
            {
                MainRebars = new List<string>
                {
                    string.Format("{0,4}", w.Round10())
                };
            }
            else
            {
                _jt = PrcsHdrMainRebar(w, out var lRddRebarL, out var lRddRebarR);
                MainRebars = new List<string>
                {
                    string.Format("{0,4}", lRddRebarL)
                };
                for (var i = 1; i < _jt; i++)
                {
                    MainRebars.Add(_lMaxRawWood.ToString());
                }
                MainRebars.Add(string.Format("{0,4}", lRddRebarR));
            }
        }

        // Amount calculate
        protected void CalcAmt()
        {
            var amt = (int)Ceiling(H / Default.Pitch);
            // split
            if (_bdngHead == 1 && _jt > 0)
            {
                MainAmt = (int)Ceiling(amt / 2d);
                var subAmt = amt - MainAmt;
                SubAmt = subAmt > 0 ? subAmt : 1;
            }
            else
            {
                MainAmt = amt;
                SubAmt = null;
            }
            // over
            if (IsLongest)
            {
                MainAmt++;
            }
        }

        // Joint count
        protected int JtCnt(ref double w)
        {
            var jt = 1;
            var lMaxRawWoodRip = _lMaxRawWood - _lFixn;
            var body = 2 * _lMaxRawWood - _lBdngL - _lBdngR - _chidoriHorz - _lFixn;
            while (w > body)
            {
                w -= lMaxRawWoodRip;
                jt++;
            }
            w = (w + jt * _lFixn + _bdngHead * _lBdng).Round500();
            return jt;
        }

        // Process header main rebar
        protected int PrcsHdrMainRebar(double w, out int lRddRebarL, out int lRddRebarR)
        {
            var jt = JtCnt(ref w);
            lRddRebarL = ((w + _chidoriHorz) / 2).Round500();
            lRddRebarR = ((w - _chidoriHorz) / 2).Round500();
            while (lRddRebarL + lRddRebarR > w)
            {
                lRddRebarR -= 500;
            }
            PrcsBdngHdrRebar(ref lRddRebarL, ref lRddRebarR);
            return jt;
        }

        // Process header sub rebar
        protected int PrcsHdrSubRebar(double w, out int lRddRebarL, out int lRddRebarR)
        {
            var jt = JtCnt(ref w);
            lRddRebarL = ((w - _chidoriHorz) / 2).Round500();
            lRddRebarR = ((w + _chidoriHorz) / 2).Round500();
            while (lRddRebarL + lRddRebarR > w)
            {
                lRddRebarL -= 500;
            }
            PrcsBdngHdrRebar(ref lRddRebarL, ref lRddRebarR);
            return jt;
        }

        // Process bending header rebar
        protected void PrcsBdngHdrRebar(ref int lRddRebarL, ref int lRddRebarR)
        {
            lRddRebarL -= _lBdngL;
            lRddRebarR -= _lBdngR;
        }
        #endregion
    }
}
