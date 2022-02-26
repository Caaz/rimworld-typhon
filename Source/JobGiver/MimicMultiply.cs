using RimWorld;
using Verse;
using Verse.AI;

namespace Typhon.JobGiver
{
    internal class MimicMultiply : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            Corpse target = (Corpse)GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, ThingRequest.ForGroup(ThingRequestGroup.Corpse), PathEndMode.Touch, TraverseParms.For(pawn), 10);
            if (
                target.GetRotStage() == RotStage.Dessicated
                || !pawn.CanReserveAndReach(target, PathEndMode.Touch, Danger.Deadly)
                || !(target.InnerPawn.RaceProps.FleshType == FleshTypeDefOf.Normal)
            ) return null;
            return JobMaker.MakeJob(TyphonDefOf.Job.TyphonMimicMultiply, target);
        }
    }
}
