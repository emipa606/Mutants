using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Ent.Logic;

public static class BodypartUtility
{
    public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(this Pawn pawn, BodyPartRecord startingPart,
        HediffDef hediffDef)
    {
        var hediffs = pawn.health.hediffSet.hediffs;
        var currentSet = new List<BodyPartRecord>();
        var nextSet = new List<BodyPartRecord> { startingPart };
        do
        {
            currentSet.AddRange(nextSet);
            nextSet.Clear();
            foreach (var part in currentSet)
            {
                var matchingPart = false;
                for (var i = hediffs.Count - 1; i >= 0; i--)
                {
                    var hediff = hediffs[i];
                    if (hediff.Part != part || hediff.def != hediffDef)
                    {
                        continue;
                    }

                    matchingPart = true;
                    yield return part;
                }

                if (matchingPart)
                {
                    continue;
                }

                // ReSharper disable once ForCanBeConvertedToForeach
                for (var j = 0; j < part.parts.Count; j++)
                {
                    nextSet.Add(part.parts[j]);
                }
            }

            currentSet.Clear();
        } while (nextSet.Count > 0);
    }

    public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(this Pawn pawn, BodyPartRecord startingPart,
        HediffDef hediffDef, HediffDef hediffExceptionDef)
    {
        var hediffs = pawn.health.hediffSet.hediffs;
        var currentSet = new List<BodyPartRecord>();
        var nextSet = new List<BodyPartRecord> { startingPart };
        do
        {
            currentSet.AddRange(nextSet);
            nextSet.Clear();
            foreach (var part in currentSet)
            {
                var matchingPart = false;
                for (var i = hediffs.Count - 1; i >= 0; i--)
                {
                    var hediff = hediffs[i];
                    if (hediff.Part != part)
                    {
                        continue;
                    }

                    if (hediff.def == hediffExceptionDef)
                    {
                        matchingPart = true;
                        break;
                    }

                    if (hediff.def != hediffDef)
                    {
                        continue;
                    }

                    matchingPart = true;
                    yield return part;
                    break;
                }

                if (matchingPart)
                {
                    continue;
                }

                // ReSharper disable once ForCanBeConvertedToForeach
                for (var j = 0; j < part.parts.Count; j++)
                {
                    nextSet.Add(part.parts[j]);
                }
            }

            currentSet.Clear();
        } while (nextSet.Count > 0);
    }

    public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(this Pawn pawn, BodyPartRecord startingPart,
        HediffDef hediffDef, HediffDef hediffExceptionDef, Predicate<Hediff> extraExceptionPredicate)
    {
        var hediffs = pawn.health.hediffSet.hediffs;
        var currentSet = new List<BodyPartRecord>();
        var nextSet = new List<BodyPartRecord> { startingPart };
        do
        {
            currentSet.AddRange(nextSet);
            nextSet.Clear();
            foreach (var part in currentSet)
            {
                var matchingPart = false;
                for (var i = hediffs.Count - 1; i >= 0; i--)
                {
                    var hediff = hediffs[i];
                    if (hediff.Part != part)
                    {
                        continue;
                    }

                    if (hediff.def == hediffExceptionDef || extraExceptionPredicate(hediff))
                    {
                        matchingPart = true;
                        break;
                    }

                    if (hediff.def != hediffDef)
                    {
                        continue;
                    }

                    matchingPart = true;
                    yield return part;
                    break;
                }

                if (matchingPart)
                {
                    continue;
                }

                // ReSharper disable once ForCanBeConvertedToForeach
                for (var j = 0; j < part.parts.Count; j++)
                {
                    nextSet.Add(part.parts[j]);
                }
            }

            currentSet.Clear();
        } while (nextSet.Count > 0);
    }

    public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(this Pawn pawn, BodyPartRecord startingPart,
        HediffDef hediffDef, HediffDef[] hediffExceptionDefs)
    {
        var hediffs = pawn.health.hediffSet.hediffs;
        var currentSet = new List<BodyPartRecord>();
        var nextSet = new List<BodyPartRecord> { startingPart };
        do
        {
            currentSet.AddRange(nextSet);
            nextSet.Clear();
            foreach (var part in currentSet)
            {
                var matchingPart = false;
                for (var i = hediffs.Count - 1; i >= 0; i--)
                {
                    var hediff = hediffs[i];
                    if (hediff.Part != part)
                    {
                        continue;
                    }

                    if (hediffExceptionDefs.Contains(hediff.def))
                    {
                        matchingPart = true;
                        break;
                    }

                    if (hediff.def != hediffDef)
                    {
                        continue;
                    }

                    matchingPart = true;
                    yield return part;
                    break;
                }

                if (matchingPart)
                {
                    continue;
                }

                // ReSharper disable once ForCanBeConvertedToForeach
                for (var j = 0; j < part.parts.Count; j++)
                {
                    nextSet.Add(part.parts[j]);
                }
            }

            currentSet.Clear();
        } while (nextSet.Count > 0);
    }

    public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(this Pawn pawn, BodyPartRecord startingPart,
        HediffDef[] hediffDefs)
    {
        var hediffs = pawn.health.hediffSet.hediffs;
        var currentSet = new List<BodyPartRecord>();
        var nextSet = new List<BodyPartRecord> { startingPart };
        do
        {
            currentSet.AddRange(nextSet);
            nextSet.Clear();
            foreach (var part in currentSet)
            {
                var matchingPart = false;
                for (var i = hediffs.Count - 1; i >= 0; i--)
                {
                    var hediff = hediffs[i];
                    if (hediff.Part != part || !hediffDefs.Contains(hediff.def))
                    {
                        continue;
                    }

                    matchingPart = true;
                    yield return part;
                    break;
                }

                if (matchingPart)
                {
                    continue;
                }

                // ReSharper disable once ForCanBeConvertedToForeach
                for (var j = 0; j < part.parts.Count; j++)
                {
                    nextSet.Add(part.parts[j]);
                }
            }

            currentSet.Clear();
        } while (nextSet.Count > 0);
    }

    public static void ReplaceHediffFromBodypart(this Pawn pawn, BodyPartRecord startingPart, HediffDef hediffDef,
        HediffDef replaceWithDef)
    {
        var hediffs = pawn.health.hediffSet.hediffs;
        var list = new List<BodyPartRecord>();
        var list2 = new List<BodyPartRecord> { startingPart };
        do
        {
            list.AddRange(list2);
            list2.Clear();
            foreach (var item2 in list)
            {
                for (var num = hediffs.Count - 1; num >= 0; num--)
                {
                    var hediff = hediffs[num];
                    if (hediff.Part != item2 || hediff.def != hediffDef)
                    {
                        continue;
                    }

                    var hediff2 = hediffs[num];
                    hediffs.RemoveAt(num);
                    hediff2.PostRemoved();
                    var item = HediffMaker.MakeHediff(replaceWithDef, pawn, item2);
                    hediffs.Insert(num, item);
                }

                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < item2.parts.Count; i++)
                {
                    list2.Add(item2.parts[i]);
                }
            }

            list.Clear();
        } while (list2.Count > 0);
    }
}