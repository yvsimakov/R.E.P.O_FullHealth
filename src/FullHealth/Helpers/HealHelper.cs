using System.Threading.Tasks;

namespace FullHealth;

public static class HealHelper
{
    public static async Task Heal()
    {
        try
        {
            await InternalHeal();
        }
        finally
        {
            SurviveHelper.AddAllOnLevelStart();
        }
    }

    private static async Task InternalHeal()
    {
        if (!await WaitHelper.Wait())
        {
            return;
        }

        var players = PlayersHelper.Get();
        foreach (var player in players)
        {
            switch (player.isLocal)
            {
                case true when !Configuration.HealMode.Value.HasFlag(HealMode.Self):
                    Plugin.Logger.LogDebug("Self-healing disabled");
                    return;
                case false when !Configuration.HealMode.Value.HasFlag(HealMode.Others):
                    Plugin.Logger.LogDebug("Heal others disabled");
                    return;
                default:
                    HealPlayerHelper.Heal(player, players.Count);
                    break;
            }
        }
    }
}