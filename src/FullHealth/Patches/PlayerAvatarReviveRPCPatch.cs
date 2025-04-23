using HarmonyLib;

namespace FullHealth;

[HarmonyPatch(typeof(PlayerAvatar), nameof(PlayerAvatar.ReviveRPC))]
public class PlayerAvatarReviveRPCPatch
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    private static void Postfix(PlayerAvatar __instance)
    {
        Plugin.Logger.LogDebug($"{nameof(PlayerAvatar)}.{nameof(PlayerAvatar.ReviveRPC)} patch");

        if (!Configuration.Enabled.Value)
        {
            Plugin.Logger.LogDebug("The mod is disabled");
            return;
        }

        SurviveHelper.Add(__instance);
    }
}