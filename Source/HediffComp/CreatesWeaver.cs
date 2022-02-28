using Verse;

namespace Typhon.HediffComp
{
    internal class CreatesWeaver : Verse.HediffComp
	{
		public override void Notify_PawnDied()
		{
			base.Notify_PawnDied();
			GenSpawn.Spawn(TyphonUtility.GenerateWeaver(), base.Pawn.Corpse.Position, base.Pawn.Corpse.Map);
			base.Pawn.Corpse.Destroy();
		}
    }
}
