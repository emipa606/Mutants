using System;
using RimWorld;
using Verse;

namespace Mitosis
{
	// Token: 0x02000004 RID: 4
	public class GetPawnThing : MoteThrown
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000217C File Offset: 0x0000037C
		public override void Tick()
		{
			if (base.Map == null)
			{
				this.Destroy(DestroyMode.Vanish);
			}
			GenSpawn.Spawn(PawnGenerator.GeneratePawn(new PawnGenerationRequest(PawnKindDefOf.ManBearPig, null, PawnGenerationContext.NonPlayer, -1, false, true, false, false, true, false, 1f, false, true, true, false, false, false, false, null, null, null, null, null, null, null, null)), base.Position, base.Map, WipeMode.Vanish);
			this.Destroy(DestroyMode.Vanish);
		}
	}
}
