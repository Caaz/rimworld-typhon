﻿using HarmonyLib;
using HugsLib;
using HugsLib.Settings;
using HugsLib.Utils;
using RimWorld;
using System;
using System.Collections.Generic;
using Verse;

namespace Typhon
{
    public class Mod : ModBase
    {
        internal static SettingHandle<bool> neuromodRetexture;
        public override void DefsLoaded()
        {
            CreateSettings();
            CreateRecipes();
            UpdateNeuromodTextures();
        }

        private void CreateSettings()
        {
            neuromodRetexture = Settings.GetHandle<bool>(
                "neuromodRetexture",
                "Retexture trainers",
                "Use a prey-themed texture for skilltrainers and psytrainers (Requires reload of save)",
                false);
            neuromodRetexture.ValueChanged += handle => UpdateNeuromodTextures();
        }
        private void CreateRecipes()
        {
            IngredientCount gold = new IngredientCount();
            gold.SetBaseCount(5);
            gold.filter.SetAllow(ThingDef.Named("Gold"), true);
            IngredientCount componentSpacer = new IngredientCount();
            componentSpacer.SetBaseCount(20);
            componentSpacer.filter.SetAllow(ThingDef.Named("ComponentSpacer"), true);
            IngredientCount typhonOrgan = new IngredientCount();
            typhonOrgan.SetBaseCount(20);
            typhonOrgan.filter.SetAllow(ThingDef.Named("TyphonOrgan"), true);
            SoundDef soundWorking = SoundDef.Named("RecipeMachining");
            ThingDef unfinishedThing = ThingDef.Named("UnfinishedComponent");
            EffecterDef effecterDef = DefDatabase<EffecterDef>.GetNamed("Cook");
            StatDef workSpeed = StatDef.Named("GeneralLaborSpeed");
            SkillRequirement skillRequirement = new SkillRequirement();
            skillRequirement.skill = DefDatabase<SkillDef>.GetNamed("Intellectual");
            skillRequirement.minLevel = 15;
            List<SkillRequirement> skills = new List<SkillRequirement>();
            skills.Add(skillRequirement);
            ThingDef fabricationBench = ThingDef.Named("FabricationBench");
            foreach (ThingDef thingDef in DefDatabase<ThingDef>.AllDefs)
            {
                try
                {
                    if (thingDef.thingCategories != null && thingDef.thingCategories.Contains(ThingCategoryDefOf.NeurotrainersPsycast))
                    {
                        CompProperties_Neurotrainer compDef = thingDef.CompDefFor<CompNeurotrainer>() as CompProperties_Neurotrainer;
                        if (compDef == null) continue;
                        RecipeDef recipeDef = new RecipeDef();
                        recipeDef.defName = "Typhon_Make_Psytrainer_" + compDef.ability.defName;
                        recipeDef.label = "make psytrainer (" + compDef.ability.label + ")";
                        recipeDef.ingredients.Add(gold);
                        recipeDef.ingredients.Add(componentSpacer);
                        recipeDef.ingredients.Add(typhonOrgan);
                        recipeDef.adjustedCount = 1;
                        recipeDef.workAmount = 2000;
                        recipeDef.soundWorking = soundWorking;
                        recipeDef.unfinishedThingDef = unfinishedThing;
                        recipeDef.effectWorking = effecterDef;
                        recipeDef.workSpeedStat = workSpeed;
                        recipeDef.description = "Make psytrainer from typhon organs, advanced components, and gold.";
                        recipeDef.skillRequirements = skills;
                        ThingDefCountClass product = new ThingDefCountClass();
                        product.count = 1;
                        product.thingDef = thingDef;
                        recipeDef.products.Add(product);
                        recipeDef.ResolveReferences();
                        InjectedDefHasher.GiveShortHashToDef(recipeDef, typeof(RecipeDef));
                        DefDatabase<RecipeDef>.Add(recipeDef);
                        fabricationBench.recipes.Add(recipeDef);
                    }
                }
                catch (Exception)
                {
                    Logger.Message("Failed to create trainer for " + thingDef);
                }
            }
        }
        private void UpdateNeuromodTextures()
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
