using RimWorld;
using Verse;
using Verse.AI;
using UnityEngine;

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
        public static Pawn GenerateMimic() => GenerateTyphon(Random.Range(0.0f, 1.0f) > .2f ? TyphonDefOf.PawnKind.Typhon_Mimic : TyphonDefOf.PawnKind.Typhon_Greater_Mimic);
        public static Pawn GenerateWeaver() => GenerateTyphon(TyphonDefOf.PawnKind.Typhon_Weaver);
        public static Pawn GenerateCystoid() => GenerateTyphon(TyphonDefOf.PawnKind.Typhon_Cystoid);
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
            if (target == null) target = TyphonUtility.GetAttackableTarget(typhon, AttackRange(typhon));
            if (target == null) return null;
            Verb verb = typhon.TryGetAttackVerb(target, !typhon.IsColonist);
            bool isMelee = (verb == null || verb.IsMeleeAttack || verb.ApparelPreventsShooting() || typhon.CanReachImmediate(target, PathEndMode.Touch));
            if (typhon.def == TyphonDefOf.Thing.Typhon_Cystoid_Race) return JobMaker.MakeJob(TyphonDefOf.Job.TyphonCystoidAttack, target);
            if (typhon.def == TyphonDefOf.Thing.Typhon_Weaver_Race) return JobMaker.MakeJob(TyphonDefOf.Job.TyphonCreateCystoid, target);
            if (typhon.def == TyphonDefOf.Thing.Typhon_Telepath_Race && Random.Range(0f, 1f) > .5f) return JobMaker.MakeJob(TyphonDefOf.Job.TyphonMindControl, target);
            Job attackJob = JobMaker.MakeJob((isMelee) ? JobDefOf.AttackMelee : JobDefOf.AttackStatic, target);
            attackJob.maxNumStaticAttacks = 2;
            attackJob.killIncappedTarget = true;
            attackJob.expiryInterval = Rand.Range(420, 900);
            attackJob.endIfCantShootTargetFromCurPos = true;
            return attackJob;
        }
        public static bool AcceptablePrey(Pawn hunter, Thing prey)
        {
            if (
                prey == null
                || hunter == null
                || prey == hunter
                || prey.def.thingClass != typeof(Pawn)
                || !hunter.CanReserve(prey, 4)
                || IsTyphon(prey as Pawn)
            )
                return false;

            if (hunter.def == TyphonDefOf.Thing.Typhon_Cystoid_Race) return true;
            Pawn target = (Pawn)prey;

            return !(
                target.Dead
                || target.BodySize > PreySize(hunter)
                || !hunter.CanSee(target)
                || !hunter.CanReach(target, PathEndMode.OnCell, Danger.Deadly)
            );
        }
        private static float PreySize(Pawn typhon)
        {
            if (typhon.def == TyphonDefOf.Thing.Typhon_Cystoid_Race) return 15f;
            if (typhon.def == TyphonDefOf.Thing.Typhon_Weaver_Race) return 15f;
            if (typhon.def == TyphonDefOf.Thing.Typhon_Telepath_Race) return 15f;
            return 2f;
        }
        private static float AttackRange(Pawn typhon)
        {
            if (typhon.def == TyphonDefOf.Thing.Typhon_Mimic) return 5f;
            if (typhon.def == TyphonDefOf.Thing.Typhon_Phantom_Race) return 10f;
            if (typhon.def == TyphonDefOf.Thing.Typhon_Cystoid_Race) return 15f;
            if (typhon.def == TyphonDefOf.Thing.Typhon_Weaver_Race) return 10f;
            if (typhon.def == TyphonDefOf.Thing.Typhon_Telepath_Race) return 10f;
            return 15f;
        }
        public static bool IsTyphon(Pawn pawn)
        {
            if (pawn == null || pawn.Faction == null || pawn.RaceProps == null)
                return false;
            return pawn.Faction.def == TyphonDefOf.Faction.Typhon || pawn.RaceProps.FleshType == TyphonDefOf.FleshType.Typhon;
        }
        public static bool IsHiddenMimic(Thing thing)
        {
            Pawn pawn = thing as Pawn;
            return ((pawn != null) && ((pawn.kindDef == TyphonDefOf.PawnKind.Typhon_Mimic_Hidden) || (pawn.kindDef == TyphonDefOf.PawnKind.Typhon_Greater_Mimic_Hidden)));
        }
    }
}
