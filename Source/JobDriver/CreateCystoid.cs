using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Typhon.JobDriver
{
    internal class CreateCystoid : Verse.AI.JobDriver
    {
        private int wait_time = 20;
        private int burst_amount = 3;
        private int cystoid_amount = 1;
        private int cooldown = 200;
        public override bool TryMakePreToilReservations(bool errorOnFailed) => true;

        protected override IEnumerable<Toil> MakeNewToils()
        {
            for (int i = 0; i < burst_amount; i++)
            {
                TryFindShootingPosition(out var dest);
                if (dest != pawn.Position)
                    yield return Toils_Goto.GotoCell(dest, PathEndMode.OnCell);
                yield return Toils_General.Wait(wait_time);
                yield return Toils_General.Do(SpawnCystoids);
            }
            yield return Toils_General.Wait(cooldown);
        }
        private void SpawnCystoids()
        {
            for (int i = 0; i < cystoid_amount; i++)
                GenSpawn.Spawn(TyphonUtility.GenerateCystoid(), pawn.Position, pawn.Map);
        }
        private bool TryFindShootingPosition(out IntVec3 dest)
        {
            CastPositionRequest newReq = default(CastPositionRequest);
            newReq.caster = pawn;
            newReq.target = TargetThingA;
            newReq.maxRangeFromTarget = 14;
            newReq.wantCoverFromTarget = true;
            return CoverFinder.TryFindCastPosition(newReq, 14f, out dest);
        }
    }
}
