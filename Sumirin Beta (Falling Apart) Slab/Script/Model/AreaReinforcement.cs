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
        private readonly int _lFixn;
        private readonly int _lBdngL;
        private readonly int _lBdngR;
        private readonly int _bdngHead;
        private int _jt = 0;
        #endregion

        #region Constructors
        public AreaReinforcement(SumirinBranch branch, double lMaxRawWood) : base(branch, lMaxRawWood)
        {
            _branch = branch;
            _lMaxRawWood = lMaxRawWood;
            _bdngHead = BendingL ? BendingR ? 2 : 1 : BendingR ? 1 : 0;
            _lFixn = Fixation();
            _lBdngL = BendingL ? _lBdng : 0;
            _lBdngR = BendingR ? _lBdng : 0;
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
        // Fixation
        protected new int Fixation() => _branch == Touhoku
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
            var fixnR = FixationR ? _lFixn : 0;
            var w = W + fixnR;
            if (w <= _lMaxRawWood - _lBdngL)
            {
                MainRebars = new List<string>
                {
                    string.Format("{0}×{1,4}", _lBdngL, W.Round10())
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
                MainRebars.Add(string.Format("{0,4}", lRddRebarR));
                // sub
                _jt = PrcsHdrSubRebar(w, out lRddRebarL, out lRddRebarR);
                SubRebars = new List<string>
                {
                    string.Format("{0}×{1,4}", _lBdngL, lRddRebarL)
                };
                for (var i = 1; i < _jt; i++)
                {
                    SubRebars.Add(_lMaxRawWood.ToString());
                }
                SubRebars.Add(string.Format("{0,4}", lRddRebarR));
            }
        }

        // Rebar calculate bending right
        protected new void CalcRebarBdngR()
        {
            var fixnL = FixationL ? _lFixn : 0;
            var w = W + fixnL;
            if (w <= _lMaxRawWood - _lBdngR)
            {
                MainRebars = new List<string>
                {
                    string.Format("{0}×{1,4}", _lBdngR, W.Round10())
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
        protected new void CalcRebarBdngLR()
        {
            var w = W;
            if (w <= _wMinBdngLR)
            {
                MainRebars = new List<string>
                {
                    $"{_lBdngL}×{(W + 2 * D).Round10()}×{_lBdngR}"
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
        protected new void CalcRebarSt()
        {
            var fixnL = FixationL ? Fixation() : 0;
            var fixnR = FixationR ? Fixation() : 0;
            var w = W + fixnL + fixnR;
            if (w <= _lMaxRawWood)
            {
                MainRebars = new List<string>
                {
                    string.Format("{0,4}", W)
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
        protected new void CalcAmt()
        {
            var amt = (int)Ceiling(H / Default.Pitch);
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
        }
        #endregion
    }
}
