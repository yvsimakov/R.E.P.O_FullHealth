namespace FullHealth;

public static class WorkModeValidatorHelper
{
    public static bool Validate()
    {
        if (SemiFunc.IsMasterClientOrSingleplayer() && !Configuration.WorkMode.Value.HasFlag(WorkMode.Host))
        {
            Plugin.Logger.LogDebug("This is a host, but heals when you host is disabled");
            return false;
        }

        if (!SemiFunc.IsMasterClientOrSingleplayer() && !Configuration.WorkMode.Value.HasFlag(WorkMode.Client))
        {
            Plugin.Logger.LogDebug("This is a client, but heals when you client is disabled");
            return false;
        }

        return true;
    }
}