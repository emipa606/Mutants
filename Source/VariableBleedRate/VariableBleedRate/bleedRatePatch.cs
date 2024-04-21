using HarmonyLib;
using RimWorld;
using Verse;

namespace VariableBleedRate.VariableBleedRate;

[HarmonyPatch(typeof(HediffSet), "CalculateBleedRate")]
internal static class bleedRatePatch
{
    public static void Postfix(ref float __result, HediffSet __instance)
    {
        __result *= __instance.pawn.GetStatValue(StatDef.Named("BleedRate"));
    }
}