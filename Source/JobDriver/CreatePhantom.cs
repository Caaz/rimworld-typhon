using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Typhon.JobDriver
{
    internal class CreatePhantom : Verse.AI.JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(job.GetTarget(TargetIndex.A), job, 1, -1, null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.OnCell).FailOnDespawnedOrNull(TargetIndex.A);
            Toil toil = Toils_General.Wait(200);
            toil.WithProgressBarToilDelay(TargetIndex.A);
            toil.FailOnDespawnedOrNull(TargetIndex.A);
            toil.FailOnCannotTouch(TargetIndex.A, PathEndMode.OnCell);
            toil.FailOn((System.Func<bool>)delegate
            {
                Corpse target = (Corpse)job.GetTarget(TargetIndex.A);
                return target.GetRotStage() != RotStage.Dessicated;
            });
            yield return toil;
            yield return Toils_General.Do(PhantomGenesis);
        }
        private void PhantomGenesis()
        {
            Corpse target = (Corpse)job.GetTarget(TargetIndex.A);
            Pawn typhon = CreateTyphon(target.InnerPawn);
            GenSpawn.Spawn(typhon, pawn.Position, pawn.Map);
            target.Destroy();
        }
        private Pawn CreateTyphon(Pawn pawn)
        {
            if (Random.Range(0f, 1f) > .9f) return TyphonUtility.GenerateTelepath();
            return TyphonUtility.GeneratePhantom(pawn);
        }
    }
}
