using RimWorld;
using Verse;
using Verse.AI;

namespace Typhons
{
    internal class JobGiver_MimicNearbyObjects : ThinkNode_JobGiver
	{
		protected override Job TryGiveJob(Pawn pawn)
		{
			return null;

			//if ((target == null) || !pawn.CanReserve(target, 4)) return null;
			//return new Job(JobDefOf.AttackMelee, target)
			//{
			//	killIncappedTarget = true,
			//	expiryInterval = Rand.Range(420, 900),
			//};
		}
	}
}