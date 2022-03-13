using RimWorld;
using Verse;
using Verse.AI;

namespace Typhon.JobGiver
{
    internal class AttackPawn : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            Job attackJob = TyphonUtility.AttackJob(pawn);
            if (attackJob == null) return null;
            CompHivemind comp = pawn.GetComp<CompHivemind>();
            if(comp != null)
                comp.SendAttackSignal(pawn, attackJob.targetA.Pawn);
            return attackJob;
        }
    }
}