using RimWorld;
using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Typhon
{
    internal class JobDriver_MimicBuilding : JobDriver
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
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch).FailOnDespawnedOrNull(TargetIndex.A);
            yield return Mimicry();
            yield return Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.OnCell);
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
                Pawn target = (Pawn)AttackTargetFinder.BestAttackTarget(pawn, TargetScanFlags.None, null, 0, 3);
                bool failed = (target != null || Copy == null || Copy.DestroyedOrNull() || Copy.HitPoints != buildingHitPoints);
                return failed;
            });
            wait_toil.AddFinishAction(delegate
            {
                Log.Message("Finishing up.");
                if (!Copy.DestroyedOrNull())
                    Copy.Destroy(DestroyMode.Vanish);
                UpdateMimic(false);
            });
            return wait_toil;
        }
        private Toil Mimicry()
        {
            Toil mimic_toil = new Toil();
            mimic_toil.initAction = delegate
            {
                mimic_toil.actor.pather.StopDead();
                Building copy = (Building)ThingMaker.MakeThing(Copying.def, Copying.Stuff);
                job.SetTarget(TargetIndex.B, copy);
                bool placed = GenPlace.TryPlaceThing(Copy, pawn.Position, pawn.Map, ThingPlaceMode.Near);
                Log.Message("Placed? " + placed);
                buildingHitPoints = Copy.HitPoints;
                UpdateMimic(true);
            };
            return mimic_toil;
        }

        private void UpdateMimic(bool hidden)
        {
            PawnKindDef pawnKind = (hidden) ? TyphonDefOf.PawnKind.Typhon_Mimic_Hidden : TyphonDefOf.PawnKind.Typhon_Mimic;
            pawn.ChangeKind(pawnKind);
            pawn.SetFaction(FactionUtility.DefaultFactionFrom(pawnKind.defaultFactionType));
            pawn.Drawer.renderer.graphics.ResolveAllGraphics();
        }
    }
}
