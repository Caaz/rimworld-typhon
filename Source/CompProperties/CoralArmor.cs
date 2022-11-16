using System.Collections.Generic;
using Verse;

namespace Typhon.CompProperties
{
    internal class CoralArmor : Verse.CompProperties
    {
        public List<GraphicData> graphicElements;
        public CoralArmor()
        {
            compClass = typeof(Comps.CoralArmor);
        }
    }
}