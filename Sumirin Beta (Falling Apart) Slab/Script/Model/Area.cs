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

        // Joint count
        protected abstract int JtCnt(ref double w, double lMaxRawWood, int lFixn, int lBdngL, int lBdngR);

        // Process header main rebar
        protected abstract int PrcsHdrMainRebar(double w, double lMaxRawWood, int lFixn, int lBdngL, int lBdngR, out int lRddRebarL, out int lRddRebarR);

        // Process bending header rebar
        protected virtual void PrcsBdngHdrRebar(int lBdngL, int lBdngR, ref int lRddRebarL, ref int lRddRebarR)
        {
            lRddRebarL -= lBdngL;
            lRddRebarR -= lBdngR;
        }
        #endregion
    }
}
