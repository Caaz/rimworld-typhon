using RimWorld;
using Verse;

namespace Typhon
{
    internal class Projectile_Gloo : Bullet
	{
		protected override void Impact(Thing hitThing)
		{
			if (hitThing == null)
			{
				Building wall = (Building)ThingMaker.MakeThing(ThingDefOf.Wall, ThingDefOf.Steel);
				bool wouldWipe = GenSpawn.WouldWipeAnythingWith(Position, Rotation, ThingDefOf.Wall, Map, delegate(Thing thing) {
					return (thing.def.thingClass == typeof(Building));
				});
				if(!wouldWipe)
					GenSpawn.Spawn(wall, Position, Map, Rotation);
			}
			base.Impact(hitThing);
		}
	}
}
