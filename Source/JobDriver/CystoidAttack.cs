using System;
using System.Collections.Generic;
using Verse.AI;

namespace Typhon.JobDriver
{
    internal class CystoidAttack : Verse.AI.JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }
        protected override IEnumerable<Toil> MakeNewToils()
        {
            AddFailCondition((Func<bool>)delegate
            {
                return (TargetA.Thing == null || TargetA.ThingDestroyed);
            });
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.OnCell).FailOnDespawnedOrNull(TargetIndex.A);
            yield return Toils_General.Do(Explode);
        }
        private void Explode()
        {
            pawn.Kill(null);
        }
    }
}
