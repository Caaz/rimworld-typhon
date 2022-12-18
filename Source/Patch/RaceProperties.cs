using HarmonyLib;

namespace Typhon.Patch.RaceProperties
{
    [HarmonyPatch(typeof(Verse.RaceProperties), nameof(Verse.RaceProperties.IsFlesh), MethodType.Getter)]
    class IsFlesh
    {
        static void Postfix(Verse.RaceProperties __instance, ref bool __result)
        {
            if (__instance.FleshType == FleshTypeDefOf.Typhon_FleshType_Typhon) __result = false;
        }
    }
}