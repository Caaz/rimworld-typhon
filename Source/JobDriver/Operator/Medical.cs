using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Typhon.JobDriver.Operator
{
    internal class Medical : Verse.AI.JobDriver
    {
        protected Pawn Deliveree => job.targetA.Pawn;
		protected Thing MedicineUsed => job.targetB.Thing;

		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			if (Deliveree != pawn && !pawn.Reserve(Deliveree, job, 1, -1, null, errorOnFailed))
			{
				return false;
			}
			return true;
		}

        protected override IEnumerable<Toil> MakeNewToils()
        {
			this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
			this.FailOn(delegate
			{
				if (MedicineUsed != null && pawn.Faction == Faction.OfPlayer && Deliveree.playerSettings != null && !Deliveree.playerSettings.medCare.AllowsMedicine(MedicineUsed.def))
				{
					return true;
				}
				return (pawn == Deliveree && pawn.Faction == Faction.OfPlayer && pawn.playerSettings != null && !pawn.playerSettings.selfTend) ? true : false;
			});
			AddEndCondition(delegate
			{
				if (pawn.Faction == Faction.OfPlayer && HealthAIUtility.ShouldBeTendedNowByPlayer(Deliveree))
				{
					return JobCondition.Ongoing;
				}
				return ((job.playerForced || pawn.Faction != Faction.OfPlayer) && Deliveree.health.HasHediffsNeedingTend()) ? JobCondition.Ongoing : JobCondition.Succeeded;
			});
			this.FailOnAggroMentalState(TargetIndex.A);
			PathEndMode interactionCell = PathEndMode.None;
			if (Deliveree == pawn)
			{
				interactionCell = PathEndMode.OnCell;
			}
			else if (Deliveree.InBed())
			{
				interactionCell = PathEndMode.InteractionCell;
			}
			else if (Deliveree != pawn)
			{
				interactionCell = PathEndMode.ClosestTouch;
			}
			Toil gotoToil = Toils_Goto.GotoThing(TargetIndex.A, interactionCell);
			yield return gotoToil;
			int ticks = (int)(1f / pawn.GetStatValue(StatDefOf.MedicalTendSpeed) * 600f);
			Toil waitToil;
			if (!job.draftedTend)
			{
				waitToil = Toils_General.Wait(ticks);
			}
			else
			{
				waitToil = Toils_General.WaitWith(TargetIndex.A, ticks, useProgressBar: false, maintainPosture: true);
				waitToil.AddFinishAction(delegate
				{
					if (Deliveree != null && Deliveree != pawn && Deliveree.CurJob != null && (Deliveree.CurJob.def == JobDefOf.Wait || Deliveree.CurJob.def == JobDefOf.Wait_MaintainPosture))
					{
						Deliveree.jobs.EndCurrentJob(JobCondition.InterruptForced);
					}
				});
			}
			waitToil.FailOnCannotTouch(TargetIndex.A, interactionCell).WithProgressBarToilDelay(TargetIndex.A).PlaySustainerOrSound(SoundDefOf.Interact_Tend);
			waitToil.activeSkill = () => SkillDefOf.Medicine;
			waitToil.handlingFacing = true;
			waitToil.tickAction = delegate
			{
				if (pawn == Deliveree && pawn.Faction != Faction.OfPlayer && pawn.IsHashIntervalTick(100) && !pawn.Position.Fogged(pawn.Map))
				{
					FleckMaker.ThrowMetaIcon(pawn.Position, pawn.Map, FleckDefOf.HealingCross);
				}
				if (pawn != Deliveree)
				{
					pawn.rotationTracker.FaceTarget(Deliveree);
				}
			};
			yield return Toils_Tend.PickupMedicine(TargetIndex.B, Deliveree).FailOnDestroyedOrNull(TargetIndex.B);
			yield return waitToil;
			yield return FinalizeTend(Deliveree);
			yield return Toils_Jump.Jump(gotoToil);
		}
		public static Toil FinalizeTend(Pawn patient)
		{
			Toil toil = new Toil();
			toil.initAction = delegate
			{
				Pawn actor = toil.actor;
				Medicine medicine = (Medicine)actor.CurJob.targetB.Thing;
				TendUtility.DoTend(actor, patient, medicine);
				if (medicine != null && medicine.Destroyed)
				{
					actor.CurJob.SetTarget(TargetIndex.B, LocalTargetInfo.Invalid);
				}
				if (toil.actor.CurJob.endAfterTendedOnce)
				{
					actor.jobs.EndCurrentJob(JobCondition.Succeeded);
				}
			};
			toil.defaultCompleteMode = ToilCompleteMode.Instant;
			return toil;
		}
	}
}
