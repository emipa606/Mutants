using System.Text;
using Ent.Logic;
using RimWorld;
using Verse;

namespace Ent;

public class GrowingPartHediff : Hediff_AddedPart
{
    public override bool ShouldRemove => Severity >= def.maxSeverity;

    public override string TipStringExtra
    {
        get
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(base.TipStringExtra);
            stringBuilder.AppendLine("Efficiency".Translate() + ": " +
                                     def.addedPartProps.partEfficiency.ToStringPercent());
            stringBuilder.AppendLine($"Growth: {Severity.ToStringPercent()}");
            return stringBuilder.ToString();
        }
    }

    public override void PostRemoved()
    {
        base.PostRemoved();
        if (Severity >= 1f)
        {
            pawn.ReplaceHediffFromBodypart(Part, HediffDefOf.MissingBodyPart, EntHediffDefOf.EntCuredBodypart);
        }
    }
}