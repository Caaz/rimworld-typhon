using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Typhon
{
    internal class TyphonUtility
    {
        public static Pawn GenerateMimic()
        {
            PawnKindDef mimicKind = TyphonDefOf.PawnKind.Typhon_Mimic;
            Pawn mimic = PawnGenerator.GeneratePawn(mimicKind, FactionUtility.DefaultFactionFrom(mimicKind.defaultFactionType));
            mimic.ageTracker.AgeBiologicalTicks = 0;
            mimic.ageTracker.AgeChronologicalTicks = 0;
            return mimic;
        }
        public static Pawn GetAttackableTarget(Pawn pawn, float distance = 4f)
        {
            foreach (Thing thing in GenRadial.RadialDistinctThingsAround(pawn.Position, pawn.Map, distance * 2, true))
            {
                if (pawn.Position.DistanceTo(thing.Position) < distance)
                    if (TyphonUtility.AcceptablePrey(pawn, thing))
                        return (Pawn)thing;

                if (TyphonUtility.IsTyphon(thing))
                {
                    Pawn typhon = thing as Pawn;
                    Pawn target = typhon.LastAttackedTarget.Pawn;
                    if (target == null || target.Dead) continue;
                    return target;
                }
            }

            return null;
        }
        private static bool IsTyphon(Thing thing)
        {
            return (thing.def == TyphonDefOf.Thing.Typhon_Mimic);
        }
        private static bool AcceptablePrey(Pawn hunter, Thing prey)
        {
            if (
                prey == null
                || hunter == null
                || prey.def.thingClass != typeof(Pawn)
            )
                return false;

            Pawn target = (Pawn)prey;

            return !(
                target.Dead
                || !(target.RaceProps.FleshType == FleshTypeDefOf.Normal)
                || target.BodySize > 1.2
                || !hunter.CanSee(target)
            );
        }
    }
}
