using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Typhons
{
    internal class JobDriver_MimicMultiply : JobDriver
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
            for(int i = 0; i < 3; i ++)
            {
                PawnKindDef mimicKind = TyphonDefOf.PawnKind.Typhon_Mimic;
                Pawn child = PawnGenerator.GeneratePawn(mimicKind, FactionUtility.DefaultFactionFrom(mimicKind.defaultFactionType));
                child.ageTracker.AgeBiologicalTicks = 0;
                child.ageTracker.AgeChronologicalTicks = 0;
                GenSpawn.Spawn(child, pawn.Position, pawn.Map);
            }
            CompRottable comp = target.GetComp<CompRottable>();
            if (comp != null)
                comp.RotProgress = comp.PropsRot.TicksToDessicated;
        }
    }
}
