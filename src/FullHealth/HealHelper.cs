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
                return;
            }

            var players = SemiFunc.PlayerGetList();
            players = players is { Count: > 0 } ? players : [SemiFunc.PlayerAvatarLocal()];

            if (players.All(x => x.levelAnimationCompleted))
            {
                break;
            }

            Plugin.Logger.LogDebug("Waiting for all players to land");
            await Task.Delay(1000, CancellationToken.None);
        }

        foreach (var player in SemiFunc.PlayerGetList())
        {
            var playerHealthValue = player.playerHealth.health;
            var expectedHealth = decimal.ToInt32(player.playerHealth.maxHealth * (Configuration.Percent.Value / 100));

            if (playerHealthValue >= expectedHealth)
            {
                Plugin.Logger.LogDebug($"Player's health '{playerHealthValue}' is higher or equal than expected '{expectedHealth}', so health is not changed");
                return;
            }

            if (SemiFunc.IsMultiplayer())
            {
                player.playerHealth.HealOtherRPC(expectedHealth - playerHealthValue, true);
            }
            else
            {
                player.playerHealth.HealOther(expectedHealth - playerHealthValue, true);
            }

            Plugin.Logger.LogDebug($"Player's health has been changed from '{playerHealthValue}' to '{expectedHealth}'");
        }
    }
}