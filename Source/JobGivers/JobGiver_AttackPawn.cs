using RimWorld;
using Verse;
using Verse.AI;

namespace Typhon
{
    internal class JobGiver_AttackPawn : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            Job attackJob = TyphonUtility.AttackJob(pawn, null);
            if (attackJob == null) return null;
            CompHivemind comp = pawn.GetComp<CompHivemind>();
            if(comp == null) return null;
            comp.SendAttackSignal(pawn, attackJob.targetA.Pawn);
            return attackJob;
        }
    }
}