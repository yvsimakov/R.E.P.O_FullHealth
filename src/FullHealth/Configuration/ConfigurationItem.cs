using BepInEx.Configuration;

namespace FullHealth;

public class ConfigurationItem
{
    public ConfigEntry<bool> Enabled { get; set; }
    public ConfigEntry<decimal> ToMaxHealthPercentage { get; set; }
    public ConfigEntry<decimal> ByMaxHealthPercentage { get; set; }
    public ConfigEntry<WorkMode> WorkMode { get; set; }
    public ConfigEntry<HealMode> HealMode { get; set; }
    public ConfigEntry<int> ExactValue { get; set; }
    public ConfigEntry<HealthPackMode> HealthPackMode { get; set; }
    public ConfigEntry<string> Expression { get; set; }
    public ConfigEntry<PhaseMode> PhaseMode { get; set; }
    internal int[] HealthPackModeValues { get; set; } = [];
}