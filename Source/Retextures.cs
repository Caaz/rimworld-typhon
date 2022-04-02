using HugsLib.Settings;
using RimWorld;
using System;
using System.Collections.Generic;
using Verse;

namespace Typhon
{
    internal static class Retextures
    {
        internal static List<string> retextureTargets = new List<string>(){
            "Gun_Autopistol",
            "Weapon_GrenadeEMP",
            "Gun_PumpShotgun",
            "Proj_GrenadeEMP"
        };
        // defName -> texPath
        internal static Dictionary<string, string> originalTextures = new Dictionary<string, string>();
        // defName -> handle
        internal static Dictionary<string, SettingHandle<bool>> optionalRetextures = new Dictionary<string, SettingHandle<bool>>();
        internal static SettingHandle<bool> neuromodRetexture;
        public static void Initialize()
        {
            foreach (string defName in retextureTargets)
            {
                ThingDef def = DefDatabase<ThingDef>.GetNamedSilentFail(defName);
                if (def == null)
                {
                    Log.Message("[Typhon] Couldn't find a def with defName " + defName + " for optional retextures.");
                    continue;
                }
                originalTextures.Add(defName, def.graphicData.texPath);
            }
        }
        public static void CreateSettings(ModSettingsPack settings)
        {
            foreach (string defName in originalTextures.Keys)
            {
                ThingDef def = ThingDef.Named(defName);
                SettingHandle<bool> handle = settings.GetHandle<bool>(
                    "retexture_" + def.defName,
                    "Retexture " + def.label,
                    "Use a prey-themed texture (Requires reload of save)",
                    false);
                handle.ValueChanged += h => HandleRetexture(h as SettingHandle<bool>);
                optionalRetextures.Add(defName, handle);
                HandleRetexture(handle);
            }
            neuromodRetexture = settings.GetHandle<bool>(
                "neuromodRetexture",
                "Retexture trainers",
                "Use a prey-themed texture for skilltrainers and psytrainers (Requires reload of save)",
                false);
            neuromodRetexture.ValueChanged += handle => UpdateNeuromodTextures();
        }
        private static void HandleRetexture(SettingHandle<bool> handle)
        {
            string name = handle.Name;
            var defName = name.Substring(name.IndexOf("_") + 1);
            ThingDef def = ThingDef.Named(defName);
            string newTexture = originalTextures.TryGetValue(defName);
            if (handle) newTexture = "Typhon/Retextures/" + newTexture;
            GraphicData graphicData = new GraphicData
            {
                texPath = newTexture,
                graphicClass = typeof(Graphic_Single)
            };
            def.graphicData = graphicData;
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
