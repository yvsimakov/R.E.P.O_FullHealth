using BepInEx.Configuration;

namespace FullHealth;

public static class Configuration
{
    public static ConfigEntry<bool> Enabled { get; set; }
    public static ConfigEntry<decimal> Percent { get; set; }
}