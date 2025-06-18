using System.Threading.Tasks;
using HarmonyLib;

namespace FullHealth;

[HarmonyPatch(typeof(RoundDirector), nameof(RoundDirector.StartRoundRPC))]
public class RoundDirectorStartRoundRPCPatch
{
    private const ConfigurationMode Mode = ConfigurationMode.Start;

    [HarmonyPostfix]
    // ReSharper disable once UnusedMember.Local
    private static void Postfix()
    {
        Plugin.Logger.LogDebug($"{nameof(RoundDirector)}.{nameof(RoundDirector.StartRoundRPC)} patch");

        SurviveHelper.AddAllOnGameStart();

        if (!Configuration.Start.Enabled.Value)
        {
            Plugin.Logger.LogDebug("Healing is disabled at the start of a level or in a shop, etc.");
            SurviveHelper.AddAllOnLevelStart();
            return;
        }

        if (!SemiFunc.IsMultiplayer())
        {
            Plugin.Logger.LogDebug("This is not a multiplayer");
            SurviveHelper.AddAllOnLevelStart();
            return;
        }

        if (!WorkModeValidatorHelper.Validate(Mode))
        {
            SurviveHelper.AddAllOnLevelStart();
            return;
        }

        if (!GamePhaseValidateHelper.IsValid(Mode))
        {
            SurviveHelper.AddAllOnLevelStart();
            return;
        }

        Task.Run(() => HealHelper.Heal(Mode));
    }
}