using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FullHealth;

public static class WaitHelper
{
    public static async Task<bool> Wait()
    {
        var cts = new CancellationTokenSource(TimeSpan.FromMinutes(1));

        while (!cts.IsCancellationRequested)
        {
            Plugin.Logger.LogDebug("Waiting for all players to load");
            await Task.Delay(1000, CancellationToken.None);

            if (SemiFunc.RunIsLobby())
            {
                return true;
            }

            if (PlayersHelper.Get().All(x => x.levelAnimationCompleted))
            {
                return true;
            }
        }

        Plugin.Logger.LogDebug("Timeout exceeded, operation cancelled");
        return false;
    }
}