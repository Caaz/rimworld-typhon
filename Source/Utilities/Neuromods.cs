using RimWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using Verse;

namespace Typhon
{
    internal class Neuromods : IEnumerable<ThingDef>
	{
		public IEnumerator<ThingDef> GetEnumerator()
		{
			foreach (ThingDef thingDef in DefDatabase<ThingDef>.AllDefs)
			{
				if (
					thingDef.thingCategories.Contains(ThingCategoryDefOf.NeurotrainersPsycast)
					|| thingDef.thingCategories.Contains(ThingCategoryDefOf.NeurotrainersSkill)
					)
					yield return thingDef;
			}
			yield break;
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
    }
}
