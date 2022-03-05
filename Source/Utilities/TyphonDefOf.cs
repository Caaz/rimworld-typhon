using RimWorld;
using Verse;

namespace Typhon.TyphonDefOf
{
#pragma warning disable 0649
    [DefOf]
    internal static class Thing
    {
        public static Verse.ThingDef Typhon_Mimic;
        public static Verse.ThingDef Typhon_Mimic_Hidden;
        public static Verse.ThingDef Typhon_Weaver_Race;
        static Thing()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ThingDefOf));
        }
    }
    [DefOf]
    internal static class Job
    {
        public static Verse.JobDef TyphonMimicMultiply;
        public static Verse.JobDef TyphonMimicBuilding;
        public static Verse.JobDef TyphonAttackPawn;
        public static Verse.JobDef TyphonCreateWeaver;
        static Job()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(JobDefOf));
        }
    }
    [DefOf]
    internal static class PawnKind
    {
        public static Verse.PawnKindDef Typhon_Mimic;
        public static Verse.PawnKindDef Typhon_Mimic_Hidden;
        public static Verse.PawnKindDef Typhon_Weaver;
        static PawnKind()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PawnKindDefOf));
        }
    }
    [DefOf]
    internal static class Faction
    {
        public static FactionDef Typhon;
        //public static FactionDef Typhon_Hidden;
        static Faction()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(FactionDefOf));
        }
    }
    [DefOf]
    internal static class FleshType
    {
        public static FleshTypeDef Typhon;
        static FleshType()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(FleshTypeDefOf));
        }
    }
    [DefOf]
    internal static class Hediff
    {
        public static HediffDef TyphonCreatesWeaver;
        static Hediff()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(HediffDefOf));
        }
    }
#pragma warning restore 0649
}
