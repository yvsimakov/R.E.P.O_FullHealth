using org.mariuszgromada.math.mxparser;

namespace FullHealth;

public static class ExpressionHelper
{
    public const string PlayerMaxHealthConstName = "PlayerMaxHealth";
    public const string PlayerHealthConstName = "PlayerHealth";
    public const string PlayerCountConstName = "PlayerCount";
    private static readonly object LockObject = new();

    public static double Calculate(PlayerAvatar player, int playerCountValue)
    {
        return Calculate(
            player.playerName,
            player.playerHealth.maxHealth,
            player.playerHealth.health,
            playerCountValue);
    }

    private static readonly Argument PlayerMaxHealthArgument = new(PlayerMaxHealthConstName, 100);
    private static readonly Argument PlayerHealthArgument = new(PlayerHealthConstName, 30);
    private static readonly Argument PlayerCountArgument = new(PlayerCountConstName, 4);
    private static Expression _expression = new();

    public static void UpdateExpression(string expression)
    {
        _expression = new(expression, PlayerMaxHealthArgument, PlayerHealthArgument, PlayerCountArgument);
    }

    public static double Calculate(
        string playerName,
        int playerMaxHealth,
        int playerHealth,
        int playerCount)
    {
        lock (LockObject)
        {
            PlayerMaxHealthArgument.setArgumentValue(playerMaxHealth);
            PlayerHealthArgument.setArgumentValue(playerHealth);
            PlayerCountArgument.setArgumentValue(playerCount);
            var value = _expression.calculate();
            Plugin.Logger.LogDebug($"Calculation of the healing value for " +
                                   $"Player: '{playerName}', " +
                                   $"PlayerMaxHealth: '{playerMaxHealth}', " +
                                   $"PlayerHealth: '{playerHealth}', " +
                                   $"PlayerCount: '{playerCount}' completed. " +
                                   $"Result: '{value}'");
            return value;
        }
    }
}