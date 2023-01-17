using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Typhon
{
    class MimickedMatter : Thing, IThingHolder
    {
        private ThingOwner<Thing> innerContainer;
        private Thing mimicking;
        private JobQueue jobQueue;
        private bool pawnWasDrafted;
        private int ticks;
        public Pawn Mimic
        {
            get
            {
                if (innerContainer.InnerListForReading.Count <= 0)
                {
                    return null;
                }
                return innerContainer.InnerListForReading[0] as Pawn;
            }
        }

        public MimickedMatter()
        {
            innerContainer = new ThingOwner<Thing>(this);
        }
        public void GetChildHolders(List<IThingHolder> outChildren) => ThingOwnerUtility.AppendThingHoldersFromThings(outChildren, GetDirectlyHeldThings());
        public ThingOwner GetDirectlyHeldThings() => innerContainer;
        public static MimickedMatter MimicMatter(ThingDef thingDef, Pawn pawn)
        {
            MimickedMatter mimickedMatter = (MimickedMatter)ThingMaker.MakeThing(ThingDefOf.Typhon_Thing_MimickedMatter);
            if (pawn.CurJob != null)
            {
                pawn.jobs.SuspendCurrentJob(JobCondition.InterruptForced);
            }
            mimickedMatter.jobQueue = pawn.jobs.CaptureAndClearJobQueue();
            mimickedMatter.pawnWasDrafted = pawn.Drafted;
            pawn.DeSpawn(DestroyMode.WillReplace);
            if (!mimickedMatter.innerContainer.TryAdd(pawn))
            {
                Log.Error("Could not add " + pawn.ToStringSafe() + " to mimicked thing.");
                pawn.Destroy();
            }
            mimickedMatter.mimicking = ThingMaker.MakeThing(thingDef);
            return mimickedMatter;
        }
        public override void Tick()
        {
            innerContainer.ThingOwnerTick();
            if (ticks % 60 == 0)
                if (mimicking == null || mimicking.DestroyedOrNull() || mimicking.HitPoints != mimicking.MaxHitPoints)
                    UnMimic();
            ticks++;
        }
        protected virtual void UnMimic()
        {
            Pawn mimic = Mimic;
            innerContainer.TryDrop(mimic, base.Position, mimic.MapHeld, ThingPlaceMode.Direct, out var lastResultingThing, null, null, playDropSound: false);
            if (mimic.drafter != null)
                mimic.drafter.Drafted = pawnWasDrafted;
            // if (jobQueue != null)
            //     mimic.jobs.RestoreCapturedJobs(jobQueue);
            mimic.jobs.CheckForJobOverride();
            mimicking.Destroy();
            Destroy();
        }
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Deep.Look(ref innerContainer, "innerContainer", this);
            Scribe_Deep.Look(ref mimicking, "mimicking", this);
            Scribe_Deep.Look(ref jobQueue, "jobQueue", this);
            Scribe_Values.Look(ref ticks, "ticks");
            Scribe_Values.Look(ref pawnWasDrafted, "pawnWasDrafted");
        }
        public void DoSpawn(IntVec3 position, Map map)
        {
            GenSpawn.Spawn(this, position, map);
            GenSpawn.Spawn(this.mimicking, position, map);
        }
    }
}