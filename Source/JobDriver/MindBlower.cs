using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace Typhon.JobDriver
{
    public class MindBlower : Verse.AI.JobDriver
    {
        private Pawn Prey => (Pawn)job.GetTarget(TargetIndex.A).Thing;
        public override bool TryMakePreToilReservations(bool errorOnFailed) => true;
        protected override IEnumerable<Toil> MakeNewToils()
        {
            AddFailCondition((Func<bool>)delegate
            {
                return (Prey == null || Prey.Dead || !pawn.CanSee(Prey) || TyphonUtility.IsTyphon(Prey));
            });
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch).FailOnDespawnedOrNull(TargetIndex.A);
            yield return Toils_General.Do(Explode);
        }
        private void Explode()
        {
            IEnumerable<BodyPartRecord> parts = pawn.def.race.body.GetPartsWithTag(BodyPartTagDefOf.ConsciousnessSource);
            if (!parts.Any()) return;
            DamageInfo damage = new DamageInfo(DamageDefOf.Bomb, 100, 100, -1, pawn, parts.First());
            GenExplosion.DoExplosion(pawn.Position, pawn.Map, 1.9f, DamageDefOf.Bomb, null, 1, 1, SoundDefOf.Interact_Ignite);
            try
            {
                if (pawn.Dead) pawn.Corpse.TakeDamage(damage);
                else pawn.TakeDamage(damage);
            }
            catch { }
        }
    }
}