using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Typhon.IncidentWorkers
{
    internal class Herd : IncidentWorker
    {
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            if (!PawnsArrivalModeDefOf.EdgeWalkIn.Worker.TryResolveRaidSpawnCenter(parms)) return false;
            float percent = parms.points / 10000;

            List<Pawn> typhon = new List<Pawn>();
            for (int i = 0; i < (percent * 20); i++)
            {
                typhon.Add(TyphonUtility.GenerateMimic());
                if ((i > 2) && (i % 2 == 0))
                    typhon.Add(TyphonUtility.GenerateWeaver());
            }

            PawnsArrivalModeDefOf.EdgeWalkIn.Worker.Arrive(typhon, parms);
            SendStandardLetter(parms, new TargetInfo(typhon[0].Position, map));
            return true;
        }
    }
}
