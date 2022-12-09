using System.Collections.Generic;
using static Sumirin_Beta__Falling_Apart__Slab.Script.Constant;

namespace Sumirin_Beta__Falling_Apart__Slab.Script.Model
{
    public abstract class Area
    {
        #region Properties
        public int Id { get; set; }
        public double W { get; set; } = 910;
        public double H { get; set; } = 910;
        public bool BendingL { get; set; }
        public bool BendingR { get; set; }
        public List<(int?, string)> MainRebars { get; set; }
        public List<(int?, string)> SubRebars { get; set; }
        public int MainAmount { get; set; }
        public int? SubAmount { get; set; }
        public int BendingHead { get; protected set; }
        #endregion

        #region Methods
        /// <summary>
        /// Process.
        /// </summary>
        public virtual void Prcs()
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
        protected abstract void FillFlds();

        // Fixation length
        protected abstract int FixnLen();

        // Rebar calculate bending left
        protected abstract void CalcRebarBdngL();

        // Rebar calculate bending right
        protected abstract void CalcRebarBdngR();

        // Rebar calculate bending couple head
        protected abstract void CalcRebarBdngLR();

        // Rebar calculate straight
        protected abstract void CalcRebarSt();

        // Amount calculate
        protected abstract void CalcAmt();
        #endregion

        #region Function
        // Joint count
        protected virtual int JtCnt(ref double w, double lMaxRawWood, int lFixn, int lBdngL, int lBdngR)
        {
            var jt = 1;
            var lMaxRawWoodRip = lMaxRawWood - lFixn;
            var body = 2 * lMaxRawWood - lBdngL - lBdngR - CHIDORI_HORZ - lFixn;
            while (w > body)
            {
                w -= lMaxRawWoodRip;
                jt++;
            }
            w = (w + jt * lFixn + BendingHead * L_BDNG).Round500();
            return jt;
        }

        // Process header main rebar
        protected virtual int PrcsHdrMainRebar(double w, double lMaxRawWood, int lFixn, int lBdngL, int lBdngR, out int lRddRebarL, out int lRddRebarR)
        {
            var jt = JtCnt(ref w, lMaxRawWood, lFixn, lBdngL, lBdngR);
            lRddRebarL = ((w + CHIDORI_HORZ) / 2).Round500();
            lRddRebarR = ((w - CHIDORI_HORZ) / 2).Round500();
            while (lRddRebarL + lRddRebarR > w)
            {
                lRddRebarR -= 500;
            }
            PrcsBdngHdrRebar(lBdngL, lBdngR, ref lRddRebarL, ref lRddRebarR);
            return jt;
        }

        // Process bending header rebar
        protected virtual void PrcsBdngHdrRebar(int lBdngL, int lBdngR, ref int lRddRebarL, ref int lRddRebarR)
        {
            lRddRebarL -= lBdngL;
            lRddRebarR -= lBdngR;
        }
        #endregion
    }
}
