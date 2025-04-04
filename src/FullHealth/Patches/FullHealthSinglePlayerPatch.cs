using System.Threading.Tasks;
using HarmonyLib;

namespace FullHealth;

[HarmonyPatch(typeof(RoundDirector), nameof(RoundDirector.StartRound))]
public class FullHealthSinglePlayerPatch
{
    // ReSharper disable once InconsistentNaming
    private static void Postfix()
    {
        Plugin.Logger.LogDebug("StartRound patch");

        if (!Configuration.Enabled.Value)
        {
            Plugin.Logger.LogDebug("The mod is disabled, so HP is not changed");
            return;
        }

        if (SemiFunc.IsMultiplayer())
        {
            Plugin.Logger.LogDebug("This is not a single player");
            return;
        }

        if (!GamePhaseValidateHelper.IsValid())
        {
            return;
        }

        Task.Run(HealHelper.Heal);
    }
}