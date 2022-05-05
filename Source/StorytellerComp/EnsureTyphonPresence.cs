using System.Collections.Generic;
using RimWorld;
using Verse;

namespace Typhon.StorytellerComp
{
    internal class EnsureTyphonPresence : RimWorld.StorytellerComp
    {
        public override IEnumerable<FiringIncident> MakeIntervalIncidents(IIncidentTarget target)
        {
            Map map = target as Map;
            if (
                !Rand.MTBEventOccurs(.5f, 60000, 1000f) 
                || (map == null)
                || HasMimic(map)
            ) yield break;

            IncidentDef mimicCrash = TyphonDefOf.Incident.MimicCrash;
            IncidentParms parms = GenerateParms(mimicCrash.category, target);
            if (mimicCrash.Worker.CanFireNow(parms))
                yield return new FiringIncident(mimicCrash, this, parms);
        }
        private bool HasMimic(Map map)
        {
            return map.listerThings.ThingsOfDef(TyphonDefOf.Thing.Typhon_Mimic).Any()
                || map.listerThings.ThingsOfDef(TyphonDefOf.Thing.Typhon_Mimic_Hidden).Any();
        }
    }
}
