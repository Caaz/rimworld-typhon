using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Typhon
{
    internal class TyphonUtility
	{
		public static Pawn GenerateMimic()
        {
			PawnKindDef mimicKind = TyphonDefOf.PawnKind.Typhon_Mimic;
			Pawn mimic = PawnGenerator.GeneratePawn(mimicKind, FactionUtility.DefaultFactionFrom(mimicKind.defaultFactionType));
			mimic.ageTracker.AgeBiologicalTicks = 0;
			mimic.ageTracker.AgeChronologicalTicks = 0;
			return mimic;
		}
		public static Pawn GetAttackableTarget(Pawn pawn, float distance = 2f)
		{
			foreach (Thing item in GenRadial.RadialDistinctThingsAround(pawn.Position, pawn.Map, distance, true))
				if (TyphonUtility.AcceptablePrey(pawn, item))
					return (Pawn)item;
			return null;
		}
		private static bool AcceptablePrey(Pawn hunter, Thing prey)
        {
			if (
				prey == null || 
				hunter == null || 
				prey.def.thingClass != typeof(Pawn)
			)
				return false;

            Pawn target = (Pawn)prey;

			return !(
				target.Dead ||
				!(target.RaceProps.FleshType == FleshTypeDefOf.Normal) ||
				!hunter.CanSee(target)
			);
        }
	}
}
