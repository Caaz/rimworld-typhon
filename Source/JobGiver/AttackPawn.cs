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

            Comps.Hivemind comp = pawn.GetComp<Comps.Hivemind>();
            if (comp != null)
                comp.SendSignal_Attack(pawn, attackJob.targetA.Pawn);

            return attackJob;
        }
        private void AlertHivemind(Pawn pawn, Pawn target)
        {
            Comps.Hivemind comp = pawn.GetComp<Comps.Hivemind>();
            if (comp != null)
                comp.SendSignal_Attack(pawn, target);
        }
    }
}