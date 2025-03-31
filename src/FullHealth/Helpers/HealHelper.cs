using System.Threading.Tasks;

namespace FullHealth;

public static class HealHelper
{
    public static async Task Heal()
    {
        if (!await WaitHelper.Wait())
        {
            return;
        }

        switch (Configuration.HealMode.Value)
        {
            case HealMode.All:
            {
                foreach (var player in PlayersHelper.Get())
                {
                    HealPlayerHelper.Heal(player);
                }

                break;
            }
            case HealMode.Self:
            {
                var player = SemiFunc.PlayerAvatarLocal();
                if (player == null)
                {
                    Plugin.Logger.LogWarning("An error occurred while retrieving data about yourself");
                    return;
                }

                HealPlayerHelper.Heal(SemiFunc.PlayerAvatarLocal());
                break;
            }
            default:
                Plugin.Logger.LogWarning("Unknown HealMode value");
                break;
        }
    }
}