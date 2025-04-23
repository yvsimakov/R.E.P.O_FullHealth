using System.Collections.Generic;

namespace FullHealth;

internal static class SurviveHelper
{
    private static HashSet<string> Players { get; } = [];

    internal static void Add(PlayerAvatar player)
    {
        if (!SemiFunc.RunIsLevel())
        {
            return;
        }

        Players.Add(player.steamID);
        Plugin.Logger.LogDebug($"Player '{player.playerName}' has been added to the list");
    }

    internal static void Delete(PlayerAvatar player)
    {
        if (!SemiFunc.RunIsLevel())
        {
            return;
        }

        Players.Remove(player.steamID);
        Plugin.Logger.LogDebug($"Player '{player.playerName}' has been removed from the list.");
    }

    internal static bool Check(PlayerAvatar player)
    {
        return Check(player.steamID);
    }

    internal static bool Check(string steamId)
    {
        return Players.Contains(steamId);
    }

    internal static void AddAllOnGameStart()
    {
        if (SemiFunc.RunIsLobbyMenu())
        {
            _isItGameStart = true;
            Plugin.Logger.LogDebug("Game status is marked as game start");
            return;
        }

        if (!_isItGameStart)
        {
            return;
        }

        AddAll();
        _isItGameStart = false;
    }

    internal static void AddAllOnLevelStart()
    {
        if (!SemiFunc.RunIsLevel())
        {
            return;
        }

        AddAll();
    }

    private static void AddAll()
    {
        Players.Clear();

        foreach (var player in PlayersHelper.Get())
        {
            Players.Add(player.steamID);
            Plugin.Logger.LogDebug($"Player '{player.playerName}' has been added to the list");
        }
    }

    private static bool _isItGameStart;
}