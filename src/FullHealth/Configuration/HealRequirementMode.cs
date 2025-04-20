using System;

namespace FullHealth;

[Flags]
public enum HealRequirementMode : byte
{
    None = 0,
    Survive = 1 << 0
}
