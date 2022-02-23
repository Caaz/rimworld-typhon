using HarmonyLib;
using Verse;

namespace Typhon
{
    internal class Patches
    {
        [HarmonyPatch(typeof(RaceProperties), nameof(RaceProperties.IsFlesh), MethodType.Getter)]
        class RacePropertiesIsFlesh
        {
            static void Postfix(RaceProperties __instance, ref bool __result)
            {
                if (__instance.FleshType == TyphonDefOf.FleshType.Typhon) __result = false;
            }
        }
    }
}
