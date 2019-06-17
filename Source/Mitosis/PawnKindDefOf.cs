using System;
using RimWorld;
using Verse;

namespace Mitosis
{
	// Token: 0x02000007 RID: 7
	[DefOf]
	public static class PawnKindDefOf
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000020A8 File Offset: 0x000002A8
		static PawnKindDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(PawnKindDefOf));
		}

		// Token: 0x04000005 RID: 5
		public static PawnKindDef ManBearPig;
	}
}
