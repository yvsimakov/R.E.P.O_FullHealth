using System;

namespace FullHealth;

public static class HealPlayerHelper
{
    private static readonly Random Random = new();

    public static void Heal(ConfigurationMode mode, PlayerAvatar player, int playerCount)
    {
        switch (player.isLocal)
        {
            case true when !Configuration.Items[mode].HealMode.Value.HasFlag(HealMode.Self):
                Plugin.Logger.LogDebug("Self-healing disabled");
                return;
            case false when !Configuration.Items[mode].HealMode.Value.HasFlag(HealMode.Others):
                Plugin.Logger.LogDebug("Heal others disabled");
                return;
        }

        if (player.playerHealth == null)
        {
            Plugin.Logger.LogWarning("Player health is not set");
            return;
        }

        var playerHealthValue = mode switch
        {
            ConfigurationMode.Revival => 1,
            _ => player.playerHealth.health,
        };

        int healValue;

        if (mode == ConfigurationMode.Start && Configuration.Start.HealRequirementMode.Value.HasFlag(HealRequirementMode.Survive) && !SurviveHelper.Check(player))
        {
            Plugin.Logger.LogDebug($"The player '{player.playerName}' did not survive and will not be healed");
            healValue = 0;
        }
        else if (!string.IsNullOrWhiteSpace(Configuration.Items[mode].Expression.Value))
        {
            try
            {
                healValue = ExpressionHelper.Calculate(mode, player, playerCount);
            }
            catch (Exception e)
            {
                Plugin.Logger.LogError("An error occurred while calculating the heal value. Please check the expression: " + e.Message);
                healValue = 0;
            }
        }
        else if (Configuration.Items[mode].HealthPackModeValues is { Length: > 0 })
        {
            var index = Configuration.Items[mode].HealthPackModeValues.Length == 1 ? 0 : Random.Next(0, Configuration.Items[mode].HealthPackModeValues.Length);
            healValue = Configuration.Items[mode].HealthPackModeValues[index];
        }
        else if (Configuration.Items[mode].ExactValue.Value > 0)
        {
            healValue = Configuration.Items[mode].ExactValue.Value;
        }
        else if (Configuration.Items[mode].ByMaxHealthPercentage.Value > 0)
        {
            healValue = decimal.ToInt32(player.playerHealth.maxHealth * (Configuration.Items[mode].ByMaxHealthPercentage.Value / 100));
        }
        else
        {
            var expectedHealth = decimal.ToInt32(player.playerHealth.maxHealth * (Configuration.Items[mode].ToMaxHealthPercentage.Value / 100));
            healValue = Math.Max(0, expectedHealth - playerHealthValue);
        }

        if (healValue == 0)
        {
            Plugin.Logger.LogDebug($"Player '{player.playerName}' was not healed because the heal value is zero");
            return;
        }

        player.playerHealth.HealOther(healValue, true);

        Plugin.Logger.LogDebug($"Player '{player.playerName}' with '{playerHealthValue}' HP received '{healValue}' HP. " +
                               $"So player should have '{Math.Min(player.playerHealth.maxHealth, playerHealthValue + healValue)}' HP");
    }
}