using System.Collections.Generic;

namespace FullHealth;

public static class PlayersHelper
{
    public static List<PlayerAvatar> Get()
    {
        try
        {
            var players = SemiFunc.PlayerGetList();
            if (players is { Count: > 0 })
            {
                return players;
            }
        }
        catch
        {
            Plugin.Logger.LogError("An error occurred while retrieving the list of players");
            // ignored
        }

        try
        {
            var player = SemiFunc.PlayerAvatarLocal();
            if (player != null)
            {
                return [player];
            }

            return [];
        }
        catch
        {
            Plugin.Logger.LogError("An error occurred while getting the local player");
            return [];
        }
    }
}