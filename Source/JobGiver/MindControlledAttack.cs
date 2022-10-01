using Verse;
using Verse.AI;

namespace Typhon.JobGiver
{
    internal class MindControlledAttack : AttackPawn
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            Job job = base.TryGiveJob(pawn);
            if (job == null) return null;
            if (Mod.telepathsExplodeBrains)
            {
                Pawn target = job.GetTarget(TargetIndex.A).Pawn;
                job = JobMaker.MakeJob(TyphonDefOf.Job.TyphonMindBlower, target);
            }
            return job;
        }
    }
}