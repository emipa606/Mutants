using System;
using RimWorld;
using Verse;

namespace Mitosis
{
	// Token: 0x02000002 RID: 2
	public class HediffComp_Hatcher : HediffComp
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public HediffCompProperties_Hatcher Props
		{
			get
			{
				return (HediffCompProperties_Hatcher)this.props;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205D File Offset: 0x0000025D
		public override void CompPostTick(ref float severityAdjustment)
		{
			this.Hatch();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020BC File Offset: 0x000002BC
		public void Hatch()
		{
			if ((float)this.HatchingTicker < this.Props.hatcherDaystoHatch * 60000f)
			{
				this.HatchingTicker++;
				return;
			}
			if (this.parent.pawn.Map != null && (this.parent.pawn.Faction == Faction.OfPlayer || (this.parent.pawn.IsPrisoner && this.parent.pawn.Map.IsPlayerHome)))
			{
				GenSpawn.Spawn(ThingDef.Named("Mote_MakeMBP"), this.parent.pawn.Position, this.parent.pawn.Map, WipeMode.Vanish);
			}
			this.HatchingTicker = 0;
		}

		// Token: 0x04000001 RID: 1
		private int HatchingTicker;
	}
}
