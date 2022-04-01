using Verse;

namespace Typhon.Comps
{
    internal class Reincarnate : ThingComp
    {
        private CompProperties.Reincarnate Props => (CompProperties.Reincarnate)props;
        public override void PostDestroy(DestroyMode mode, Map previousMap)
        {
            if(Props.amount > 0)
                for (int i = 0; i < Props.amount - 1; i++)
                    GenSpawn.Spawn(TyphonUtility.GenerateTyphon(Props.pawnKind), parent.Position, previousMap);
        }
    }
}
