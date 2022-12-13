using HugsLib;

namespace Typhon
{
    public class Mod : ModBase
    {
        public override void DefsLoaded()
        {
            Logger.Message("Hello, World!");
        }
    }
}
