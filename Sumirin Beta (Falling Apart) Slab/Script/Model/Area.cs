using System.Collections.Generic;
using static Sumirin_Beta__Falling_Apart__Slab.Properties.Settings;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant.AreaBranch;
using static System.Math;

namespace Sumirin_Beta__Falling_Apart__Slab.Script.Model
{
    internal class Area
    {
        #region Fields
        private readonly double _rawWood;
        #endregion

        #region Constructors
        internal Area(double rawWood) => _rawWood = rawWood;
        #endregion

        #region Properties
        internal bool IsLg { get; set; } = false;
        internal AreaBranch Branch { get; set; } = Touhoku;
        internal double W { get; set; } = 910;
        internal double H { get; set; } = 910;
        internal List<string> Rebar { get; private set; }
        internal double Amount { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Process.
        /// </summary>
        internal void Prcs()
        {
            if (Branch == Touhoku)
            {
                CalcRebarTouhokuPart();
            }
            else
            {
                CalcRebarIbarakiPart();
            }
            CalcAmt();
        }

        // Rebar calculate Touhoku part
        private void CalcRebarTouhokuPart()
        {
            if (W > Default.W_Min_Sgl)
            {
                var jt = 1;
                var w = W;
                while (w > 2 * (_rawWood - Default.L_Bdng) - Default.Chidori_Horz)
                {
                    w -= _rawWood;
                    jt++;
                }
                w += jt * Default.D_Slab * Default.Rate_Fixn + 2 * Default.L_Bdng;
                var wHalf = w / 2;
                var horzChidoriHalf = Default.Chidori_Horz / 2;
                Rebar = new List<string>
                {
                    string.Format("{0}×{1,4}", Default.L_Bdng, (wHalf + horzChidoriHalf).Round500() - Default.L_Bdng)
                };
                for (var i = 1; i < jt; i++)
                {
                    Rebar.Add(_rawWood.ToString());
                }
                Rebar.Add(string.Format("{0}×{1,4}", Default.L_Bdng, (wHalf - horzChidoriHalf).Round500() - Default.L_Bdng));
            }
            else
            {
                var w = (W + 2 * Default.D_Slab).Round10();
                Rebar = new List<string>
                {
                    $"{Default.L_Bdng}×{w}×{Default.L_Bdng}"
                };
            }
        }

        // Rebar calculate Ibaraki part
        private void CalcRebarIbarakiPart()
        {
            if (W > Default.W_Min_Sgl)
            {
                var jt = 1;
                var w = W;
                while (w > 2 * (Default.Max_Raw_Wood_Nml - Default.L_Bdng) - Default.Chidori_Horz)
                {
                    w -= Default.Max_Raw_Wood_Nml;
                    jt++;
                }
                w += jt * Default.Fixn_D13 + 2 * Default.L_Bdng;
                var wHalf = w / 2;
                var horzChidoriHalf = Default.Chidori_Horz / 2;
                Rebar = new List<string>
                {
                    string.Format("{0}×{1,4}", Default.L_Bdng, (wHalf + horzChidoriHalf).Round500() - Default.L_Bdng)
                };
                for (var i = 1; i < jt; i++)
                {
                    Rebar.Add(Default.Max_Raw_Wood_Nml.ToString());
                }
                Rebar.Add(string.Format("{0}×{1,4}", Default.L_Bdng, (wHalf - horzChidoriHalf).Round500() - Default.L_Bdng));
            }
            else
            {
                var w = (W + 2 * Default.D_Slab).Round10();
                Rebar = new List<string>
                {
                    $"{Default.L_Bdng}×{w}×{Default.L_Bdng}"
                };
            }
        }

        // Amount calculate
        private void CalcAmt()
        {
            var amt = Ceiling(H / Default.Pitch);
            Amount = IsLg ? amt + 1 : amt;
        }
        #endregion
    }
}
