using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace Typhon.HistoryAutoRecorderWorker
{
    public class Goodwill : RimWorld.HistoryAutoRecorderWorker
    {
        public override float PullRecord()
        {
            float average = 0;
            List<Faction> factions = Find.FactionManager.AllFactionsListForReading;
            foreach (Faction faction in factions)
            {
                if (
                    (faction.IsPlayer)
                    || (faction.Hidden)
                    || (faction.def.permanentEnemy)
                ) continue;
                average += faction.PlayerGoodwill;
            }
            average /= factions.Count;
            return Math.Max(average, 0);
        }
    }
}