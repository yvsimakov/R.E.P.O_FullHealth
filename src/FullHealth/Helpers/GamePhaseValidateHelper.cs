namespace FullHealth;

public static class GamePhaseValidateHelper
{
    public static bool IsValid()
    {
        var gamePhaseMode = GetGamePhaseMode();

        if (!gamePhaseMode.HasValue)
        {
            Plugin.Logger.LogDebug("This is not a game phase");
            return false;
        }

        if (!Configuration.PhaseMode.Value.HasFlag(gamePhaseMode.Value))
        {
            Plugin.Logger.LogDebug("Healing in this phase is disabled in the configuration");
            return false;
        }

        return true;
    }

    private static PhaseMode? GetGamePhaseMode()
    {
        if (SemiFunc.RunIsLevel())
        {
            return PhaseMode.Level;
        }

        if (SemiFunc.RunIsShop())
        {
            return PhaseMode.Shop;
        }

        if (SemiFunc.RunIsLobby())
        {
            return PhaseMode.Lobby;
        }

        return null;
    }
}