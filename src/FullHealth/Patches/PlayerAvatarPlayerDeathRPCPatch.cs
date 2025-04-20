using HarmonyLib;

namespace FullHealth;

[HarmonyPatch(typeof(PlayerAvatar), nameof(PlayerAvatar.PlayerDeathRPC))]
public class PlayerAvatarPlayerDeathRPCPatch
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    private static void Postfix(PlayerAvatar __instance)
    {
        Plugin.Logger.LogDebug($"{nameof(PlayerAvatar)}.{nameof(PlayerAvatar.PlayerDeathRPC)} patch");

        if (!Configuration.Enabled.Value)
        {
            Plugin.Logger.LogDebug("The mod is disabled");
            return;
        }

        AlivePlayersHelper.Delete(__instance);
    }
}