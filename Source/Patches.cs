﻿using HarmonyLib;
using RimWorld;
using System;
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
                Pawn pawn = a as Pawn;
                if ((pawn != null) && (pawn.kindDef == TyphonDefOf.PawnKind.Typhon_Mimic_Hidden))
                {
                    __result = false;
                }
            }
        }
    }
}
