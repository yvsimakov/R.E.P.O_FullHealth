namespace FullHealth;

public static class HealPlayerHelper
{
    public static void Heal(PlayerAvatar player)
    {
        if (player?.playerHealth == null)
        {
            Plugin.Logger.LogWarning("Player or his health is not set");
            return;
        }

        var playerHealthValue = player.playerHealth.health;
        var expectedHealth = decimal.ToInt32(player.playerHealth.maxHealth * (Configuration.Percent.Value / 100));

        if (playerHealthValue >= expectedHealth)
        {
            Plugin.Logger.LogDebug(
                $"The health of the player '{player.playerName}' is '{playerHealthValue}' " +
                $"and this is is more or equal than expected '{expectedHealth}', so health is not changed");
            return;
        }

        player.playerHealth.HealOther(expectedHealth - playerHealthValue, true);

        Plugin.Logger.LogDebug($"The health of the player '{player.playerName}' has been changed from '{playerHealthValue}' to '{expectedHealth}'");
    }
}