using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace FullHealth;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    // ReSharper disable once MemberCanBePrivate.Global
    internal new static ManualLogSource Logger;

    private void Awake()
    {
        Logger = base.Logger;
        Configuration.Enabled = Config.Bind("General", "Enabled", true, "Indicates whether the plugin is enabled");
        Configuration.Percent = Config.Bind("General", "Percent", 100M, "Percentage of maximum health to which the player will be healed (Min: 0, Max: 100)");
        Configuration.Percent.Value = Math.Min(100M, Math.Max(0M, Configuration.Percent.Value));

        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll(typeof(FullHealthMultiPlayerPatch));
        harmony.PatchAll(typeof(FullHealthSinglePlayerPatch));
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }
}