using System;
using System.Collections.Generic;
using Verse;

namespace Typhon
{
    internal class CompProperties_Hivemind : CompProperties
    {
        public string hivemindSignalTag = "Typhon.Hivemind";
        public CompProperties_Hivemind()
        {
            compClass = typeof(CompHivemind);
        }
        public override IEnumerable<string> ConfigErrors(ThingDef parentDef)
        {
            if (!parentDef.receivesSignals)
                yield return "ThingDefs with Typhon.CompProperties_Hivemind component must have receivesSignals set to true!";
        }
    }
}