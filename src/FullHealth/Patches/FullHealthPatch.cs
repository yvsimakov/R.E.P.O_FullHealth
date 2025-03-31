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

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        HealHelper.Heal();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
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

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        HealHelper.Heal();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
    }
}