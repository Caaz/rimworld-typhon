using System;
using Verse;

namespace Typhon
{
    internal class CompProperties_Reincarnate : CompProperties
    {
        public PawnKindDef pawnKind = null;
        public int amount = 0;
        public CompProperties_Reincarnate()
        {
            compClass = typeof(CompReincarnate);
        }
    }
}
