using BepInEx.Configuration;

namespace FullHealth;

public class ConfigurationItemStart : ConfigurationItem
{
    public ConfigEntry<HealRequirementMode> HealRequirementMode { get; set; }
}