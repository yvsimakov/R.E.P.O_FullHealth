using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BepInEx.Configuration;

namespace FullHealth;

public static class ConfigurationHelper
{
    public static void Bind(ConfigFile config)
    {
        Configuration.Enabled = config.Bind("General", "Enabled", true,
            "Indicates whether the mod is enabled.");
        Configuration.ToMaxHealthPercentage = config.Bind("General", "ToMaxHealthPercentage", 100M,
            "The percentage of the player's maximum health to which he will be healed (Min: 0, Max: 100).\r\n" +
            "Attention! You must set the `PhaseMode` parameter to only one of `Level` or `Lobby`, not both. Otherwise, the healing will be done twice.");
        Configuration.ByMaxHealthPercentage = config.Bind("General", "ByMaxHealthPercentage", 0M,
            "The percentage of the player's maximum health that the player will be healed to (Min: 0, Max: 100).\r\n" +
            "Attention! You must set the `PhaseMode` parameter to only one of `Level` or `Lobby`, not both. Otherwise, the healing will be done twice.\r\n" +
            "It has priority over the `ToMaxHealthPercentage` parameter.");
        Configuration.ExactValue = config.Bind("General", "ExactValue", 0,
            "Sets the exact value to heal.\r\n" +
            "Attention! You must set the `PhaseMode` parameter to only one of `Level` or `Lobby`, not both. Otherwise, the healing will be done twice.\r\n" +
            "If the value is `0`, then it is disabled.\r\n" +
            "It has priority over the `ToMaxHealthPercentage` and `ByMaxHealthPercentage` parameters.\r\n" +
            "For example, if you set the value to `15`, you will always receive 15 HP.");
        Configuration.HealthPackMode = config.Bind("General", "HealthPackMode", HealthPackMode.None,
            "Sets healing value options to the same as in health packs, one of which will be applied randomly.\r\n" +
            "Attention! You must set the `PhaseMode` parameter to only one of `Level` or `Lobby`, not both. Otherwise, the healing will be done twice.\r\n" +
            "If the value is `None`, then it is disabled.\r\n" +
            "It has priority over the `ToMaxHealthPercentage` and `ByMaxHealthPercentage` and `ExactValue` parameters.\r\n" +
            "For example, if you set the value to `Medium, Large`, you will receive a random healing of 50HP or 100HP.\r\n" +
            "`Small` - heal 25 HP.\r\n" +
            "`Medium` - heal 50 HP.\r\n" +
            "`Large` - heal 100 HP.");
        Configuration.Expression = config.Bind<string>("General", "Expression", null,
            "The expression used to calculate the healing value.\r\n" +
            "Attention! You must set the `PhaseMode` parameter to only one of `Level` or `Lobby`, not both. Otherwise, the healing will be done twice.\r\n" +
            "If the value is null or empty string or whitespace, then it is disabled.\r\n" +
            "It has priority over the `ToMaxHealthPercentage` and `ByMaxHealthPercentage` and `ExactValue` and `HealthPackMode` parameters.\r\n" +
            "The library used to parse expression is https://mathparser.org/ . You can find detailed information in the documentation.\r\n" +
            "Variables:\r\n" +
            $"{ExpressionHelper.PlayerMaxHealthConstName} - player's maximum health.\r\n" +
            $"{ExpressionHelper.PlayerHealthConstName} - player's health.\r\n" +
            $"{ExpressionHelper.PlayerCountConstName} - number of players.\r\n" +
            "Examples (quotation marks should be removed):\r\n" +
            $"`max((({ExpressionHelper.PlayerMaxHealthConstName}*0.8)-{ExpressionHelper.PlayerHealthConstName}),0)` - analog of the `ToMaxHealthPercentage` parameter. The player will be healed to 80% of maximum health.\r\n" +
            $"`{ExpressionHelper.PlayerMaxHealthConstName}*0.3` - analog of `ByMaxHealthPercentage` parameter. The player will be healed by 30% of maximum health.\r\n" +
            "`40` - analog of `ExactValue` parameter. The player will be healed for 40HP.\r\n" +
            "`rList(25,50,100)` - analog of `HealthPackMode` parameter. The player will be randomly healed for 25HP or 50HP or 100HP.");
        ExpressionHelper.UpdateExpression(Configuration.Expression.Value);
        Configuration.Expression.SettingChanged += (_, _) => ExpressionHelper.UpdateExpression(Configuration.Expression.Value);
        Configuration.WorkMode = config.Bind("General", "WorkMode", WorkMode.Host,
            "Configures the work mode.\r\n" +
            "Warning: You are using `All` value for this parameter at your own risk.\r\n" +
            "It may stop working after future game updates.\r\n" +
            "If another lobby players do not agree with this parameter, do not use it.\r\n" +
            "Also, the incomplete heal may not work correctly with this parameter, since the host or other clients may also perform the heal.\r\n" +
            "`Host` - works only on host.\r\n" +
            "`Client` - works only on client.\r\n" +
            "`All` - works on host and client.");
        Configuration.HealMode = config.Bind("General", "HealMode", HealMode.All,
            "Configures the heal mode.\r\n" +
            "`Self` - heal self.\r\n" +
            "`Others` - heal others.\r\n" +
            "`All` - heal all.");
        Configuration.PhaseMode = config.Bind("General", "PhaseMode", PhaseMode.All,
            "Configures in which phases of the game health restoration will be triggered.\r\n" +
            "`Level` - when spawning in the level.\r\n" +
            "`Shop` - when spawning in the shop. Attention! Healing in the shop does not affect the game.\r\n" +
            "`Lobby` - when spawning in the truck (not menu) before starting the next level.\r\n" +
            "`Arena` - when spawning in the arena.\r\n" +
            "`All` - in all phases.");
        Configuration.HealRequirementMode = config.Bind("General", "HealRequirementMode", HealRequirementMode.None,
            "Sets requirements for performing heal.\r\n" +
            "`None` - no requirements.\r\n" +
            "`Survive` - the player must return to the truck at the end of the round and survive.");
        UpdateHealthPackModeValues();
        Configuration.HealthPackMode.SettingChanged += (_, _) => UpdateHealthPackModeValues();

        var fixToMaxHealthPercentageValue = Math.Min(100M, Math.Max(0M, Configuration.ToMaxHealthPercentage.Value));
        if (fixToMaxHealthPercentageValue != Configuration.ToMaxHealthPercentage.Value)
        {
            Configuration.ToMaxHealthPercentage.Value = fixToMaxHealthPercentageValue;
        }

        var fixByMaxHealthPercentageValue = Math.Min(100M, Math.Max(0M, Configuration.ByMaxHealthPercentage.Value));
        if (fixByMaxHealthPercentageValue != Configuration.ByMaxHealthPercentage.Value)
        {
            Configuration.ByMaxHealthPercentage.Value = fixByMaxHealthPercentageValue;
        }

        if (Configuration.ExactValue.Value < 0)
        {
            Configuration.ExactValue.Value = 0;
        }

        var configFileEntriesPropertyInfo = typeof(ConfigFile).GetProperty("OrphanedEntries", BindingFlags.NonPublic | BindingFlags.Instance);
        var configFileEntries = (Dictionary<ConfigDefinition, string>)configFileEntriesPropertyInfo?.GetValue(config);
        if (configFileEntries != null && configFileEntries.Remove(new("General", "Percent"), out var entry) && decimal.TryParse(entry, out var decimalEntry))
        {
            Configuration.ToMaxHealthPercentage.Value = Math.Min(100M, Math.Max(0M, decimalEntry));
            config.Save();
            Plugin.Logger.LogInfo("The value of the old variable 'General.Percent' has been copied to the new variable 'General.ToMaxHealthPercentage'");
        }

        if (!string.IsNullOrWhiteSpace(Configuration.Expression.Value))
        {
            const int playerMaxHealth = 100;
            const int playerHealth = 30;
            const int playerCount = 4;
            var result = ExpressionHelper.Calculate("TestPlayer", playerMaxHealth, playerHealth, playerCount);
            if (double.IsNaN(result))
            {
                Plugin.Logger.LogError("An error occurred while testing the heal calculation using the expression. Please fix the expression and restart the game.");
            }
            else
            {
                Plugin.Logger.LogInfo($"Calculation test of the healing completed. " +
                                      $"Variables: " +
                                      $"{ExpressionHelper.PlayerMaxHealthConstName}='{playerMaxHealth}', " +
                                      $"{ExpressionHelper.PlayerHealthConstName}='{playerHealth}', " +
                                      $"{ExpressionHelper.PlayerCountConstName}='{playerCount}'. " +
                                      $"Result: '{result}'");
            }
        }
    }

    private static readonly HealthPackMode[] HealthPackModes = [HealthPackMode.Small, HealthPackMode.Medium, HealthPackMode.Large];

    private static void UpdateHealthPackModeValues()
    {
        var healthPackModeValues = new List<int>();

        foreach (var variant in HealthPackModes.Where(x => Configuration.HealthPackMode.Value.HasFlag(x)))
        {
            switch (variant)
            {
                case HealthPackMode.Small:
                    healthPackModeValues.Add(25);
                    break;
                case HealthPackMode.Medium:
                    healthPackModeValues.Add(50);
                    break;
                case HealthPackMode.Large:
                    healthPackModeValues.Add(100);
                    break;
            }
        }

        Configuration.HealthPackModeValues = healthPackModeValues.ToArray();
    }
}