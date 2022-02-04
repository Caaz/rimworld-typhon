using RimWorld;
using Verse;
using Verse.AI;

namespace Typhons
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
			foreach (Thing item in GenRadial.RadialDistinctThingsAround(pawn.Position, pawn.Map, 20f, useCenter: true))
			{
				if (item.def.thingClass != typeof(Building)) continue;
				Building building = item as Building;
				if (building == null) continue;
				if (building.def.passability == Traversability.Impassable)
					continue;
				if (!ReservationUtility.CanReserveAndReach(pawn, building, PathEndMode.ClosestTouch, Danger.Deadly)) continue;
				return building;
			}
			return null;
		}
	}
}