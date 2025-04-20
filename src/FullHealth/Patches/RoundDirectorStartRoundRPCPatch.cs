using System.Threading.Tasks;
using HarmonyLib;
using Steamworks.Data;

namespace FullHealth;

[HarmonyPatch(typeof(RoundDirector), nameof(RoundDirector.StartRoundRPC))]
public class RoundDirectorStartRoundRPCPatch
{
    [HarmonyPostfix]
    // ReSharper disable once UnusedMember.Local
    private static void Postfix()
    {
        Plugin.Logger.LogDebug($"{nameof(RoundDirector)}.{nameof(RoundDirector.StartRoundRPC)} patch");
        
        if (!Configuration.Enabled.Value)
        {
            Plugin.Logger.LogDebug("The mod is disabled");
            return;
        }

        if (!SemiFunc.IsMultiplayer())
        {
            Plugin.Logger.LogDebug("This is not a multiplayer");
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