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