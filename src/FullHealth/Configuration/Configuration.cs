using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FullHealth;

public static class Configuration
{
    public static ConfigurationItemStart Start { get; set; } = new();
    public static ConfigurationItem Revival { get; set; } = new();

    public static readonly ReadOnlyDictionary<ConfigurationMode, ConfigurationItem> Items = new(new Dictionary<ConfigurationMode, ConfigurationItem>
    {
        { ConfigurationMode.Start, Start },
        { ConfigurationMode.Revival, Revival }
    });
}