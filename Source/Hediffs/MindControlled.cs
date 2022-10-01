using RimWorld;
using Verse;

namespace Typhon.Hediffs
{
    internal class MindControlled : Verse.Hediff
    {
        public Faction originalFaction;
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref originalFaction, "originalFaction");
        }
        public override void PostMake()
        {
            base.PostMake();
            originalFaction = pawn.Faction;
        }
        public override void Notify_PawnPostApplyDamage(DamageInfo damageInfo, float amount)
        {
            pawn.SetFaction(originalFaction);
            severityInt = 0f;
        }
    }
}
