using System.Collections.Generic;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Settings;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant.SumirinBranch;
using static System.Math;

namespace Sumirin_Beta__Falling_Apart__Slab.Script.Model
{
    public class AreaReinforcement : AreaSlab
    {
        #region Fields
        private readonly int _fixnD10 = Default.Fixn_D10;
        private readonly int _fixnD13 = Default.Fixn_D13;
        private readonly int _fixnD16 = Default.Fixn_D16;
        private readonly int _fixnD19 = Default.Fixn_D19;
        private readonly int _fixnD22 = Default.Fixn_D22;
        private readonly double _lMaxRawWood = Default.Max_Raw_Wood_Nml;
        private readonly SumirinBranch _branch = Touhoku;
        private int _lFixn;
        private int _lFixnL;
        private int _lFixnR;
        private int _lBdngL;
        private int _lBdngR;
        private int _jt = 0;
        #endregion

        #region Constructors
        public AreaReinforcement(SumirinBranch branch, double lMaxRawWood) : base(branch, lMaxRawWood)
        {
            _branch = branch;
            _lMaxRawWood = lMaxRawWood;
        }
        #endregion

        #region Properties
        public int D { get; set; } = 10;
        public new bool BendingL { get; set; } = false;
        public new bool BendingR { get; set; } = false;
        public bool FixationL { get; set; } = true;
        public bool FixationR { get; set; } = true;
        #endregion

        #region Overridden
        // Fill fields
        protected new void FillFlds()
        {
            BendingHead = GetBdngHead();
            _lFixn = FixnLen();
            _lBdngL = BendingL ? _lBdng : 0;
            _lBdngR = BendingR ? _lBdng : 0;
            _lFixnL = FixationL ? _lFixn : 0;
            _lFixnR = FixationR ? _lFixn : 0;
        }

        // Fixation length
        protected new int FixnLen() => _branch == Touhoku
                ? _rateFixn * D
                : D switch
                {
                    10 => _fixnD10,
                    13 => _fixnD13,
                    16 => _fixnD16,
                    19 => _fixnD19,
                    22 => _fixnD22,
                    _ => (_rateFixn * D).Ceiling50()
                };

        // Rebar calculate bending left
        protected new void CalcRebarBdngL()
        {
            var w = W + _lFixnR;
            if (w <= _lMaxRawWood - _lBdngL)
            {
                MainRebars = new List<(int?, string)>
                {
                    (1, string.Format("{0}×{1,4}", _lBdngL, w.Round10()))
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
        protected new void CalcRebarBdngR()
        {
            var w = W + _lFixnL;
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
        protected new void CalcRebarBdngLR()
        {
            var w = W;
            if (w <= _wMinBdngLR)
            {
                MainRebars = new List<(int?, string)>
                {
                    (2, $"{_lBdngL}×{(w + 2 * D).Round10()}×{_lBdngR}")
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
        protected new void CalcRebarSt()
        {
            var w = W + _lFixnL + _lFixnR;
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
        protected new void CalcAmt()
        {
            var amt = (int)Ceiling(H / Default.Pitch);
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
        }
        #endregion
    }
}
