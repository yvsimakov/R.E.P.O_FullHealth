using System;
using BepInEx.Configuration;

namespace FullHealth;

public static class ConfigurationHelper
{
    public static void Bind(ConfigFile config)
    {
        Configuration.Enabled = config.Bind("General", "Enabled", true, "Indicates whether the plugin is enabled");
        Configuration.Percent = config.Bind("General", "Percent", 100M, "Percentage of maximum health to which the player will be healed (Min: 0, Max: 100)");
        Configuration.WorkMode = config.Bind("General", "WorkMode", WorkMode.Host,
            "Configures the work mode.\r\n" +
            "Warning: You are using not '0' value for this parameter at your own risk.\r\n" +
            "It may stop working after future game updates.\r\n" +
            "If another lobby players do not agree with this parameter, do not use it.\r\n" +
            "Also, the 'Percent' parameter may not work correctly with this parameter, since the host or other clients may also perform the heal.\r\n" +
            "(Host - works only on host, HostAndClient - works on host and client)");
        Configuration.HealMode = config.Bind("General", "HealMode", HealMode.All,
            "Configures the heal mode.\r\n" +
            "(All - heal all, Self - heal self)");

        var fixPercentValue = Math.Min(100M, Math.Max(0M, Configuration.Percent.Value));
        if (fixPercentValue != Configuration.Percent.Value)
        {
            Configuration.Percent.Value = fixPercentValue;
        }

        var fixedWorkModeValue = Enum.IsDefined(typeof(WorkMode), Configuration.WorkMode.Value) ? Configuration.WorkMode.Value : WorkMode.Host;
        if (fixedWorkModeValue != Configuration.WorkMode.Value)
        {
            Configuration.WorkMode.Value = fixedWorkModeValue;
        }

        var fixedHealMode = Enum.IsDefined(typeof(HealMode), Configuration.HealMode.Value) ? Configuration.HealMode.Value : HealMode.All;
        if (fixedHealMode != Configuration.HealMode.Value)
        {
            Configuration.HealMode.Value = fixedHealMode;
        }
    }
}