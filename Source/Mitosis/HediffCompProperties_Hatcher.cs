using System;
using Verse;

namespace Mitosis
{
	// Token: 0x02000003 RID: 3
	public class HediffCompProperties_Hatcher : HediffCompProperties
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000206D File Offset: 0x0000026D
		public HediffCompProperties_Hatcher()
		{
			this.compClass = typeof(HediffComp_Hatcher);
		}

		// Token: 0x04000002 RID: 2
		public float hatcherDaystoHatch = 1f;
	}
}
