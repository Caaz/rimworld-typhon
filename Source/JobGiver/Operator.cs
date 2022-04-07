using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Typhon.JobGiver
{
    internal class Operator : ThinkNode_JobGiver
    {
        private float operatorRange = 30f;
        protected override Job TryGiveJob(Pawn pawn)
        {
            return MedicalJobGiver(pawn);
        }
        private Job MedicalJobGiver(Pawn medicalOperator)
        {
            if (medicalOperator.inventory.innerContainer.EnumerableNullOrEmpty())
            {
                Thing medicine = GenClosest.ClosestThingReachable(medicalOperator.Position, medicalOperator.Map, ThingRequest.ForDef(ThingDefOf.MedicineIndustrial), PathEndMode.OnCell, TraverseParms.For(medicalOperator));
                if(medicine == null
                    || medicine.IsForbidden(medicalOperator)
                ) return null;
                Job takeMedicine = JobMaker.MakeJob(JobDefOf.TakeCountToInventory, medicine);
                takeMedicine.count = 1;
                return takeMedicine;
            }
            else
            {
                foreach (Pawn pawn in medicalOperator.Map.mapPawns.FreeColonists)
                {
                    if(pawn.health.HasHediffsNeedingTend()
                        && medicalOperator.Position.DistanceTo(pawn.Position) < operatorRange
                        && medicalOperator.CanReserveAndReach(pawn, PathEndMode.Touch, Danger.Deadly)
                    )
                    {
                        Job tend = JobMaker.MakeJob(TyphonDefOf.Job.TyphonOperatorMedical, pawn, medicalOperator.inventory.innerContainer.FirstOrFallback());
                        return tend;
                    }
                }
            }
            return null;
        }
    }
}