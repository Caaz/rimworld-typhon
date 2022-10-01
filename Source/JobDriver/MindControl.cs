using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace Typhon.JobDriver
{
    internal class MindControl : Verse.AI.JobDriver
    {
        private Pawn Prey => (Pawn)job.GetTarget(TargetIndex.A).Thing;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }
        protected override IEnumerable<Toil> MakeNewToils()
        {
            AddFailCondition((Func<bool>)delegate
            {
                return (Prey == null || Prey.Dead || !pawn.CanSee(Prey));
            });
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch).FailOnDespawnedOrNull(TargetIndex.A);
            yield return Toils_General.Do(ControlPawn);
        }
        private void ControlPawn()
        {
            IEnumerable<BodyPartRecord> parts = Prey.def.race.body.GetPartsWithTag(BodyPartTagDefOf.ConsciousnessSource);
            if (!parts.Any()) return;
            BodyPartRecord brain = parts.First();

            Hediff hediff = HediffMaker.MakeHediff(TyphonDefOf.Hediff.TyphonMindControlled, Prey, brain);
            Prey.health.AddHediff(hediff, brain);
            Prey.SetFaction(pawn.Faction);
        }
    }
}
