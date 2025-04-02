using System.Threading.Tasks;
using HarmonyLib;

namespace FullHealth;

[HarmonyPatch(typeof(RoundDirector), nameof(RoundDirector.StartRoundRPC))]
public class FullHealthMultiplayerPatch
{
    // ReSharper disable once InconsistentNaming
    private static void Postfix()
    {
        Plugin.Logger.LogDebug("StartRoundRPC patch");

        if (!Configuration.Enabled.Value)
        {
            Plugin.Logger.LogDebug("The mod is disabled, so health is not changed");
            return;
        }

        if (!SemiFunc.IsMultiplayer())
        {
            Plugin.Logger.LogDebug("This is not a multiplayer");
            return;
        }

        if (Configuration.WorkMode.Value == WorkMode.Host && !SemiFunc.IsMasterClient())
        {
            Plugin.Logger.LogDebug("This is not a host");
            return;
        }

        if (!GamePhaseValidateHelper.IsValid())
        {
            return;
        }

        Task.Run(HealHelper.Heal);
    }
}