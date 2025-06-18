using System.Threading.Tasks;
using HarmonyLib;

namespace FullHealth;

[HarmonyPatch(typeof(PlayerAvatar), nameof(PlayerAvatar.ReviveRPC))]
public class PlayerAvatarReviveRPCPatch
{
    private const ConfigurationMode Mode = ConfigurationMode.Revival;

    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    private static void Postfix(PlayerAvatar __instance)
    {
        Plugin.Logger.LogDebug($"{nameof(PlayerAvatar)}.{nameof(PlayerAvatar.ReviveRPC)} patch");

        SurviveHelper.Add(__instance);

        if (!Configuration.Revival.Enabled.Value)
        {
            Plugin.Logger.LogDebug("Healing is disabled upon revival");
            return;
        }

        if (!WorkModeValidatorHelper.Validate(Mode))
        {
            return;
        }

        if (!GamePhaseValidateHelper.IsValid(Mode))
        {
            return;
        }

        HealPlayerHelper.Heal(Mode, __instance, PlayersHelper.Get().Count);
    }
}