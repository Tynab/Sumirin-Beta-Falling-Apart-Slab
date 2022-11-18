using System.Collections.Generic;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Settings;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Common;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant.SumirinBranch;
using static System.Math;

namespace Sumirin_Beta__Falling_Apart__Slab.Script.Model
{
    public class Area
    {
        #region Fields
        private readonly SumirinBranch _branch;
        private readonly double _lMaxRawWood;
        private readonly int _lBdng = Default.L_Bdng;
        private readonly int _chidoriHorz = Default.Chidori_Horz;
        private readonly int _rateFixn = Default.Rate_Fixn;
        private readonly int _dSlab = Default.D_Slab;
        private readonly int _fixnSlab = Default.Fixn_D13;
        #endregion

        #region Constructors
        public Area(SumirinBranch branch, double lMaxRawWood)
        {
            _branch = branch;
            _lMaxRawWood = lMaxRawWood;
        }
        #endregion

        #region Properties
        public bool IsLongest { get; set; } = false;
        public double W { get; set; } = 910;
        public double H { get; set; } = 910;
        public bool BendingL { get; set; }
        public bool BendingR { get; set; }
        public List<string> Rebars { get; private set; }
        public int Amount { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Process.
        /// </summary>
        public void Prcs()
        {
            if (_branch == Touhoku)
            {
                CalcRebarTouhoku();
            }
            else
            {
                CalcRebarIbaraki();
            }
            CalcAmt();
        }

        // Rebar calculate Touhoku
        private void CalcRebarTouhoku()
        {
            if (BendingL)
            {
                if (BendingR)
                {
                    CalcRebarBdngLRTouhoku();
                }
                else
                {
                    CalcRebarBdngLTouhoku();
                }
            }
            else
            {
                if (BendingR)
                {
                    CalcRebarBdngRTouhoku();
                }
                else
                {
                    CalcRebarStTouhoku();
                }
            }
        }

        // Rebar calculate Touhoku bending left
        private void CalcRebarBdngLTouhoku()
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
                var lFixn = _rateFixn * _dSlab;
                var jt = JtCnt(ref w, _lMaxRawWood + lMaxRebarR - lFixn, _lMaxRawWood - lFixn);
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

        // Rebar calculate Touhoku bending right
        private void CalcRebarBdngRTouhoku()
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
                var lFixn = _rateFixn * _dSlab;
                var jt = JtCnt(ref w, _lMaxRawWood + lMaxRebarR - lFixn, _lMaxRawWood - lFixn);
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

        // Rebar calculate Touhoku bending couple head
        private void CalcRebarBdngLRTouhoku()
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
                var lFixn = _rateFixn * _dSlab;
                var jt = JtCnt(ref w, (_lMaxRawWood - _lBdng) * 2 - _chidoriHorz - lFixn, _lMaxRawWood - lFixn);
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

        // Rebar calculate Touhoku straight
        private void CalcRebarStTouhoku()
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
                var lFixn = _rateFixn * _dSlab;
                var jt = JtCnt(ref w, _lMaxRawWood * 2 - _chidoriHorz - lFixn, _lMaxRawWood - lFixn);
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

        // Rebar calculate Ibaraki
        private void CalcRebarIbaraki()
        {
            if (BendingL)
            {
                if (BendingR)
                {
                    CalcRebarBdngLRIbaraki();
                }
                else
                {
                    CalcRebarBdngLIbaraki();
                }
            }
            else
            {
                if (BendingR)
                {
                    CalcRebarBdngRIbaraki();
                }
                else
                {
                    CalcRebarStIbaraki();
                }
            }
        }

        // Rebar calculate Ibaraki bending left
        private void CalcRebarBdngLIbaraki()
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
                var jt = JtCnt(ref w, _lMaxRawWood + lMaxRebarR - _fixnSlab, _lMaxRawWood - _fixnSlab);
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

        // Rebar calculate Ibaraki bending right
        private void CalcRebarBdngRIbaraki()
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
                var jt = JtCnt(ref w, _lMaxRawWood + lMaxRebarR - _fixnSlab, _lMaxRawWood - _fixnSlab);
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

        // Rebar calculate Ibaraki bending couple head
        private void CalcRebarBdngLRIbaraki()
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
                var jt = JtCnt(ref w, (_lMaxRawWood - _lBdng) * 2 - _chidoriHorz - _fixnSlab, _lMaxRawWood - _fixnSlab);
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

        // Rebar calculate Ibaraki straight
        private void CalcRebarStIbaraki()
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
                var jt = JtCnt(ref w, _lMaxRawWood * 2 - _chidoriHorz - _fixnSlab, _lMaxRawWood - _fixnSlab);
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
        private void CalcAmt()
        {
            var amt = (int)Ceiling(H / Default.Pitch);
            Amount = IsLongest ? amt + 1 : amt;
        }
        #endregion
    }
}
