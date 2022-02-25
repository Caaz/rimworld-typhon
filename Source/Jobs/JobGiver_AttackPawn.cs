using RimWorld;
using Verse;
using Verse.AI;

namespace Typhon
{
    internal class JobGiver_AttackPawn : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            Pawn target = TyphonUtility.GetAttackableTarget(pawn, 5);
            if ((target == null) || !pawn.CanReserve(target, 4)) return null;
            Job job = JobMaker.MakeJob(TyphonDefOf.Job.TyphonAttackPawn, target);
            job.killIncappedTarget = true;
            job.expiryInterval = Rand.Range(420, 900);
            return job;
        }
    }
}