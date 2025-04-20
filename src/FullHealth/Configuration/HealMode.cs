using System;

namespace FullHealth;

[Flags]
public enum HealMode : byte
{
    Self = 1 << 0,
    Others = 1 << 1,
    All = Self | Others
}