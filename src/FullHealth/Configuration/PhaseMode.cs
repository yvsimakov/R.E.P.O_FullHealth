using System;

namespace FullHealth;

[Flags]
public enum PhaseMode : byte
{
    Level = 1 << 0,
    Shop = 1 << 1,
    Lobby = 1 << 2,
    All = Level | Shop | Lobby
}