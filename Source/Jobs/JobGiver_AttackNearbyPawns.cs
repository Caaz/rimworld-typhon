using RimWorld;
using Verse;
using Verse.AI;

namespace Typhon
{
    internal class JobGiver_AttackNearbyPawns : ThinkNode_JobGiver
	{
		protected override Job TryGiveJob(Pawn pawn)
		{
            Pawn target = GetAttackableTarget(pawn);
            if ((target == null) || !pawn.CanReserve(target, 4)) return null;
			return new Job(JobDefOf.AttackMelee, target)
			{
				killIncappedTarget = true,
				expiryInterval = Rand.Range(420, 900),
			};
        }
        private Pawn GetAttackableTarget(Pawn pawn)
        {
            foreach (Thing item in GenRadial.RadialDistinctThingsAround(pawn.Position, pawn.Map, 2f, useCenter: true))
            {
                if (item.def.thingClass != typeof(Pawn)) continue;
                Pawn target = (Pawn)item;
                if (target.Dead) continue;
                if (!(target.RaceProps.FleshType == FleshTypeDefOf.Normal)) continue;
                if (!pawn.CanSee(target)) continue;
                return target;
            }
            return null;
        }
    }
}