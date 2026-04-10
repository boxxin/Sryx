using BepInEx;

namespace StupidTemplate
{
    [System.ComponentModel.Description(PluginInfo.Description)]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class HarmonyPatches : BaseUnityPlugin
    {
        private void Awake()
        {
             Patches.PatchHandler.PatchAll();
        }
    }
}
