using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace FullHealth;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    // ReSharper disable once MemberCanBePrivate.Global
    public new static ManualLogSource Logger;

    private void Awake()
    {
        Logger = base.Logger;
        ConfigurationHelper.Bind(Config);
        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll(typeof(FullHealthMultiPlayerPatch));
        harmony.PatchAll(typeof(FullHealthSinglePlayerPatch));
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }
}