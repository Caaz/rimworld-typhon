using RimWorld;
using Verse;

#pragma warning disable 0649
namespace Typhon
{
    [DefOf]
    internal static class ThingDefOf
    {
        public static Verse.ThingDef Typhon_Thing_Phantom;
        public static Verse.ThingDef Typhon_Thing_MimickedMatter;
        static ThingDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(RimWorld.ThingDefOf));
    }

    [DefOf]
    internal static class FleshTypeDefOf
    {
        public static FleshTypeDef Typhon_FleshType_Typhon;
        static FleshTypeDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(RimWorld.FleshTypeDefOf));
    }

    [DefOf]
    internal static class EffecterDefOf
    {
        public static EffecterDef Typhon_Effecter_PhantomShift;
        static EffecterDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(RimWorld.EffecterDefOf));
    }
    [DefOf]
    internal static class AbilityDefOf
    {
        public static AbilityDef Typhon_Ability_MimicMatter;
        static AbilityDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(RimWorld.AbilityDefOf));
    }
}
#pragma warning restore 0649