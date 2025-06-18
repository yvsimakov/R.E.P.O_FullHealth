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

        SurviveHelper.Delete(__instance);
    }
}