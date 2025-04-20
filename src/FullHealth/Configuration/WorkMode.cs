using System;

namespace FullHealth;

public enum WorkMode : byte
{
    Host = 1 << 0,
    Client = 1 << 1,
    All = Host | Client,
    [Obsolete]
    HostAndClient = All
}