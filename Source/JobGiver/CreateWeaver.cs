using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Typhon.JobGiver
{
    internal class CreateWeaver : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            List<Pawn> mimics = GetMimics(pawn.Position, pawn.Map, 7f);
            return (mimics.Count > 7) ? JobMaker.MakeJob(TyphonDefOf.Job.TyphonCreateWeaver) : null;
        }
        private List<Pawn> GetMimics(IntVec3 position, Map map, float radius)
        {
            var list = new List<Pawn>();
            foreach (Thing thing in GenRadial.RadialDistinctThingsAround(position, map, radius, true))
            {
                if (!(thing.def == TyphonDefOf.Thing.Typhon_Mimic || thing.def == TyphonDefOf.Thing.Typhon_Mimic_Hidden)) continue;
                list.Add(thing as Pawn);
            }
            return list;
        }
    }
}
