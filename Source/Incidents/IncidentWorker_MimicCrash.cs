using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Typhon
{
    internal class IncidentWorker_MimicCrash : IncidentWorker
    {
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            List<Thing> things = new List<Thing>();
            things.Add(TyphonUtility.GenerateMimic());
            IntVec3 intVec = DropCellFinder.RandomDropSpot(map);
            DropPodUtility.DropThingsNear(intVec, map, things, 110, canInstaDropDuringInit: false, leaveSlag: true);
            SendStandardLetter(parms, new TargetInfo(intVec, map));
            return true;
        }
    }
}
