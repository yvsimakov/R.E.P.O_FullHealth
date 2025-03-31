using System.Collections.Generic;

namespace FullHealth;

public static class PlayersHelper
{
    public static List<PlayerAvatar> Get()
    {
        var players = SemiFunc.PlayerGetList();

        if (players is { Count: > 0 })
        {
            return players;
        }

        var player = SemiFunc.PlayerAvatarLocal();

        if (player != null)
        {
            return [player];
        }

        return [];
    }
}