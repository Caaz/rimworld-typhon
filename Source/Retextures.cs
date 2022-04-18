using HugsLib.Settings;
using RimWorld;
using System;
using Verse;

namespace Typhon
{
    internal static class Retextures
    {
        internal static SettingHandle<bool> neuromodRetexture;
        public static void CreateSettings(ModSettingsPack settings)
        {
            neuromodRetexture = settings.GetHandle<bool>(
                "neuromodRetexture",
                "Retexture trainers",
                "Use a prey-themed texture for skilltrainers and psytrainers (Requires reload of save)",
                false);
            neuromodRetexture.ValueChanged += handle => UpdateNeuromodTextures();
            UpdateNeuromodTextures();
        }
        private static void UpdateNeuromodTextures()
        {
            GraphicData graphicData = new GraphicData
            {
                texPath = (neuromodRetexture) ? "Things/Item/Typhon_Neuromod" : "Things/Item/Special/MechSerumNeurotrainer",
                graphicClass = typeof(Graphic_Single)
            };

            foreach (ThingDef thingDef in DefDatabase<ThingDef>.AllDefs)
            {
                try
                {
                    if (
                        thingDef.thingCategories.Contains(ThingCategoryDefOf.NeurotrainersPsycast)
                        || thingDef.thingCategories.Contains(ThingCategoryDefOf.NeurotrainersSkill)
                    )
                        thingDef.graphicData = graphicData;
                }
                catch (Exception) { }
            }
        }
    }
}
