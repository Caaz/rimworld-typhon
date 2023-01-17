using RimWorld;
using Verse;
using Verse.AI;

namespace Typhon.JobGiver
{
    public class CastAbilityOnJobTarget : JobGiver_AICastAbility
    {
        protected override LocalTargetInfo GetTarget(Pawn pawn, Ability ability)
        {
            LocalTargetInfo target = pawn.CurJob.targetA;
            if (!target.HasThing) return null;

            IntVec3 targetLocation = target.Cell;
            float num = pawn.Position.DistanceTo(targetLocation);
            VerbProperties verbProps = ability.verb.verbProps;
            if (num < verbProps.minRange || num > verbProps.range || !GenSight.LineOfSight(pawn.Position, targetLocation, pawn.Map))
            {
                return null;
            }
            if (!ability.verb.ValidateTarget(target, showMessages: false))
            {
                return null;
            }
            return target;
        }
    }
}
