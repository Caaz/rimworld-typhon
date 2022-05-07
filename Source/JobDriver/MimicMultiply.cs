using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Typhon.JobDriver
{
    internal class MimicMultiply : Verse.AI.JobDriver
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
                return target.GetRotStage() == RotStage.Dessicated;
            });
            yield return toil;
            yield return Toils_General.Do(Multiply);
        }
        private void Multiply()
        {
            Corpse target = (Corpse)job.GetTarget(TargetIndex.A);
            int mimicAmount = (int)(target.InnerPawn.BodySize * 4);
            if (mimicAmount > 1)
                for (int i = 0; i < mimicAmount - 1; i++)
                    GenSpawn.Spawn(TyphonUtility.GenerateMimic(), pawn.Position, pawn.Map);

            ProcessCorpse(target);
        }
        private void ProcessCorpse(Corpse corpse)
        {
            if (Mod.mimicsDestroyCorpses)
                corpse.Destroy();
            else
            {
                CompRottable comp = corpse.GetComp<CompRottable>();
                if (comp != null)
                    comp.RotProgress = comp.PropsRot.TicksToDessicated;
            }
        }
    }
}
