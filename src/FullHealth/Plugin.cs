using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using org.mariuszgromada.math.mxparser;

namespace FullHealth;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    // ReSharper disable once MemberCanBePrivate.Global
    public new static ManualLogSource Logger;

    private void Awake()
    {
        License.iConfirmNonCommercialUse("yvsimakov");
        Logger = base.Logger;
        ConfigurationHelper.Bind(Config);
        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll();
        Logger.LogInfo($"Mod '{MyPluginInfo.PLUGIN_GUID}' is loaded!");
        Logger.LogInfo("Configuration can be changed in 'BepInEx/config/FullHealth.cfg'");
    }
}