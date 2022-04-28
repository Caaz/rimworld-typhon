using RimWorld;
using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Typhon.JobDriver
{
    internal class MimicBuilding : Verse.AI.JobDriver
    {
        private Building Copying => (Building)job.GetTarget(TargetIndex.A).Thing;
        private Building Copy => (Building)job.GetTarget(TargetIndex.B).Thing;
        private int buildingHitPoints;
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(Copying, job, 1, -1, null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            AddFinishAction(delegate
            {
                if (!Copy.DestroyedOrNull())
                    Copy.Destroy(DestroyMode.Vanish);
                UpdateMimic(false);
            });
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.ClosestTouch).FailOnDespawnedOrNull(TargetIndex.A);
            yield return Mimicry();
            if (pawn.CanReach(TargetB, PathEndMode.OnCell, Danger.Deadly))
                yield return Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.Touch).FailOnDespawnedOrNull(TargetIndex.B);
            yield return Wait();
        }
        public override void ExposeData()
        {
            Scribe_Values.Look(ref buildingHitPoints, "buildingHitPoints", 0);
            base.ExposeData();
        }
        private Toil Wait()
        {
            Toil wait_toil = Toils_General.Wait(Int32.MaxValue);
            wait_toil.FailOn((Func<bool>)delegate
            {
                Pawn target = TyphonUtility.GetAttackableTarget(pawn);
                return (target != null || Copy == null || Copy.DestroyedOrNull() || Copy.HitPoints != buildingHitPoints);
            });
            return wait_toil;
        }
        private Toil Mimicry()
        {
            Toil mimic_toil = new Toil();
            mimic_toil.initAction = delegate
            {
                Building copy = (Building)ThingMaker.MakeThing(Copying.def, Copying.Stuff);
                job.SetTarget(TargetIndex.B, copy);
                bool placed = GenPlace.TryPlaceThing(Copy, pawn.Position, pawn.Map, ThingPlaceMode.Near);
                if (!placed)
                    EndJobWith(JobCondition.Errored);
            };
            mimic_toil.AddFinishAction(delegate
            {
                buildingHitPoints = Copy.HitPoints;
                UpdateMimic(true);
            });
            return mimic_toil;
        }

        private void UpdateMimic(bool hidden)
        {
            PawnKindDef pawnKind = (hidden) ? TyphonDefOf.PawnKind.Typhon_Mimic_Hidden : TyphonDefOf.PawnKind.Typhon_Mimic;
            ThingDef thing = (hidden) ? TyphonDefOf.Thing.Typhon_Mimic_Hidden : TyphonDefOf.Thing.Typhon_Mimic;
            RegionListersUpdater.DeregisterInRegions(pawn, pawn.Map);
            pawn.def = thing;
            pawn.ChangeKind(pawnKind);
            pawn.Drawer.renderer.graphics.ResolveAllGraphics();
            RegionListersUpdater.RegisterInRegions(pawn, pawn.Map);
        }
    }
}
