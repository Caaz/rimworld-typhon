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
            Faction typhonFaction = FactionUtility.DefaultFactionFrom(kind.defaultFactionType);
            if (typhonFaction == null) typhonFaction = Faction.OfMechanoids;
            Pawn typhon = PawnGenerator.GeneratePawn(kind, typhonFaction);
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
            if (target == null) target = TyphonUtility.GetAttackableTarget(typhon, AttackRange(typhon));
            if (target == null) return null;
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
                || target.BodySize > PreySize(hunter)
                || !hunter.CanSee(target)
                || !hunter.CanReach(target, PathEndMode.OnCell, Danger.Deadly)
            );
        }
        private static float PreySize(Pawn typhon)
        {
            if (typhon.def == TyphonDefOf.Thing.Typhon_Mimic) return 1.2f;
            if (typhon.def == TyphonDefOf.Thing.Typhon_Phantom_Race) return 2f;
            return 1f;
        }
        private static float AttackRange(Pawn typhon)
        {
            if (typhon.def == TyphonDefOf.Thing.Typhon_Mimic) return 5f;
            if (typhon.def == TyphonDefOf.Thing.Typhon_Phantom_Race) return 10f;
            return 5f;
        }
    }
}
