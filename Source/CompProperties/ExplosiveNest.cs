using Verse;

namespace Typhon.CompProperties
{
    internal class ExplosiveNest : Verse.CompProperties
    {
        public PawnKindDef pawnKind = null;
        public int amount = 0;
        public float radius = 0;
        public ExplosiveNest()
        {
            compClass = typeof(Comps.ExplosiveNest);
        }
    }
}
