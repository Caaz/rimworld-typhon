using System;
using System.Collections.Generic;
using Verse;

namespace Typhon.CompProperties
{
    internal class Hivemind : Verse.CompProperties
    {
        public string hivemindSignalTag = "Typhon.Hivemind";
        public Hivemind()
        {
            compClass = typeof(Comps.Hivemind);
        }
        public override IEnumerable<string> ConfigErrors(ThingDef parentDef)
        {
            if (!parentDef.receivesSignals)
                yield return "ThingDefs with Typhon.CompProperties_Hivemind component must have receivesSignals set to true!";
        }
    }
}