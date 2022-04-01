using Verse;

namespace Typhon.CompProperties
{
    internal class Reincarnate : Verse.CompProperties
    {
        public PawnKindDef pawnKind = null;
        public int amount = 0;
        public Reincarnate()
        {
            compClass = typeof(Comps.Reincarnate);
        }
    }
}
