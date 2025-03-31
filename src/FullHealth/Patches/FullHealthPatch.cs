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

        if (!SemiFunc.IsMasterClient())
        {
            Plugin.Logger.LogDebug("This is not a master client");
            return;
        }

        Task.Run(HealHelper.Heal);
    }
}

[HarmonyPatch(typeof(RoundDirector), nameof(RoundDirector.StartRound))]
public class FullHealthSinglePlayerPatch
{
    // ReSharper disable once InconsistentNaming
    private static void Postfix()
    {
        Plugin.Logger.LogDebug("StartRound patch");

        if (!ValidateHelper.IsValid())
        {
            return;
        }

        if (SemiFunc.IsMultiplayer())
        {
            Plugin.Logger.LogDebug("This is not a single player");
            return;
        }

        Task.Run(HealHelper.Heal);
    }
}