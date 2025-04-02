using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BepInEx.Configuration;

namespace FullHealth;

public static class ConfigurationHelper
{
    public static void Bind(ConfigFile config)
    {
        Configuration.Enabled = config.Bind("General", "Enabled", true, "Indicates whether the mod is enabled");
        Configuration.Percent = config.Bind("General", "Percent", 100M, "Percentage of maximum health to which the player will be healed (Min: 0, Max: 100)");
        Configuration.WorkMode = config.Bind("General", "WorkMode", WorkMode.Host,
            "Configures the work mode.\r\n" +
            "Warning: You are using 'HostAndClient' value for this parameter at your own risk.\r\n" +
            "It may stop working after future game updates.\r\n" +
            "If another lobby players do not agree with this parameter, do not use it.\r\n" +
            "Also, the 'Percent' parameter may not work correctly with this parameter, since the host or other clients may also perform the heal.\r\n" +
            "(Host - works only on host, HostAndClient - works on host and client)");
        Configuration.HealMode = config.Bind("General", "HealMode", HealMode.All,
            "Configures the heal mode.\r\n" +
            "(All - heal all, Self - heal self)");
        Configuration.PhaseMode = config.Bind("General", "PhaseMode", PhaseMode.All,
            "Configures in which phases of the game health restoration will be triggered.\r\n" +
            "Level - when spawning in the level.\r\n" +
            "Shop - when spawning in the shop. Attention! Healing in the store does not affect the game.\r\n" +
            "Lobby - when spawning in the truck (not menu) before starting the next level.\r\n" +
            "All - in all phases.");

        var fixPercentValue = Math.Min(100M, Math.Max(0M, Configuration.Percent.Value));
        if (fixPercentValue != Configuration.Percent.Value)
        {
            Configuration.Percent.Value = fixPercentValue;
        }
    }
}