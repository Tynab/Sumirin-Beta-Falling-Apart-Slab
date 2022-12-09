using System.Collections.Generic;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Settings;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant.SumirinBranch;
using static System.Math;

namespace Sumirin_Beta__Falling_Apart__Slab.Script.Model
{
    public class AreaSlab : Area
    {
        #region Fields
        private readonly double _lMaxRawWood = Default.Max_Raw_Wood_Nml;
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
        public bool IsLongest { get; set; } = false;
        #endregion

        #region Overridden
        protected override void FillFlds()
        {
            BendingHead = BendingL ? BendingR ? 2 : 1 : BendingR ? 1 : 0;
            _lFixn = FixnLen();
            _lBdngL = BendingL ? L_BDNG : 0;
            _lBdngR = BendingR ? L_BDNG : 0;
        }

        protected override int FixnLen() => _branch == Touhoku ? RATE_FIXN * D_SLAB : FIXN_SLAB;

        protected override void CalcRebarBdngL()
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
                _jt = PrcsHdrMainRebar(w, _lMaxRawWood, _lFixn, _lBdngL, _lBdngR, out var lRddRebarL, out var lRddRebarR);
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

        protected override void CalcRebarBdngR()
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
                _jt = PrcsHdrMainRebar(w, _lMaxRawWood, _lFixn, _lBdngL, _lBdngR, out var lRddRebarL, out var lRddRebarR);
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

        protected override void CalcRebarBdngLR()
        {
            var w = W;
            if (w <= W_MIN_BDNG_LR)
            {
                MainRebars = new List<(int?, string)>
                {
                    (2, $"{_lBdngL}×{(w + 2 * D_SLAB).Round10()}×{_lBdngR}")
                };
            }
            else
            {
                _jt = PrcsHdrMainRebar(w, _lMaxRawWood, _lFixn, _lBdngL, _lBdngR, out var lRddRebarL, out var lRddRebarR);
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

        protected override void CalcRebarSt()
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
                _jt = PrcsHdrMainRebar(w, _lMaxRawWood, _lFixn, _lBdngL, _lBdngR, out var lRddRebarL, out var lRddRebarR);
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

        protected override void CalcAmt()
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
        #endregion
    }
}
