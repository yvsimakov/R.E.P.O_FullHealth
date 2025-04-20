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
            AlivePlayersHelper.AddAllOnLevelStart();
        }
    }

    private static async Task InternalHeal()
    {
        if (!await WaitHelper.Wait())
        {
            return;
        }

        foreach (var player in PlayersHelper.Get())
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
                    HealPlayerHelper.Heal(player);
                    break;
            }
        }
    }
}