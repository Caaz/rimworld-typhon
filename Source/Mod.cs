using HugsLib;
using HugsLib.Settings;
using System;
using Verse;

namespace Typhon
{
    public class Mod : ModBase
    {
        public override string ModIdentifier => "caaz.typhon";
		internal static SettingHandle<bool> neuromodRetexture;
		private Neuromods neuromods;
		public override void DefsLoaded()
		{
			neuromods = new Neuromods();
			CreateSettings();
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

		private void UpdateNeuromodTextures()
        {
			GraphicData graphicData = new GraphicData
			{
				texPath = (neuromodRetexture)?"Things/Item/Typhon_Neuromod":"Things/Item/Special/MechSerumNeurotrainer",
				graphicClass = typeof(Graphic_Single)
			};
            foreach (ThingDef thingDef in neuromods)
            {
                thingDef.graphicData = graphicData;
            }
        }
	}
}
