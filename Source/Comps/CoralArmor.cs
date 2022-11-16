using Verse;
using Verse.AI;
using RimWorld;
using UnityEngine;

namespace Typhon.Comps
{
    internal class CoralArmor : ThingComp
    {
        private CompProperties.CoralArmor Props => (CompProperties.CoralArmor)props;
        bool enabled = true;
        public override void PostPreApplyDamage(DamageInfo dinfo, out bool absorbed)
        {
            base.PostPreApplyDamage(dinfo, out absorbed);
            enabled = false;
            absorbed = true;
        }
        public override void CompTickLong()
        {
            base.CompTickLong();
            enabled = true;
        }
        public override void PostDraw()
        {
            base.PostDraw();
            if (enabled)
            {
                int frame = (int)(Time.time % 3.0);
                CompProperties.CoralArmor props = Props;
                Vector3 drawPos = parent.DrawPos;
                drawPos.y += 0.04054054f;
                props.graphicElements[frame].Graphic.Draw(drawPos, parent.Rotation, (Thing)this.parent);
            }
        }
    }
}