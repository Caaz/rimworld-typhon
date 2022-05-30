using Verse;
using RimWorld;

namespace Typhon.DeathActionWorker
{
    internal class ToxicExplosion : DeathActionWorker_SmallExplosion
    {
        public override RulePackDef DeathRules => RulePackDefOf.Transition_DiedExplosive;
        public override bool DangerousInMelee => true;
        public override void PawnDied(Corpse corpse)
        {
            GenExplosion.DoExplosion(corpse.Position, corpse.Map, 1.9f, TyphonDefOf.Damage.Typhon_ToxicExplosion, corpse.InnerPawn, 1, 1, SoundDefOf.Interact_Ignite);
        }
    }
}