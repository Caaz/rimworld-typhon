using RimWorld;
using Verse;

namespace Typhon.AbilityComp
{
    public class MimicMatter : RimWorld.CompAbilityEffect
    {
        public new CompProperties_AbilityEffect Props => (CompProperties_AbilityEffect)props;
        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            IntVec3 position = parent.pawn.Position;
            Map map = parent.pawn.Map;
            parent.AddEffecterToMaintain(EffecterDefOf.Typhon_Effecter_PhantomShift.Spawn(position, map), position, 30);
            GenClamor.DoClamor(parent.pawn, position, Props.clamorRadius, Props.clamorType);
            MimickedMatter mimicMatter = MimickedMatter.MimicMatter(target.Thing.def, parent.pawn);
            mimicMatter.DoSpawn(position, map);
            // PawnFlyer pawnFlyer = PawnFlyer.MakeFlyer(ThingDefOf.PawnJumper, pawn, cell, verbProps.flightEffecterDef, verbProps.soundLanding, verbProps.flyWithCarriedThing);
        }

    }
}
