using HarmonyLib;
using RimWorld;
using Verse;

namespace Typhon.Patch.PawnComponentsUtility
{
    [HarmonyPatch(typeof(RimWorld.PawnComponentsUtility), nameof(RimWorld.PawnComponentsUtility.CreateInitialComponents))]
    class CreateInitialComponents
    {
        static void Postfix(Pawn pawn)
        {
            if (
                pawn.RaceProps.FleshType == FleshTypeDefOf.Typhon_FleshType_Typhon
            )
            {
                if (pawn.abilities == null) pawn.abilities = new Pawn_AbilityTracker(pawn);
                if (pawn.psychicEntropy == null) pawn.psychicEntropy = new RimWorld.Pawn_PsychicEntropyTracker(pawn);
            }
        }
    }
}