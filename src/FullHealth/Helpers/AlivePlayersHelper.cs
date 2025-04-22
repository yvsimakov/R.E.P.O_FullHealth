using System.Collections.Generic;

namespace FullHealth;

internal static class AlivePlayersHelper
{
    private static HashSet<string> Players { get; } = [];

    internal static void Add(PlayerAvatar player)
    {
        if (!ValidateRequirementAndRunIsLevel())
        {
            return;
        }

        Players.Add(GetPlayerKey(player));
        Plugin.Logger.LogDebug($"Player '{player.playerName}' has been added to the list");
    }

    internal static void Delete(PlayerAvatar player)
    {
        if (!ValidateRequirementAndRunIsLevel())
        {
            return;
        }

        Players.Remove(GetPlayerKey(player));
        Plugin.Logger.LogDebug($"Player '{player.playerName}' has been removed from the list.");
    }

    internal static bool ValidateSurviveRequirement(PlayerAvatar player)
    {
        if (!ValidateRequirement())
        {
            return true;
        }

        var result = Players.Contains(GetPlayerKey(player));
        Plugin.Logger.LogDebug($"Status of checking whether the player '{player.playerName}' in the list is '{result}'");
        return result;
    }

    internal static void AddAllOnGameStart()
    {
        if (!ValidateRequirement())
        {
            return;
        }

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
        if (!ValidateRequirementAndRunIsLevel())
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
            Players.Add(GetPlayerKey(player));
            Plugin.Logger.LogDebug($"Player '{player.playerName}' has been added to the list");
        }
    }

    private static string GetPlayerKey(PlayerAvatar player)
    {
        return !string.IsNullOrWhiteSpace(player.steamID) ? player.steamID : player.name;
    }

    private static bool _isItGameStart;

    private static bool ValidateRequirement()
    {
        return Configuration.HealRequirementMode.Value.HasFlag(HealRequirementMode.Survive);
    }

    private static bool ValidateRequirementAndRunIsLevel()
    {
        return ValidateRequirement() && SemiFunc.RunIsLevel();
    }
}