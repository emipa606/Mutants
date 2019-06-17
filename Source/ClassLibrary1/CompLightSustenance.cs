using System;
using Verse;

namespace Mutations
{
    // Token: 0x02000011 RID: 17
    public class CompLightSustenance : ThingComp
    {
        // Token: 0x1700000E RID: 14
        // (get) Token: 0x06000036 RID: 54 RVA: 0x000034A8 File Offset: 0x000016A8
        public CompProperties_LightSustenance Props
        {
            get
            {
                return (CompProperties_LightSustenance)this.props;
            }
        }

        // Token: 0x06000037 RID: 55 RVA: 0x000034C8 File Offset: 0x000016C8
        public override void CompTick()
        {
            Pawn pawn = this.parent as Pawn;
            bool spawned = pawn.Spawned;
            if (spawned)
            {
                bool flag = this.addHediffOnce;
                if (flag)
                {
                    pawn.health.AddHediff(HediffDef.Named("AA_LightSustenance"), null, null, null);
                    Hediff firstHediffOfDef = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("AA_LightSustenance"), false);
                    firstHediffOfDef.Severity = 0.2f;
                    this.addHediffOnce = false;
                }
                float num = this.parent.Map.glowGrid.GameGlowAt(this.parent.Position, false);
                bool flag2 = num >= this.growOptimalGlow;
                if (flag2)
                {
                    Hediff firstHediffOfDef2 = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("AA_LightSustenance"), false);
                    bool flag3 = firstHediffOfDef2 != null && firstHediffOfDef2.Severity > 0f;
                    if (flag3)
                    {
                        firstHediffOfDef2.Severity -= 1E-05f;
                    }
                }
                else
                {
                    Hediff firstHediffOfDef3 = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("AA_LightSustenance"), false);
                    bool flag4 = firstHediffOfDef3 != null && firstHediffOfDef3.Severity < 1f;
                    if (flag4)
                    {
                        firstHediffOfDef3.Severity += 1E-05f;
                    }
                }
            }
        }

        // Token: 0x0400001F RID: 31
        public float growOptimalGlow = 0.4f;

        // Token: 0x04000020 RID: 32
        private bool addHediffOnce = true;
    }
}
