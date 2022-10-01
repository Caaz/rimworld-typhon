using System.Linq;
using RimWorld;
using Typhon.Hediffs;
using Verse;
using Verse.AI;

namespace Typhon.JobGiver
{
    internal class MindControlledWander : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            // GenRadial.RadialCellsAround()
            MindControlled hediff = pawn.health.hediffSet.GetFirstHediffOfDef(TyphonDefOf.Hediff.TyphonMindControlled) as MindControlled;
            if (hediff == null || hediff.mindController == null || hediff.mindController.Dead)
            {
                pawn.health.RemoveHediff(hediff);
                return null;
            }
            IntVec3 position = GenRadial.RadialCellsAround(hediff.mindController.Position, 5, true).RandomElement();
            return JobMaker.MakeJob(JobDefOf.Goto, position);
        }
    }
}