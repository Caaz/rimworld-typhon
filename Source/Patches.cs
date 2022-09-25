using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                if (type != null)
                {
                    IEnumerable<MethodInfo> methods = type.GetMethods().Where(method => method.Name == "ShouldShowDot");
                    foreach (MethodInfo method in methods)
                    {
                        Log.Message("Patching " + method);
                    }
                    return methods;
                }
                return new List<MethodBase> { };
            }
            static void PostFix(Pawn pawn, ref bool __result)
            {
                Log.Message("Should Show dot? " + pawn);
                if (TyphonUtility.IsHiddenMimic(pawn))
                {
                    Log.Message("Shouldn't draw dot for this hidden mimic");
                    __result = false;
                }
            }
        }
    }
}
