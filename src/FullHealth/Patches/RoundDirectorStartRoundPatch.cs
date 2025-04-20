using System.Threading.Tasks;
using HarmonyLib;

namespace FullHealth;

[HarmonyPatch(typeof(RoundDirector), nameof(RoundDirector.StartRound))]
public class RoundDirectorStartRoundPatch
{
    [HarmonyPostfix]
    // ReSharper disable once UnusedMember.Local
    private static void Postfix()
    {
        Plugin.Logger.LogDebug($"{nameof(RoundDirector)}.{nameof(RoundDirector.StartRound)} patch");

        if (!Configuration.Enabled.Value)
        {
            Plugin.Logger.LogDebug("The mod is disabled");
            return;
        }

        if (SemiFunc.IsMultiplayer())
        {
            Plugin.Logger.LogDebug("This is not a single player");
            return;
        }

        if (!WorkModeValidatorHelper.Validate())
        {
            return;
        }

        AlivePlayersHelper.AddAllOnGameStart();

        if (!GamePhaseValidateHelper.IsValid())
        {
            AlivePlayersHelper.AddAllOnLevelStart();
            return;
        }

        Task.Run(HealHelper.Heal);
    }
}