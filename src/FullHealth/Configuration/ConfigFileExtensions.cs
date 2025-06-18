using BepInEx.Configuration;
using FullHealth.Expressions;

namespace FullHealth;

public static class ConfigFileExtensions
{
    public static ConfigEntry<decimal> BindToMaxHealthPercentage(this ConfigFile config, string section)
    {
        return config.Bind(section, "ToMaxHealthPercentage", 100M,
            "The percentage of the player's maximum health to which he will be healed (Min: 0, Max: 100).\r\n" +
            "Attention! You must set the `PhaseMode` parameter to only one of `Level` or `Lobby`, not both. Otherwise, the healing will be done twice.");
    }

    public static ConfigEntry<decimal> BindByMaxHealthPercentage(this ConfigFile config, string section)
    {
        return config.Bind(section, "ByMaxHealthPercentage", 0M,
            "The percentage of the player's maximum health that the player will be healed to (Min: 0, Max: 100).\r\n" +
            "Attention! You must set the `PhaseMode` parameter to only one of `Level` or `Lobby`, not both. Otherwise, the healing will be done twice.\r\n" +
            "It has priority over the `ToMaxHealthPercentage` parameter.");
    }

    public static ConfigEntry<int> BindExactValue(this ConfigFile config, string section)
    {
        return config.Bind(section, "ExactValue", 0,
            "Sets the exact value to heal.\r\n" +
            "Attention! You must set the `PhaseMode` parameter to only one of `Level` or `Lobby`, not both. Otherwise, the healing will be done twice.\r\n" +
            "If the value is `0`, then it is disabled.\r\n" +
            "It has priority over the `ToMaxHealthPercentage` and `ByMaxHealthPercentage` parameters.\r\n" +
            "For example, if you set the value to `15`, you will always receive 15 HP.");
    }

    public static ConfigEntry<HealthPackMode> BindHealthPackMode(this ConfigFile config, string section)
    {
        return config.Bind(section, "HealthPackMode", HealthPackMode.None,
            "Sets healing value options to the same as in health packs, one of which will be applied randomly.\r\n" +
            "Attention! You must set the `PhaseMode` parameter to only one of `Level` or `Lobby`, not both. Otherwise, the healing will be done twice.\r\n" +
            "If the value is `None`, then it is disabled.\r\n" +
            "It has priority over the `ToMaxHealthPercentage` and `ByMaxHealthPercentage` and `ExactValue` parameters.\r\n" +
            "For example, if you set the value to `Medium, Large`, you will receive a random healing of 50HP or 100HP.\r\n" +
            "`Small` - heal 25 HP.\r\n" +
            "`Medium` - heal 50 HP.\r\n" +
            "`Large` - heal 100 HP.");
    }

    public static ConfigEntry<string> BindExpression(this ConfigFile config, string section)
    {
        return config.Bind<string>(section, "Expression", null,
            "The expression used to calculate the healing value.\r\n" +
            "Attention! You must set the `PhaseMode` parameter to only one of `Level` or `Lobby`, not both. Otherwise, the healing will be done twice.\r\n" +
            "If the value is null or empty or whitespace, then it is disabled.\r\n" +
            "It has priority over the `ToMaxHealthPercentage` and `ByMaxHealthPercentage` and `ExactValue` and `HealthPackMode` parameters.\r\n" +
            "The library used to parse expression is https://github.com/bijington/expressive. You can find detailed information in the documentation.\r\n" +
            "Variables:\r\n" +
            $"`[{ExpressionHelper.PlayerName}]` - player's name.\r\n" +
            $"`[{ExpressionHelper.PlayerMaxHealthName}]` - player's maximum health.\r\n" +
            $"`[{ExpressionHelper.PlayerHealthName}]` - player's health.\r\n" +
            $"`[{ExpressionHelper.PlayerSurvivedName}]` - whether the player survived the last round.\r\n" +
            $"`[{ExpressionHelper.PlayerCountName}]` - number of players.\r\n" +
            $"`[{ExpressionHelper.PlayerUpgradeHealthName}]` - player's upgrade health level.\r\n" +
            $"`[{ExpressionHelper.PlayerUpgradeStaminaName}]` - player's upgrade stamina level.\r\n" +
            $"`[{ExpressionHelper.PlayerUpgradeExtraJumpName}]` - player's upgrade extra jump level.\r\n" +
            $"`[{ExpressionHelper.PlayerUpgradeLaunchName}]` - player's upgrade launch level.\r\n" +
            $"`[{ExpressionHelper.PlayerUpgradeMapPlayerCountName}]` - player's upgrade map player count level.\r\n" +
            $"`[{ExpressionHelper.PlayerUpgradeSpeedName}]` - player's upgrade speed level.\r\n" +
            $"`[{ExpressionHelper.PlayerUpgradeStrengthName}]` - player's upgrade strength level.\r\n" +
            $"`[{ExpressionHelper.PlayerUpgradeThrowName}]` - player's upgrade throw level.\r\n" +
            $"`[{ExpressionHelper.PlayerUpgradeRangeName}]` - player's upgrade range level.\r\n" +
            "Custom functions (Standard functions can be found here https://github.com/bijington/expressive/wiki/Functions):\r\n" +
            $"`{RandomListFunction.FunctionName}(x1, x2, x3...)` - selects a random number from a list. For example, `RandomList(10, 20)` will randomly return `10` or `20`\r\n" +
            "Examples (quotation marks should be removed):\r\n" +
            $"`Max((([{ExpressionHelper.PlayerMaxHealthName}]*0.8)-[{ExpressionHelper.PlayerHealthName}]),0)` - same as the `ToMaxHealthPercentage` parameter. The player will be healed to 80% of maximum health.\r\n" +
            $"`[{ExpressionHelper.PlayerMaxHealthName}]*0.3` - same as `ByMaxHealthPercentage` parameter. The player will be healed by 30% of maximum health.\r\n" +
            "`40` - same as `ExactValue` parameter. The player will be healed for 40HP.\r\n" +
            "`RandomList(25, 50, 100)` - same as `HealthPackMode` parameter. The player will be randomly healed for 25HP or 50HP or 100HP.\r\n" +
            "`[PlayerMaxHealth] * (If([PlayerCount] <= 2, Random(25, 30), If([PlayerCount] <= 4, Random(35, 40), If([PlayerCount] <= 6, Random(45, 50), Random(55, 60))))/100)` - if there are up to two players, it heals randomly by 25-30 HP. If there are up to four players, it heals randomly by 35-40 HP, and so on.");
    }

    public static ConfigEntry<WorkMode> BindWorkMode(this ConfigFile config, string section)
    {
        return config.Bind(section, "WorkMode", WorkMode.Host,
            "Configures the work mode.\r\n" +
            "Warning: You are using `All` or `Client` values for this parameter at your own risk.\r\n" +
            "It may stop working after future game updates.\r\n" +
            "If another lobby players do not agree with this parameter, do not use it.\r\n" +
            "Also, the incomplete heal may not work correctly with this parameter, since the host or other clients may also perform the heal.\r\n" +
            "`Host` - works only on host.\r\n" +
            "`Client` - works only on client.\r\n" +
            "`All` - works on host and client.");
    }

    public static ConfigEntry<HealMode> BindHealMode(this ConfigFile config, string section)
    {
        return config.Bind(section, "HealMode", HealMode.All,
            "Configures the heal mode.\r\n" +
            "`Self` - heal self.\r\n" +
            "`Others` - heal others.\r\n" +
            "`All` - heal all.");
    }

    public static ConfigEntry<PhaseMode> BindPhaseMode(this ConfigFile config, string section)
    {
        return config.Bind(section, "PhaseMode", PhaseMode.All,
            "Configures in which phases of the game health restoration will be triggered.\r\n" +
            "`Level` - the level.\r\n" +
            "`Shop` - in the shop. Attention! Healing in the shop does not affect the game.\r\n" +
            "`Lobby` - in the truck (not menu) before starting the next level.\r\n" +
            "`Arena` - in the arena.\r\n" +
            "`All` - in all phases.");
    }

    public static ConfigEntry<HealRequirementMode> BindHealRequirementMode(this ConfigFile config, string section)
    {
        return config.Bind(section, "HealRequirementMode", HealRequirementMode.None,
            "Sets requirements for performing heal.\r\n" +
            "`None` - no requirements.\r\n" +
            "`Survive` - the player must return to the truck at the end of the round and survive.");
    }
}