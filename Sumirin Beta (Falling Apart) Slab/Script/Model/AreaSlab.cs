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
        public int Area { get; set; }
        public double W { get; set; } = 910;
        public double H { get; set; } = 910;
        public bool BendingL { get; set; } = true;
        public bool BendingR { get; set; } = true;
        public bool IsLongest { get; set; } = false;
        public List<(int?, string)> MainRebars { get; set; }
        public List<(int?, string)> SubRebars { get; set; }
        public int MainAmount { get; set; }
        public int? SubAmount { get; set; }
        public int BendingHead { get; protected set; }
        #endregion

        #region Methods
        /// <summary>
        /// Get bending head.
        /// </summary>
        /// <returns>Bending head count.</returns>
        public int GetBdngHead() => BendingL ? BendingR ? 2 : 1 : BendingR ? 1 : 0;

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
            BendingHead = GetBdngHead();
            _lFixn = FixnLen();
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
                MainRebars = new List<(int?, string)>
                {
                    (1, string.Format("{0}×{1,4}", _lBdngL, w.Round10()))
                };
            }
            else
            {
                // main
                _jt = PrcsHdrMainRebar(w, out var lRddRebarL, out var lRddRebarR);
                MainRebars = new List<(int?, string)>
                {
                    (1, string.Format("{0}×{1,4}", _lBdngL, lRddRebarL))
                };
                for (var i = 1; i < _jt; i++)
                {
                    MainRebars.Add((0, _lMaxRawWood.ToString()));
                }
                MainRebars.Add((0, string.Format("{0,4}", lRddRebarR)));
                // sub
                SubRebars = new List<(int?, string)>
                {
                    (1, string.Format("{0}×{1,4}", _lBdngL, lRddRebarR - _lBdngL))
                };
                for (var i = 1; i < _jt; i++)
                {
                    SubRebars.Add((0, _lMaxRawWood.ToString()));
                }
                SubRebars.Add((0, string.Format("{0,4}", lRddRebarL + _lBdngL)));
            }
        }

        // Rebar calculate bending right
        protected void CalcRebarBdngR()
        {
            var w = W;
            if (w <= _lMaxRawWood - _lBdngR)
            {
                MainRebars = new List<(int?, string)>
                {
                    (1, string.Format("{0}×{1,4}", _lBdngR, w.Round10()))
                };
            }
            else
            {
                // main
                _jt = PrcsHdrMainRebar(w, out var lRddRebarL, out var lRddRebarR);
                MainRebars = new List<(int?, string)>
                {
                    (0, string.Format("{0,4}", lRddRebarL))
                };
                for (var i = 1; i < _jt; i++)
                {
                    MainRebars.Add((0, _lMaxRawWood.ToString()));
                }
                MainRebars.Add((1, string.Format("{0}×{1,4}", _lBdngR, lRddRebarR)));
                // sub
                SubRebars = new List<(int?, string)>
                {
                    (0, string.Format("{0,4}", lRddRebarR + _lBdngR))
                };
                for (var i = 1; i < _jt; i++)
                {
                    SubRebars.Add((0, _lMaxRawWood.ToString()));
                }
                SubRebars.Add((1, string.Format("{0}×{1,4}", _lBdngR, lRddRebarL - _lBdngR)));
            }
        }

        // Rebar calculate bending couple head
        protected void CalcRebarBdngLR()
        {
            var w = W;
            if (w <= _wMinBdngLR)
            {
                MainRebars = new List<(int?, string)>
                {
                    (2, $"{_lBdngL}×{(w + 2 * _dSlab).Round10()}×{_lBdngR}")
                };
            }
            else
            {
                _jt = PrcsHdrMainRebar(w, out var lRddRebarL, out var lRddRebarR);
                MainRebars = new List<(int?, string)>
                {
                    (1, string.Format("{0}×{1,4}", _lBdngL, lRddRebarL))
                };
                for (var i = 1; i < _jt; i++)
                {
                    MainRebars.Add((0, _lMaxRawWood.ToString()));
                }
                MainRebars.Add((1, string.Format("{0}×{1,4}", _lBdngR, lRddRebarR)));
            }
        }

        // Rebar calculate straight
        protected void CalcRebarSt()
        {
            var w = W;
            if (w <= _lMaxRawWood)
            {
                MainRebars = new List<(int?, string)>
                {
                    (0, string.Format("{0,4}", w.Round10()))
                };
            }
            else
            {
                _jt = PrcsHdrMainRebar(w, out var lRddRebarL, out var lRddRebarR);
                MainRebars = new List<(int?, string)>
                {
                    (0, string.Format("{0,4}", lRddRebarL))
                };
                for (var i = 1; i < _jt; i++)
                {
                    MainRebars.Add((0, _lMaxRawWood.ToString()));
                }
                MainRebars.Add((0, string.Format("{0,4}", lRddRebarR)));
            }
        }

        // Amount calculate
        protected void CalcAmt()
        {
            var amt = (int)Ceiling(H / Default.Pitch);
            // split
            if (BendingHead == 1 && _jt > 0)
            {
                MainAmount = (int)Ceiling(amt / 2d);
                var subAmt = amt - MainAmount;
                SubAmount = subAmt > 0 ? subAmt : 1;
            }
            else
            {
                MainAmount = amt;
                SubAmount = null;
            }
            // over
            if (IsLongest)
            {
                MainAmount++;
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
            w = (w + jt * _lFixn + BendingHead * _lBdng).Round500();
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

        // Process bending header rebar
        protected void PrcsBdngHdrRebar(ref int lRddRebarL, ref int lRddRebarR)
        {
            lRddRebarL -= _lBdngL;
            lRddRebarR -= _lBdngR;
        }
        #endregion
    }
}
