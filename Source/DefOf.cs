using RimWorld;

#pragma warning disable 0649
namespace Typhon
{
    [DefOf]
    internal static class ThingDefOf
    {
        public static Verse.ThingDef Typhon_Thing_Phantom;
        static ThingDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(RimWorld.ThingDefOf));
    }

    [DefOf]
    internal static class FleshTypeDefOf
    {
        public static FleshTypeDef Typhon_FleshType_Typhon;
        static FleshTypeDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(RimWorld.FleshTypeDefOf));
    }
}
#pragma warning restore 0649