using System.Reflection;
using HarmonyLib;

namespace FullHealth;

[HarmonyPatch(typeof(PlayerAvatar), nameof(PlayerAvatar.LoadingLevelAnimationCompletedRPC))]
public class FullHealthPatch
{
    private static readonly FieldInfo FieldInfo = AccessTools.Field(typeof(PlayerAvatar), nameof(PlayerAvatar.playerHealth));

    // ReSharper disable once InconsistentNaming
    private static void Postfix(PlayerAvatar __instance)
    {
        var playerHealth = (PlayerHealth)FieldInfo.GetValue(__instance);
        playerHealth.Heal(playerHealth.maxHealth - playerHealth.health);
    }
}