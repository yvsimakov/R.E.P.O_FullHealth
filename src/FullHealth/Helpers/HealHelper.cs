using System.Threading.Tasks;

namespace FullHealth;

public static class HealHelper
{
    public static async Task Heal(ConfigurationMode mode)
    {
        try
        {
            await InternalHeal(mode);
        }
        finally
        {
            SurviveHelper.AddAllOnLevelStart();
        }
    }

    private static async Task InternalHeal(ConfigurationMode mode)
    {
        if (!await WaitHelper.Wait())
        {
            return;
        }

        var players = PlayersHelper.Get();
        foreach (var player in players)
        {
            HealPlayerHelper.Heal(mode, player, players.Count);
        }
    }
}