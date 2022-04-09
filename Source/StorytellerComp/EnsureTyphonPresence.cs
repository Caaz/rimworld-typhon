using System.Collections.Generic;
using RimWorld;

namespace Typhon.StorytellerComp
{
    internal class EnsureTyphonPresence : RimWorld.StorytellerComp
    {
        public override IEnumerable<FiringIncident> MakeIntervalIncidents(IIncidentTarget target)
        {
            IncidentDef shipChunkDrop = TyphonDefOf.Incident.MimicCrash;
            IncidentParms parms = GenerateParms(shipChunkDrop.category, target);
            if (shipChunkDrop.Worker.CanFireNow(parms))
            {
                yield return new FiringIncident(shipChunkDrop, this, parms);
            }
        }
    }
}
