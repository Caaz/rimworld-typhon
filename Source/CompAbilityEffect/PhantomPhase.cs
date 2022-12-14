using RimWorld;
using Verse;

namespace Typhon.CompAbilityEffect
{
    public class PhantomPhase : RimWorld.CompAbilityEffect
    {

        public new CompProperties_AbilityTeleport Props => (CompProperties_AbilityTeleport)props;
        public static string SkipUsedSignalTag = "CompAbilityEffect.SkipUsed";
        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            parent.AddEffecterToMaintain(EffecterDefOf.Skip_ExitNoDelay.Spawn(target.Cell, parent.pawn.Map), target.Cell, 60);
            parent.pawn.Position = target.Cell;
            parent.pawn.Notify_Teleported();
            Find.SignalManager.SendSignal(new Signal(SkipUsedSignalTag, target.Named("POSITION"), parent.pawn.Named("SUBJECT")));
            GenClamor.DoClamor(parent.pawn, target.Cell, Props.destClamorRadius, Props.destClamorType);
        }
    }
}
