using HarmonyLib;

namespace Typhon.Patch.Pawn_PsychicEntropyTracker
{
    [HarmonyPatch(typeof(RimWorld.Pawn_PsychicEntropyTracker), nameof(RimWorld.Pawn_PsychicEntropyTracker.CurrentPsyfocus), MethodType.Getter)]
    class CurrentPsyfocus
    {
        static void Postfix(RimWorld.Pawn_PsychicEntropyTracker __instance, ref float __result)
        {
            if (__instance.Pawn.RaceProps.FleshType == FleshTypeDefOf.Typhon_FleshType_Typhon)
            {
                __result = 1f;
            }
        }
    }
}