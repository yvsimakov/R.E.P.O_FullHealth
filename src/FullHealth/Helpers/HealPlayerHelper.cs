using System;

namespace FullHealth;

public static class HealPlayerHelper
{
    private static readonly Random Random = new();

    public static void Heal(PlayerAvatar player)
    {
        if (player?.playerHealth == null)
        {
            Plugin.Logger.LogWarning("Player or his health is not set");
            return;
        }

        if (!AlivePlayersHelper.ValidateSurviveRequirement(player))
        {
            Plugin.Logger.LogDebug($"The player '{player.playerName}' did not survive and will not be healed");
            return;
        }
        
        var playerHealthValue = player.playerHealth.health;

        int healValue;

        if (Configuration.HealthPackModeValues is { Length: > 0 })
        {
            var index = Configuration.HealthPackModeValues.Length == 1 ? 0 : Random.Next(0, Configuration.HealthPackModeValues.Length);
            healValue = Configuration.HealthPackModeValues[index];
        }
        else if (Configuration.ExactValue.Value > 0)
        {
            healValue = Configuration.ExactValue.Value;
        }
        else
        {
            var expectedHealth = decimal.ToInt32(player.playerHealth.maxHealth * (Configuration.Percent.Value / 100));

            if (playerHealthValue >= expectedHealth)
            {
                Plugin.Logger.LogDebug(
                    $"The health of the player '{player.playerName}' is '{playerHealthValue}' " +
                    $"and this is is more or equal than expected '{expectedHealth}', so health is not changed");
                return;
            }

            healValue = expectedHealth - playerHealthValue;
        }

        player.playerHealth.HealOther(healValue, true);

        Plugin.Logger.LogDebug($"Player '{player.playerName}' with '{playerHealthValue}' health received '{healValue}' HP. " +
                               $"So player should have '{Math.Min(player.playerHealth.maxHealth, playerHealthValue + healValue)}' HP");
    }
}