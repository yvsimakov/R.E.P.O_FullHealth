using System.Threading.Tasks;
using HarmonyLib;

namespace FullHealth;

[HarmonyPatch(typeof(RoundDirector), nameof(RoundDirector.StartRoundRPC))]
public class FullHealthMultiPlayerPatch
{
    // ReSharper disable once InconsistentNaming
    private static void Postfix()
    {
        Plugin.Logger.LogDebug("StartRoundRPC patch");

        if (!ValidateHelper.IsValid())
        {
            return;
        }

        if (!SemiFunc.IsMultiplayer())
        {
            Plugin.Logger.LogDebug("This is not a multiplayer");
            return;
        }

        if (Configuration.WorkMode.Value == WorkMode.Host && !SemiFunc.IsMasterClient())
        {
            Plugin.Logger.LogDebug("This is not a master client");
            return;
        }

        Task.Run(HealHelper.Heal);
    }
}