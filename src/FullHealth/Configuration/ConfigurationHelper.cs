using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;

namespace FullHealth;

public static class ConfigurationHelper
{
    public static void Bind(ConfigFile config)
    {
        Configuration.Enabled = config.Bind("General", "Enabled", true,
            "Indicates whether the mod is enabled.");
        Configuration.Percent = config.Bind("General", "Percent", 100M,
            "Percentage of maximum health to which the player will be healed (Min: 0, Max: 100).\r\n" +
            "Attention! You must set the `PhaseMode` parameter to only one of `Level` or `Lobby`, not both. Otherwise, the healing will be done twice.\r\n");
        Configuration.WorkMode = config.Bind("General", "WorkMode", WorkMode.Host,
            "Configures the work mode.\r\n" +
            "Warning: You are using `All` value for this parameter at your own risk.\r\n" +
            "It may stop working after future game updates.\r\n" +
            "If another lobby players do not agree with this parameter, do not use it.\r\n" +
            "Also, the `Percent` parameter may not work correctly with this parameter, since the host or other clients may also perform the heal.\r\n" +
            "Host - works only on host.\r\n" +
            "Client - works only on client.\r\n" +
            "All - works on host and client.\r\n");
        Configuration.HealMode = config.Bind("General", "HealMode", HealMode.All,
            "Configures the heal mode.\r\n" +
            "Self - heal self.\r\n" +
            "Others - heal others.\r\n" +
            "All - heal all.");
        Configuration.PhaseMode = config.Bind("General", "PhaseMode", PhaseMode.All,
            "Configures in which phases of the game health restoration will be triggered.\r\n" +
            "Level - when spawning in the level.\r\n" +
            "Shop - when spawning in the shop. Attention! Healing in the shop does not affect the game.\r\n" +
            "Lobby - when spawning in the truck (not menu) before starting the next level.\r\n" +
            "Arena - when spawning in the arena.\r\n" +
            "All - in all phases.");
        Configuration.ExactValue = config.Bind("General", "ExactValue", 0,
            "Sets the exact value to heal.\r\n" +
            "Attention! You must set the `PhaseMode` parameter to only one of `Level` or `Lobby`, not both. Otherwise, the healing will be done twice.\r\n" +
            "If the value is `0`, then it is disabled.\r\n" +
            "It has priority over the `Percent` parameter.\r\n" +
            "For example, if you set the value to `15`, you will always receive 15 HP.");
        Configuration.HealthPackMode = config.Bind("General", "HealthPackMode", HealthPackMode.None,
            "Sets healing value options to the same as in health packs, one of which will be applied randomly.\r\n" +
            "Attention! You must set the `PhaseMode` parameter to only one of `Level` or `Lobby`, not both. Otherwise, the healing will be done twice.\r\n" +
            "If the value is `None`, then it is disabled.\r\n" +
            "It has priority over the `Percent` and `ExactValue` parameters.\r\n" +
            "For example, if you set the value to `Medium, Large`, you will receive a random healing of 50HP or 100HP.\r\n" +
            "Small - heal 25 HP.\r\n" +
            "Medium - heal 50 HP.\r\n" +
            "Large - heal 100 HP.");
        Configuration.HealRequirementMode = config.Bind("General", "HealRequirementMode", HealRequirementMode.None,
            "Sets requirements for performing heal.\r\n" +
            "None - no requirements.\r\n" +
            "Survive - the player must return to the truck at the end of the round and survive.");
        UpdateHealthPackModeValues();
        Configuration.HealthPackMode.SettingChanged += (_, _) => UpdateHealthPackModeValues();

        var fixPercentValue = Math.Min(100M, Math.Max(0M, Configuration.Percent.Value));
        if (fixPercentValue != Configuration.Percent.Value)
        {
            Configuration.Percent.Value = fixPercentValue;
        }

        if (Configuration.ExactValue.Value < 0)
        {
            Configuration.ExactValue.Value = 0;
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