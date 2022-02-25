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
			string prefix = Props.hivemindSignalTag+".";
			Pawn sender = signal.args.GetArg<Pawn>("SOURCE");
			if (
				(sender == null)
				|| (sender == typhon)
				|| (sender.Position.DistanceTo(typhon.Position) > 10f)
			) return;
			if (signal.tag == prefix+"Attack")
				RecievedAttackSignal(signal, typhon);
		}
		private void RecievedAttackSignal(Signal signal, Pawn typhon)
		{
			Log.Message(typhon + " recieved attack signal for " + signal);
			if (typhon.CurJobDef == TyphonDefOf.Job.TyphonAttackPawn) return;
			Pawn target = signal.args.GetArg<Pawn>("TARGET");
			if (target == null) return;
			Log.Message(typhon + " attacking "+ target);
			Job attackJob = TyphonUtility.AttackJob(typhon, target);
			if (attackJob == null) return;
			typhon.jobs.StopAll();
            typhon.jobs.StartJob(attackJob);
        }
		public void SendAttackSignal(Pawn source, Pawn target)
        {
			Log.Message("Sending attack signal for " + target);
			Signal attackSignal = new Signal(Props.hivemindSignalTag + ".Attack", new NamedArgument(source, "SOURCE"), new NamedArgument(target, "TARGET"));
            Find.SignalManager.SendSignal(attackSignal);
        }
	}
}
