using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Verse;

namespace Typhon
{
    internal class Dummy
    {
        public bool ShouldShowDot(Pawn pawn) { return false; }
    }
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

        [HarmonyPatch(typeof(GenHostility), nameof(GenHostility.HostileTo), new Type[] { typeof(Thing), typeof(Thing) })]
        class GenHostilityHostileTo
        {
            static void Postfix(Thing a, Thing b, ref bool __result)
            {
                if (TyphonUtility.IsHiddenMimic(a) || TyphonUtility.IsHiddenMimic(b))
                {
                    __result = false;
                }
            }
        }
        [HarmonyPatch]
        class ShouldDrawDot
        {
            static IEnumerable<MethodBase> TargetMethods()
            {
                Type type = AccessTools.TypeByName("CameraPlus.Tools");
                Log.Message("Trying for " + type);
                if (type == null)
                {
                    type = typeof(Dummy);
                    Log.Message("Dummy instead " + type);
                }
                return type.GetMethods().Where(method => method.Name == "ShouldShowDot");
            }
            static bool Prefix(Pawn pawn, ref bool __result)
            {
                if (TyphonUtility.IsHiddenMimic(pawn))
                {
                    __result = false;
                    return false;
                }
                return true;
            }
        }
    }
}
