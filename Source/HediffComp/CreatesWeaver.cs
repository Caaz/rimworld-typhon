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
			CompHivemind comp = weaver.GetComp<CompHivemind>();
			if (comp != null)
				comp.SendSignal_CreatedWeaver(weaver);
		}
    }
}
