using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Expressive;
using FullHealth.Expressions;

namespace FullHealth;

public static class ExpressionHelper
{
    public const string PlayerName = "PlayerName";
    public const string PlayerMaxHealthName = "PlayerMaxHealth";
    public const string PlayerHealthName = "PlayerHealth";
    public const string PlayerSurvivedName = "PlayerSurvived";
    public const string PlayerCountName = "PlayerCount";
    public const string PlayerUpgradeHealthName = "PlayerUpgradeHealth";
    public const string PlayerUpgradeStaminaName = "PlayerUpgradeStamina";
    public const string PlayerUpgradeExtraJumpName = "PlayerUpgradeExtraJump";
    public const string PlayerUpgradeLaunchName = "PlayerUpgradeLaunch";
    public const string PlayerUpgradeMapPlayerCountName = "PlayerUpgradeMapPlayerCount";
    public const string PlayerUpgradeSpeedName = "PlayerUpgradeSpeed";
    public const string PlayerUpgradeStrengthName = "PlayerUpgradeStrength";
    public const string PlayerUpgradeThrowName = "PlayerUpgradeThrow";
    public const string PlayerUpgradeRangeName = "PlayerUpgradeRange";
    private static readonly object LockObject = new();


    private static Expression _expression = new(string.Empty);

    public static void UpdateExpression(string expression)
    {
        lock (LockObject)
        {
            _expression = new(expression);
            _expression.RegisterFunction(RandomListFunction.Instance);
        }
    }


    public static int Calculate(PlayerAvatar player, int playerCount)
    {
        return Calculate(player.playerName, player.playerHealth.maxHealth, player.playerHealth.health, player.steamID, playerCount);
    }

    public static int Calculate(string playerName, int playerMaxHealth, int playerHealth, string playerSteamId, int playerCount)
    {
        lock (LockObject)
        {
            var variableProvider = new VariableProvider(playerName, playerMaxHealth, playerHealth, playerSteamId, playerCount);
            var value = _expression.Evaluate(variableProvider);
            return Convert.ToInt32(value);
        }
    }

    private class VariableProvider : IVariableProvider
    {
        private readonly string _playerName;
        private readonly int _playerMaxHealth;
        private readonly int _playerHealth;
        private readonly string _playerSteamID;
        private readonly int _playerCount;
        private readonly ConcurrentDictionary<string, object> _cache = new();

        public VariableProvider(string playerName, int playerMaxHealth, int playerHealth, string playerSteamID, int playerCount)
        {
            _playerName = playerName;
            _playerMaxHealth = playerMaxHealth;
            _playerHealth = playerHealth;
            _playerSteamID = playerSteamID;
            _playerCount = playerCount;
        }

        public bool TryGetValue(string variableName, out object value)
        {
            if (_cache.TryGetValue(variableName, out value))
            {
                return true;
            }

            switch (variableName)
            {
                case PlayerName:
                    value = _cache.GetOrAdd(PlayerName, _playerName);
                    return true;
                case PlayerMaxHealthName:
                    value = _cache.GetOrAdd(PlayerMaxHealthName, _playerMaxHealth);
                    return true;
                case PlayerHealthName:
                    value = _cache.GetOrAdd(PlayerHealthName, _playerHealth);
                    return true;
                case PlayerSurvivedName:
                    value = _cache.GetOrAdd(PlayerSurvivedName, SurviveHelper.Check(_playerSteamID));
                    return true;
                case PlayerCountName:
                    value = _cache.GetOrAdd(PlayerCountName, _playerCount);
                    return true;
                case PlayerUpgradeHealthName:
                    value = _cache.GetOrAdd(PlayerUpgradeHealthName, StatsManager.instance?.playerUpgradeHealth?.GetValueOrDefault(_playerSteamID, 0) ?? 0);
                    return true;
                case PlayerUpgradeStaminaName:
                    value = _cache.GetOrAdd(PlayerUpgradeStaminaName, StatsManager.instance?.playerUpgradeStamina?.GetValueOrDefault(_playerSteamID, 0) ?? 0);
                    return true;
                case PlayerUpgradeExtraJumpName:
                    value = _cache.GetOrAdd(PlayerUpgradeExtraJumpName, StatsManager.instance?.playerUpgradeExtraJump?.GetValueOrDefault(_playerSteamID, 0) ?? 0);
                    return true;
                case PlayerUpgradeLaunchName:
                    value = _cache.GetOrAdd(PlayerUpgradeLaunchName, StatsManager.instance?.playerUpgradeLaunch?.GetValueOrDefault(_playerSteamID, 0) ?? 0);
                    return true;
                case PlayerUpgradeMapPlayerCountName:
                    value = _cache.GetOrAdd(PlayerUpgradeMapPlayerCountName, StatsManager.instance?.playerUpgradeMapPlayerCount?.GetValueOrDefault(_playerSteamID, 0) ?? 0);
                    return true;
                case PlayerUpgradeSpeedName:
                    value = _cache.GetOrAdd(PlayerUpgradeSpeedName, StatsManager.instance?.playerUpgradeSpeed?.GetValueOrDefault(_playerSteamID, 0) ?? 0);
                    return true;
                case PlayerUpgradeStrengthName:
                    value = _cache.GetOrAdd(PlayerUpgradeStrengthName, StatsManager.instance?.playerUpgradeStrength?.GetValueOrDefault(_playerSteamID, 0) ?? 0);
                    return true;
                case PlayerUpgradeThrowName:
                    value = _cache.GetOrAdd(PlayerUpgradeThrowName, StatsManager.instance?.playerUpgradeThrow?.GetValueOrDefault(_playerSteamID, 0) ?? 0);
                    return true;
                case PlayerUpgradeRangeName:
                    value = _cache.GetOrAdd(PlayerUpgradeRangeName, StatsManager.instance?.playerUpgradeRange?.GetValueOrDefault(_playerSteamID, 0) ?? 0);
                    return true;
            }

            return false;
        }
    }
}