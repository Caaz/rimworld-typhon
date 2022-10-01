using RimWorld;
using Verse;

namespace Typhon.TyphonDefOf
{
#pragma warning disable 0649
    [DefOf]
    internal static class Damage
    {
        public static Verse.DamageDef Typhon_ToxicExplosion;
        static Damage()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(DamageDefOf));
        }
    }
    [DefOf]
    internal static class Thing
    {
        public static Verse.ThingDef Typhon_Mimic;
        public static Verse.ThingDef Typhon_Mimic_Hidden;
        public static Verse.ThingDef Typhon_Greater_Mimic_Race;
        public static Verse.ThingDef Typhon_Greater_Mimic_Hidden_Race;
        public static Verse.ThingDef Typhon_Weaver_Race;
        public static Verse.ThingDef Typhon_Phantom_Race;
        public static Verse.ThingDef Typhon_Cystoid_Race;
        public static Verse.ThingDef Typhon_Telepath_Race;
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
        public static Verse.JobDef TyphonCreateWeaver;
        public static Verse.JobDef TyphonCreatePhantom;
        public static Verse.JobDef TyphonCystoidAttack;
        public static Verse.JobDef TyphonCreateCystoid;
        public static Verse.JobDef TyphonMindControl;
        public static Verse.JobDef TyphonMindBlower;
        public static Verse.JobDef TyphonOperatorMedical;
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
        public static Verse.PawnKindDef Typhon_Greater_Mimic;
        public static Verse.PawnKindDef Typhon_Greater_Mimic_Hidden;
        public static Verse.PawnKindDef Typhon_Weaver;
        public static Verse.PawnKindDef Typhon_Phantom;
        public static Verse.PawnKindDef Typhon_Cystoid;
        static PawnKind()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PawnKindDefOf));
        }
    }
    [DefOf]
    internal static class Faction
    {
        public static FactionDef Typhon;
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
        public static HediffDef TyphonMindControlled;
        static Hediff()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(HediffDefOf));
        }
    }
    [DefOf]
    internal static class Incident
    {
        public static IncidentDef MimicCrash;
        static Incident()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(IncidentDefOf));
        }
    }
#pragma warning restore 0649
}
