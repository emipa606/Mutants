using System.Reflection;
using HarmonyLib;
using Verse;

namespace VariableBleedRate.VariableBleedRate;

[StaticConstructorOnStartup]
internal static class Main
{
    static Main()
    {
        Log.Message("VBRF :: Patching TotalBleedRate");
        var val = new Harmony("com.rate.bleed");
        val.PatchAll(Assembly.GetExecutingAssembly());
    }
}