using System.Collections.Generic;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Settings;
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
            if (W > Default.W_Min_Sgl)
            {
                var jt = 1;
                var w = W;
                var lBndgL = BendingL ? Default.L_Bdng : 0;
                var lBndgR = BendingR ? Default.L_Bdng : 0;
                var lRebarL = _lMaxRawWood - lBndgL;
                var lRebarR = _lMaxRawWood - lBndgR - Default.Chidori_Horz;
                while (lRebarL - lRebarR < Default.Chidori_Horz)
                {
                    lRebarR -= 500;
                    lRebarR = lRebarR.Round500();
                }
                while (w > lRebarL + lRebarR)
                {
                    w -= _lMaxRawWood;
                    jt++;
                }
                w += jt * Default.D_Slab * Default.Rate_Fixn + 2 * Default.L_Bdng;
                var wHalf = w / 2;
                var horzChidoriHalf = Default.Chidori_Horz / 2;
                Rebars = new List<string>
                {
                    string.Format("{0}×{1,4}", Default.L_Bdng, (wHalf + horzChidoriHalf).Round500() - Default.L_Bdng)
                };
                for (var i = 1; i < jt; i++)
                {
                    Rebars.Add(_lMaxRawWood.ToString());
                }
                Rebars.Add(string.Format("{0}×{1,4}", Default.L_Bdng, (wHalf - horzChidoriHalf).Round500() - Default.L_Bdng));
            }
            else
            {
                var w = (W + 2 * Default.D_Slab).Round10();
                Rebars = new List<string>
                {
                    $"{Default.L_Bdng}×{w}×{Default.L_Bdng}"
                };
            }
        }

        private void CalcRebarBdngLTouhoku()
        {
            var jt = 0;
            var w = W;
            var lMaxRebarL = _lMaxRawWood - Default.L_Bdng;
            for (var i = 0; ; i++)
            {
                if (w < lMaxRebarL + i * _lMaxRawWood)
                {
                    jt = i;
                    break;
                }
            }
            var rawWood = jt - 1;
            if (jt == 0)
            {

            }
            else if(jt == 1)
            {

            }
            else
            {

            }
        }

        // Rebar calculate Ibaraki
        private void CalcRebarIbaraki()
        {
            if (W > Default.W_Min_Sgl)
            {
                var jt = 1;
                var w = W;
                var lBndgL = BendingL ? Default.L_Bdng : 0;
                var lBndgR = BendingR ? Default.L_Bdng : 0;
                var lRebarL = _lMaxRawWood - lBndgL;
                var lRebarR = _lMaxRawWood - lBndgR - Default.Chidori_Horz;
                while (lRebarL - lRebarR < Default.Chidori_Horz)
                {
                    lRebarR -= 500;
                    lRebarR = lRebarR.Round500();
                }
                while (w > lRebarL + lRebarR)
                {
                    w -= Default.Max_Raw_Wood_Nml;
                    jt++;
                }
                w += jt * Default.Fixn_D13 + 2 * Default.L_Bdng;
                var wHalf = w / 2;
                var horzChidoriHalf = Default.Chidori_Horz / 2;
                Rebars = new List<string>
                {
                    string.Format("{0}×{1,4}", Default.L_Bdng, (wHalf + horzChidoriHalf).Round500() - Default.L_Bdng)
                };
                for (var i = 1; i < jt; i++)
                {
                    Rebars.Add(Default.Max_Raw_Wood_Nml.ToString());
                }
                Rebars.Add(string.Format("{0}×{1,4}", Default.L_Bdng, (wHalf - horzChidoriHalf).Round500() - Default.L_Bdng));
            }
            else
            {
                var w = (W + 2 * Default.D_Slab).Round10();
                Rebars = new List<string>
                {
                    $"{Default.L_Bdng}×{w}×{Default.L_Bdng}"
                };
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
