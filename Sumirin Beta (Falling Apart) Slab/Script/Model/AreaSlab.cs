using System.Collections.Generic;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Settings;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Common;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant.SumirinBranch;
using static System.Math;

namespace Sumirin_Beta__Falling_Apart__Slab.Script.Model
{
    public class AreaSlab
    {
        #region Fields
        protected readonly int _lBdng = Default.L_Bdng;
        protected readonly int _chidoriHorz = Default.Chidori_Horz;
        protected readonly int _rateFixn = Default.Rate_Fixn;
        private readonly int _dSlab = Default.D_Slab;
        private readonly int _fixnSlab = Default.Fixn_D13;
        private readonly SumirinBranch _branch = Touhoku;
        private readonly double _lMaxRawWood = Default.Max_Raw_Wood_Nml;
        private readonly int _lFixn;
        #endregion

        #region Constructors
        public AreaSlab(SumirinBranch branch, double lMaxRawWood)
        {
            _branch = branch;
            _lMaxRawWood = lMaxRawWood;
            _lFixn = Fixation();
        }
        #endregion

        #region Properties
        public bool IsLongest { get; set; } = false;
        public double W { get; set; } = 910;
        public double H { get; set; } = 910;
        public bool BendingL { get; set; }
        public bool BendingR { get; set; }
        public List<string> Rebars { get; protected set; }
        public int Amount { get; protected set; }
        #endregion

        #region Methods
        /// <summary>
        /// Process.
        /// </summary>
        public void Prcs()
        {
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

        // Fixation
        protected int Fixation() => _branch == Touhoku ? _rateFixn * _dSlab : _fixnSlab;

        // Rebar calculate bending left
        protected void CalcRebarBdngL()
        {
            var lMaxRebarLWoBdng = (int)_lMaxRawWood - _lBdng;
            if (W <= lMaxRebarLWoBdng)
            {
                Rebars = new List<string>
                {
                    string.Format("{0}×{1,4}", _lBdng, W)
                };
            }
            else
            {
                var lMaxRebarR = (lMaxRebarLWoBdng - _chidoriHorz).Round500();
                while (lMaxRebarLWoBdng < lMaxRebarR + _chidoriHorz)
                {
                    lMaxRebarR -= 500;
                }
                var w = W;
                var lMaxRawWoodRip = _lMaxRawWood - _lFixn;
                var jt = JtCnt(ref w, lMaxRawWoodRip + lMaxRebarR, lMaxRawWoodRip);
                var lRebarLWoBdng = ((w + _chidoriHorz) / 2).Round500() - _lBdng;
                var lRebarR = ((w - _chidoriHorz) / 2).Round500();
                while (lRebarLWoBdng < lRebarR + _chidoriHorz)
                {
                    lRebarLWoBdng += 500;
                    lRebarR -= 500;
                }
                Rebars = new List<string>
                {
                    string.Format("{0}×{1,4}", _lBdng, lRebarLWoBdng)
                };
                for (var i = 1; i < jt; i++)
                {
                    Rebars.Add(_lMaxRawWood.ToString());
                }
                Rebars.Add(string.Format("{0,4}", lRebarR));
            }
        }

        // Rebar calculate bending right
        protected void CalcRebarBdngR()
        {
            var lMaxRebarRWoBdng = _lMaxRawWood - _lBdng;
            if (W <= lMaxRebarRWoBdng)
            {
                Rebars = new List<string>
                {
                    string.Format("{0}×{1,4}", _lBdng, W)
                };
            }
            else
            {
                var lMaxRebarR = _lMaxRawWood - _chidoriHorz - _lBdng;
                var w = W;
                var lMaxRawWoodRip = _lMaxRawWood - _lFixn;
                var jt = JtCnt(ref w, lMaxRawWoodRip + lMaxRebarR, lMaxRawWoodRip);
                var lRebarL = ((w + _chidoriHorz) / 2).Round500();
                var lRebarRWoBdng = ((w - _chidoriHorz) / 2).Round500() - _lBdng;
                Rebars = new List<string>
                {
                    string.Format("{0,4}", lRebarL)
                };
                for (var i = 1; i < jt; i++)
                {
                    Rebars.Add(_lMaxRawWood.ToString());
                }
                Rebars.Add(string.Format("{0}×{1,4}", _lBdng, lRebarRWoBdng));
            }
        }

        // Rebar calculate bending couple head
        protected void CalcRebarBdngLR()
        {
            if (W <= 910)
            {
                Rebars = new List<string>
                {
                    $"{_lBdng}×{(W + 2 * _dSlab).Round10()}×{_lBdng}"
                };
            }
            else
            {
                var w = W;
                var lMaxRawWoodRip = _lMaxRawWood - _lFixn;
                var jt = JtCnt(ref w, (lMaxRawWoodRip - _lBdng) * 2 - _chidoriHorz + _lFixn, lMaxRawWoodRip);
                var lRebarLWoBdng = ((w + _chidoriHorz) / 2).Round500() - _lBdng;
                var lRebarRWoBdng = ((w - _chidoriHorz) / 2).Round500() - _lBdng;
                Rebars = new List<string>
                {
                    string.Format("{0}×{1,4}", _lBdng, lRebarLWoBdng)
                };
                for (var i = 1; i < jt; i++)
                {
                    Rebars.Add(_lMaxRawWood.ToString());
                }
                Rebars.Add(string.Format("{0}×{1,4}", _lBdng, lRebarRWoBdng));
            }
        }

        // Rebar calculate straight
        protected void CalcRebarSt()
        {
            if (W <= _lMaxRawWood)
            {
                Rebars = new List<string>
                {
                    string.Format("{0,4}", W)
                };
            }
            else
            {
                var w = W;
                var lMaxRawWoodRip = _lMaxRawWood - _lFixn;
                var jt = JtCnt(ref w, _lMaxRawWood + lMaxRawWoodRip - _chidoriHorz, lMaxRawWoodRip);
                var lRebarL = ((w + _chidoriHorz) / 2).Round500();
                var lRebarR = ((w - _chidoriHorz) / 2).Round500();
                Rebars = new List<string>
                {
                    string.Format("{0,4}", lRebarL)
                };
                for (var i = 1; i < jt; i++)
                {
                    Rebars.Add(_lMaxRawWood.ToString());
                }
                Rebars.Add(string.Format("{0,4}", lRebarR));
            }
        }

        // Amount calculate
        protected void CalcAmt()
        {
            var amt = (int)Ceiling(H / Default.Pitch);
            Amount = IsLongest ? amt + 1 : amt;
        }
        #endregion
    }
}
