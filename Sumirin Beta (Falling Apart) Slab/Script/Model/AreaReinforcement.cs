using System.Collections.Generic;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Settings;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Common;
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
        private readonly SumirinBranch _branch = Touhoku;
        private readonly double _lMaxRawWood = Default.Max_Raw_Wood_Nml;
        private readonly int _lFixn;
        #endregion

        #region Constructors
        public AreaReinforcement(SumirinBranch branch, double lMaxRawWood) : base(branch, lMaxRawWood)
        {
            _branch = branch;
            _lMaxRawWood = lMaxRawWood;
            _lFixn = Fixation();
        }
        #endregion

        #region Properties
        public int D { get; set; } = 10;
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
        protected new void CalcRebarBdngR()
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
        protected new void CalcRebarBdngLR()
        {
            if (W <= 910)
            {
                Rebars = new List<string>
                {
                    $"{_lBdng}×{(W + 2 * D).Round10()}×{_lBdng}"
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
        protected new void CalcRebarSt()
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
        protected new void CalcAmt() => Amount = (int)Ceiling(H / Default.Pitch);
        #endregion
    }
}
