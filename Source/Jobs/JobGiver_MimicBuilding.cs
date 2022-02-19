using RimWorld;
using Verse;
using Verse.AI;

namespace Typhon
{
    internal class JobGiver_MimicBuilding : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            Building building = GetNearbyBuilding(pawn);
            if (building == null) return null;
            return JobMaker.MakeJob(TyphonDefOf.Job.TyphonMimicBuilding, building);
        }
        private Building GetNearbyBuilding(Pawn pawn)
        {
            foreach (Thing item in GenRadial.RadialDistinctThingsAround(pawn.Position, pawn.Map, 20f, true))
            {
                Building building = item as Building;
                if (
                    item.def.thingClass != typeof(Building)
                    || building == null
                    || building.def.passability == Traversability.Impassable
                    || building.RotatedSize.Area != 1
                    || building.def.building.isFence
                    || building.def.altitudeLayer != AltitudeLayer.Building
                    || ThingCompUtility.TryGetComp<CompCanBeDormant>(building) != null
                    || !pawn.CanSee(building)
                    || !pawn.CanReserveAndReach(building, PathEndMode.ClosestTouch, Danger.Deadly)
                ) continue;
                return building;
            }
            return null;
        }
    }
}