using RimWorld;
using Verse;
using Verse.AI;

namespace Typhon
{
    internal class CompHivemind : ThingComp
	{
        private CompProperties_Hivemind Props => (CompProperties_Hivemind)props;
		public override void Notify_SignalReceived(Signal signal)
		{
			Pawn typhon = parent as Pawn;
			if (!AcceptingSignals(typhon)) return;

			string prefix = Props.hivemindSignalTag+".";
			Pawn sender = signal.args.GetArg<Pawn>("SOURCE");
			if (
				(sender == null)
				|| (sender == typhon)
				|| (sender.Position.DistanceTo(typhon.Position) > 10f)
			) return;
			if (signal.tag == prefix + "Attack")
				RecievedAttackSignal(signal, typhon);
		}
		private bool AcceptingSignals(Pawn typhon)
		{
			return !(
				(typhon.CurJobDef == TyphonDefOf.Job.TyphonAttackPawn)
				|| (typhon.CurJobDef == TyphonDefOf.Job.TyphonCreateWeaver)
			);
		}
		private void RecievedAttackSignal(Signal signal, Pawn typhon)
		{
			Pawn target = signal.args.GetArg<Pawn>("TARGET");
			if (target == null) return;
			bool skipValidation = (target.CurJob != null && target.CurJob.def == TyphonDefOf.Job.TyphonCreateWeaver);
			Job attackJob = TyphonUtility.AttackJob(typhon, target, skipValidation);
			if (attackJob == null) return;
			typhon.jobs.StopAll();
            typhon.jobs.StartJob(attackJob);
        }
		public void SendAttackSignal(Pawn source, Pawn target)
        {
			Signal attackSignal = new Signal(Props.hivemindSignalTag + ".Attack", new NamedArgument(source, "SOURCE"), new NamedArgument(target, "TARGET"));
            Find.SignalManager.SendSignal(attackSignal);
        }
	}
}
