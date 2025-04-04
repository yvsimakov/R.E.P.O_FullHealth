using BepInEx.Configuration;

namespace FullHealth;

public static class Configuration
{
    public static ConfigEntry<bool> Enabled { get; set; }
    public static ConfigEntry<decimal> Percent { get; set; }
    public static ConfigEntry<WorkMode> WorkMode { get; set; }
    public static ConfigEntry<HealMode> HealMode { get; set; }
    public static ConfigEntry<PhaseMode> PhaseMode { get; set; }
    public static ConfigEntry<int> ExactValue { get; set; }
    public static ConfigEntry<HealthPackMode> HealthPackMode { get; set; }
    internal static int[] HealthPackModeValues { get; set; } = [];
}