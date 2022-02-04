using RimWorld;
using Verse;
using Verse.AI;

namespace Typhon
{
    internal class JobGiver_AttackNearbyPawns : ThinkNode_JobGiver
	{
		protected override Job TryGiveJob(Pawn pawn)
		{
            Pawn target = (Pawn)AttackTargetFinder.BestAttackTarget(pawn, TargetScanFlags.None, null, 0, 10);
			if ((target == null) || !pawn.CanReserve(target, 4)) return null;
			return new Job(JobDefOf.AttackMelee, target)
			{
				killIncappedTarget = true,
				expiryInterval = Rand.Range(420, 900),
			};
		}
	}
}