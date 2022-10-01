using RimWorld;
using Verse;

namespace Typhon.Hediffs
{
    internal class MindControlled : Verse.Hediff
    {
        public Faction originalFaction;
        public Pawn mindController;
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref originalFaction, "originalFaction");
            Scribe_Values.Look(ref mindController, "mindController");
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
