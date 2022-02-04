using RimWorld;

namespace Typhons.TyphonDefOf
{
    #pragma warning disable 0649
    [DefOf]
    internal static class Thing
    {
        public static Verse.ThingDef Typhon_Mimic;
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
        static PawnKind()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PawnKindDefOf));
        }
    }
    [DefOf]
    internal static class Faction
    {
        public static FactionDef Typhon;
        public static FactionDef Typhon_Hidden;
        static Faction()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(FactionDefOf));
        }
    }
#pragma warning restore 0649
}
