using RimWorld;
using Verse;
using Verse.AI;

namespace Typhon.Comps
{
    internal class Hivemind : ThingComp
	{
        private CompProperties.Hivemind Props => (CompProperties.Hivemind)props;
		public override void Notify_SignalReceived(Signal signal)
		{
			Pawn typhon = parent as Pawn;
			if (!AcceptingSignals(typhon)) return;

			string prefix = Props.hivemindSignalTag+".";
			if (!signal.tag.StartsWith(prefix)) return;

			Pawn sender = signal.args.GetArg<Pawn>("SOURCE");
			if (
				(sender == null)
				|| (sender == typhon)
				|| (sender.Position.DistanceTo(typhon.Position) > 10f)
			) return;
			if (signal.tag == prefix + "Attack")
				RecieveSignal_Attack(signal, typhon);
			else if (signal.tag == prefix + "CreatedWeaver")
				RecieveSignal_CreatedWeaver(typhon);
		}
		private bool AcceptingSignals(Pawn typhon)
		{
			return !(
				(typhon.CurJobDef == JobDefOf.AttackMelee)
				|| (typhon.CurJobDef == JobDefOf.AttackStatic)
				|| (typhon.CurJobDef == TyphonDefOf.Job.TyphonCreateWeaver)
			);
		}
		private void RecieveSignal_Attack(Signal signal, Pawn typhon)
		{
			Pawn target = signal.args.GetArg<Pawn>("TARGET");
			if (target == null) return;
			bool skipValidation = (target.CurJob != null && target.CurJob.def == TyphonDefOf.Job.TyphonCreateWeaver);
			if (!skipValidation && !TyphonUtility.AcceptablePrey(typhon, target)) return;
			Job attackJob = TyphonUtility.AttackJob(typhon, target);
			if (attackJob == null) return;
			typhon.jobs.StopAll();
            typhon.jobs.StartJob(attackJob);
		}
		public void SendSignal_Attack(Pawn source, Pawn target)
		{
			Signal attackSignal = new Signal(Props.hivemindSignalTag + ".Attack", new NamedArgument(source, "SOURCE"), new NamedArgument(target, "TARGET"));
			Find.SignalManager.SendSignal(attackSignal);
		}
		private void RecieveSignal_CreatedWeaver(Pawn typhon)
		{
			Hediff weaverMaker = typhon.health.hediffSet.GetFirstHediffOfDef(TyphonDefOf.Hediff.TyphonCreatesWeaver);
			if(weaverMaker != null) typhon.health.RemoveHediff(weaverMaker);
		}
		public void SendSignal_CreatedWeaver(Pawn source)
		{
			Signal attackSignal = new Signal(Props.hivemindSignalTag + ".CreatedWeaver", new NamedArgument(source, "SOURCE"));
			Find.SignalManager.SendSignal(attackSignal);
		}
	}
}
