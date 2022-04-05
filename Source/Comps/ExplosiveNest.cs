using Verse;
using Verse.AI;

namespace Typhon.Comps
{
    internal class ExplosiveNest : ThingComp
    {
        private CompProperties.ExplosiveNest Props => (CompProperties.ExplosiveNest)props;
        public override void PostDestroy(DestroyMode mode, Map previousMap)
        {
            if(Props.amount > 0)
                for (int i = 0; i < Props.amount - 1; i++)
                    GenSpawn.Spawn(TyphonUtility.GenerateTyphon(Props.pawnKind), parent.Position, previousMap);
        }
        public override void CompTickRare()
        {
            if (GenClosest.ClosestThingReachable(
                parent.Position, 
                parent.Map, 
                ThingRequest.ForGroup(ThingRequestGroup.Pawn), 
                PathEndMode.OnCell, 
                TraverseParms.For(TraverseMode.NoPassClosedDoors), 
                Props.radius
            ) != null)
                parent.Destroy();
        }
    }
}
