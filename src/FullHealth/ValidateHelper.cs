namespace FullHealth;

public static class ValidateHelper
{
    public static bool IsValid()
    {
        if (!Configuration.Enabled.Value)
        {
            Plugin.Logger.LogDebug("The plugin is disabled, so health is not changed");
            return false;
        }

        if (!SemiFunc.RunIsArena() && !SemiFunc.RunIsShop() && !SemiFunc.RunIsLevel())
        {
            Plugin.Logger.LogDebug("This is not a game phase");
            return false;
        }

        return true;
    }
}