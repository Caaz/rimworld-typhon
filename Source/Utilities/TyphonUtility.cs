using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Typhon
{
    internal class TyphonUtility
    {
        public static Pawn GenerateTyphon(PawnKindDef kind)
        {
            Pawn typhon = PawnGenerator.GeneratePawn(kind, FactionUtility.DefaultFactionFrom(kind.defaultFactionType));
            typhon.ageTracker.AgeBiologicalTicks = 0;
            typhon.ageTracker.AgeChronologicalTicks = 0;
            return typhon;
        }
        public static Pawn GenerateMimic() => GenerateTyphon(TyphonDefOf.PawnKind.Typhon_Mimic);
        public static Pawn GenerateWeaver() => GenerateTyphon(TyphonDefOf.PawnKind.Typhon_Weaver);
        public static Pawn GetAttackableTarget(Pawn pawn, float distance = 4f)
        {
            foreach (Thing thing in GenRadial.RadialDistinctThingsAround(pawn.Position, pawn.Map, distance * 2, true))
            {
                if (pawn.Position.DistanceTo(thing.Position) < distance)
                    if (TyphonUtility.AcceptablePrey(pawn, thing))
                        return (Pawn)thing;
            }

            return null;
        }
        public static Job AttackJob(Pawn typhon, Pawn target, bool skipValidation = false)
        {
            if (target == null)
                target = TyphonUtility.GetAttackableTarget(typhon, 5);
            else if (!skipValidation && !AcceptablePrey(typhon, target)) return null;
            if (!typhon.CanReach(target, PathEndMode.OnCell, Danger.Deadly)) return null;
            if ((target == null) || !typhon.CanReserve(target, 4)) return null;
            Job job = JobMaker.MakeJob(TyphonDefOf.Job.TyphonAttackPawn, target);
            job.killIncappedTarget = true;
            job.expiryInterval = Rand.Range(420, 900);
            return job;
        }
        private static bool IsTyphon(Thing thing)
        {
            return (
                thing.def == TyphonDefOf.Thing.Typhon_Mimic
                || thing.def == TyphonDefOf.Thing.Typhon_Mimic_Hidden
                || thing.def == TyphonDefOf.Thing.Typhon_Weaver_Race
            );
        }
        private static bool AcceptablePrey(Pawn hunter, Thing prey)
        {
            if (
                prey == null
                || hunter == null
                || prey == hunter
                || prey.def.thingClass != typeof(Pawn)
            )
                return false;

            Pawn target = (Pawn)prey;

            return !(
                target.Dead
                || !target.RaceProps.IsFlesh
                || target.BodySize > 1.2
                || !hunter.CanSee(target)
            );
        }
    }
}
