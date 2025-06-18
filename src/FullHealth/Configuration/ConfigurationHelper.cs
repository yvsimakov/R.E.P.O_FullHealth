using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;

namespace FullHealth;

public static class ConfigurationHelper
{
    public static void Bind(ConfigFile config)
    {
        ConfigureStart(config);
        ConfigureRevival(config);
    }

    private static void ConfigureItem(ConfigurationMode mode,
        string section,
        ConfigFile config,
        bool enabledDefault)
    {
        // Enabled
        Configuration.Items[mode].Enabled = config.Bind(section, "Enabled", enabledDefault, "Indicates whether the heal is enabled.");

        // ToMaxHealthPercentage
        Configuration.Items[mode].ToMaxHealthPercentage = config.BindToMaxHealthPercentage(section);
        var fixToMaxHealthPercentageValue = Math.Min(100M, Math.Max(0M, Configuration.Items[mode].ToMaxHealthPercentage.Value));
        if (fixToMaxHealthPercentageValue != Configuration.Items[mode].ToMaxHealthPercentage.Value)
        {
            Configuration.Items[mode].ToMaxHealthPercentage.Value = fixToMaxHealthPercentageValue;
        }

        // ByMaxHealthPercentage
        Configuration.Items[mode].ByMaxHealthPercentage = config.BindByMaxHealthPercentage(section);
        var fixByMaxHealthPercentageValue = Math.Min(100M, Math.Max(0M, Configuration.Items[mode].ByMaxHealthPercentage.Value));
        if (fixByMaxHealthPercentageValue != Configuration.Items[mode].ByMaxHealthPercentage.Value)
        {
            Configuration.Items[mode].ByMaxHealthPercentage.Value = fixByMaxHealthPercentageValue;
        }

        // ExactValue
        Configuration.Items[mode].ExactValue = config.BindExactValue(section);
        if (Configuration.Items[mode].ExactValue.Value < 0)
        {
            Configuration.Items[mode].ExactValue.Value = 0;
        }

        // HealthPackMode
        Configuration.Items[mode].HealthPackMode = config.BindHealthPackMode(section);
        UpdateHealthPackModeValues(mode);
        Configuration.Items[mode].HealthPackMode.SettingChanged += (_, _) => UpdateHealthPackModeValues(mode);
        
        // Expression
        Configuration.Items[mode].Expression = config.BindExpression(section);
        ExpressionHelper.UpdateExpression(mode, Configuration.Items[mode].Expression.Value);
        Configuration.Items[mode].Expression.SettingChanged += (_, _) => ExpressionHelper.UpdateExpression(mode, Configuration.Items[mode].Expression.Value);
        
        if (!string.IsNullOrWhiteSpace(Configuration.Items[mode].Expression.Value))
        {
            const int playerMaxHealth = 100;
            const int playerHealth = 30;
            const int playerCount = 4;

            try
            {
                var result = ExpressionHelper.Calculate(mode, "TestPlayer", playerMaxHealth, playerHealth, Guid.NewGuid().ToString(), playerCount);
                Plugin.Logger.LogInfo($"Calculation test of the healing completed. " +
                                      $"Variables: " +
                                      $"{ExpressionHelper.PlayerMaxHealthName}='{playerMaxHealth}', " +
                                      $"{ExpressionHelper.PlayerHealthName}='{playerHealth}', " +
                                      $"{ExpressionHelper.PlayerCountName}='{playerCount}'. " +
                                      $"Result: '{result}'");
            }
            catch (Exception e)
            {
                Plugin.Logger.LogError("An error occurred while calculating the heal value. Please check the expression: " + e.Message);
            }
        }

        // WorkMode
        Configuration.Items[mode].WorkMode = config.BindWorkMode(section);
        
        // HealMode
        Configuration.Items[mode].HealMode = config.BindHealMode(section); 
        
        // PhaseMode
        Configuration.Items[mode].PhaseMode = config.BindPhaseMode(section);
    }

    private static void ConfigureStart(ConfigFile config)
    {
        const string section = "General";

        ConfigureItem(ConfigurationMode.Start, section, config, true);

        Configuration.Start.HealRequirementMode = config.BindHealRequirementMode(section);
    }

    private static void ConfigureRevival(ConfigFile config)
    {
        const string section = "Revival";

        ConfigureItem(ConfigurationMode.Revival, section, config, false);
    }

    private static readonly HealthPackMode[] HealthPackModes = [HealthPackMode.Small, HealthPackMode.Medium, HealthPackMode.Large];

    private static void UpdateHealthPackModeValues(ConfigurationMode mode)
    {
        var healthPackModeValues = new List<int>();

        foreach (var variant in HealthPackModes.Where(x => Configuration.Items[mode].HealthPackMode.Value.HasFlag(x)))
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

        Configuration.Items[mode].HealthPackModeValues = healthPackModeValues.ToArray();
    }
}