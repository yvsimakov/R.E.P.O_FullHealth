using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FullHealth;

internal static class HealHelper
{
    internal static async Task Heal()
    {
        var cts = new CancellationTokenSource(60000);

        while (true)
        {
            if (cts.IsCancellationRequested)
            {
                Plugin.Logger.LogDebug("Timeout exceeded, operation cancelled");
                return;
            }

            Plugin.Logger.LogDebug("Waiting for all players to land");
            await Task.Delay(1000, CancellationToken.None);

            if (SemiFunc.RunIsLobby())
            {
                break;
            }

            var players = SemiFunc.PlayerGetList();
            players = players is { Count: > 0 } ? players : [SemiFunc.PlayerAvatarLocal()];

            if (players.All(x => x.levelAnimationCompleted))
            {
                break;
            }
        }

        foreach (var player in SemiFunc.PlayerGetList())
        {
            var playerHealthValue = player.playerHealth.health;
            var expectedHealth = decimal.ToInt32(player.playerHealth.maxHealth * (Configuration.Percent.Value / 100));

            if (playerHealthValue >= expectedHealth)
            {
                Plugin.Logger.LogDebug(
                    $"The health of the player '{player.playerName}' is '{playerHealthValue}' and this is is more or equal than expected '{expectedHealth}', so health is not changed");
                return;
            }

            player.playerHealth.HealOther(expectedHealth - playerHealthValue, true);

            Plugin.Logger.LogDebug($"The health of the player '{player.playerName}' has been changed from '{playerHealthValue}' to '{expectedHealth}'");
        }
    }
}