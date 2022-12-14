using HarmonyLib;
using RimWorld;
using Verse;

namespace Typhon.Patches.PawnComponentsUtility
{
    [HarmonyPatch(typeof(RimWorld.PawnComponentsUtility), nameof(RimWorld.PawnComponentsUtility.CreateInitialComponents))]
    class CreateInitialComponents
    {
        static void Postfix(Pawn pawn)
        {
            if (
                pawn.RaceProps.FleshType == FleshTypeDefOf.Typhon_FleshType_Typhon
                && pawn.abilities == null
            )
            {
                pawn.abilities = new Pawn_AbilityTracker(pawn);
            }
        }
    }
}