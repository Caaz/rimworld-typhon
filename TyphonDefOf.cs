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
        static Job()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(JobDefOf));
        }
    }
    [DefOf]
    internal static class PawnKind
    {
        public static Verse.PawnKindDef Typhon_Mimic;
        static PawnKind()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PawnKindDefOf));
        }
    }
#pragma warning restore 0649
}
