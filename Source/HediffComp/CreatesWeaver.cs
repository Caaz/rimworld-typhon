using Verse;

namespace Typhon.HediffComp
{
    internal class CreatesWeaver : Verse.HediffComp
	{
		public override void Notify_PawnDied()
		{
			base.Notify_PawnDied();
			Pawn weaver = TyphonUtility.GenerateWeaver();
			GenSpawn.Spawn(weaver, base.Pawn.Corpse.Position, base.Pawn.Corpse.Map);
			base.Pawn.Corpse.Destroy();
			Comps.Hivemind comp = weaver.GetComp<Comps.Hivemind>();
			if (comp != null)
				comp.SendSignal_CreatedWeaver(weaver);
		}
    }
}
