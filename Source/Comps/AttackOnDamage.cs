using RimWorld;
using Verse;
using Verse.AI;

namespace Typhon.Comps
{
    internal class AttackOnDamage : ThingComp
    {
        private CompProperties.AttackOnDamage Props => (CompProperties.AttackOnDamage)props;
        public override void PostPostApplyDamage(DamageInfo dinfo, float totalDamageDealt)
        {
            Pawn pawn = parent as Pawn;
            if (pawn == null || pawn.Dead) return;
            if (pawn.CurJobDef == JobDefOf.AttackMelee || pawn.CurJobDef == JobDefOf.AttackStatic)
                return;
            Pawn target = dinfo.Instigator as Pawn;
            if (target != null)
            {
                Job job = TyphonUtility.AttackJob(pawn, target);
                if (job != null)
                {
                    pawn.jobs.StopAll();
                    pawn.jobs.StartJob(job);
                    Comps.Hivemind comp = pawn.GetComp<Comps.Hivemind>();
                    if (comp != null)
                        comp.SendSignal_Attack(pawn, target);
                }
            }
        }
    }
}
