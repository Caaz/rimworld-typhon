using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace Typhon.JobDriver
{
    internal class CreateWeaver : Verse.AI.JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            yield return Wait();
        }
        private Toil Wait()
        {
            Toil wait = Toils_General.Wait(100);
            wait.AddPreInitAction(delegate
            {
                CompHivemind comp = pawn.GetComp<CompHivemind>();
                if (comp == null) EndJobWith(JobCondition.Errored);
                pawn.health.AddHediff(TyphonDefOf.Hediff.TyphonCreatesWeaver);
                comp.SendSignal_Attack(pawn, pawn);
            });
            return wait;
        }
    }
}
