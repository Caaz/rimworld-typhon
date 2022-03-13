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
            typhon.health.RemoveAllHediffs();
            return typhon;
        }
        public static Pawn GenerateMimic() => GenerateTyphon(TyphonDefOf.PawnKind.Typhon_Mimic);
        public static Pawn GenerateWeaver() => GenerateTyphon(TyphonDefOf.PawnKind.Typhon_Weaver);
        public static Pawn GeneratePhantom(Pawn from)
        {
            Pawn phantom = GenerateTyphon(TyphonDefOf.PawnKind.Typhon_Phantom);
            phantom.Name = from.Name;
            return phantom;
        }
        public static Pawn GetAttackableTarget(Pawn pawn, float distance = 4f)
        {
            foreach (Thing thing in GenRadial.RadialDistinctThingsAround(pawn.Position, pawn.Map, distance, true))
                if (TyphonUtility.AcceptablePrey(pawn, thing))
                    return (Pawn)thing;
            return null;
        }
        public static Job AttackJob(Pawn typhon, Pawn target = null)
        {
            Job job;
            if (target == null) target = TyphonUtility.GetAttackableTarget(typhon);
            if (target == null) return null;
            // Try ranged attack job
            Verb verb = typhon.TryGetAttackVerb(target, !typhon.IsColonist);
            if (verb == null || verb.IsMeleeAttack || verb.ApparelPreventsShooting() || typhon.CanReachImmediate(target, PathEndMode.Touch))
            {
                job = JobMaker.MakeJob(JobDefOf.AttackMelee, target);
                job.killIncappedTarget = true;
                job.expiryInterval = Rand.Range(420, 900);
                return job;
            }
            job = JobMaker.MakeJob(JobDefOf.AttackStatic, target);
            job.maxNumStaticAttacks = 2;
            job.killIncappedTarget = true;
            job.expiryInterval = Rand.Range(420, 900);
            job.endIfCantShootTargetFromCurPos = true;
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
        public static bool AcceptablePrey(Pawn hunter, Thing prey)
        {
            if (
                prey == null
                || hunter == null
                || prey == hunter
                || prey.def.thingClass != typeof(Pawn)
                || !hunter.CanReserve(prey, 4)
            )
                return false;

            Pawn target = (Pawn)prey;

            return !(
                target.Dead
                || !target.RaceProps.IsFlesh
                || target.BodySize > 1.2
                || !hunter.CanSee(target)
                || !hunter.CanReach(target, PathEndMode.OnCell, Danger.Deadly)
            );
        }
    }
}
