using LabApi.Features.Wrappers;
using PlayerRoles.PlayableScps;
using PlayerStatsSystem;

namespace SCP_207Upgr.Features.SCP127.Upgrades
{
    internal class TestUpgrade : IScp127Upgrade
    {
        public string Name => "Test Upgrade";

        public void OnUnlock(Player player)
        {
            player.SendHint(
                "<color=green>Получено улучшение SCP-127!</color>",
                5);
        }

        public void OnApply(Player player)
        {
            player.SendHint(
                "<color=yellow>Улучшение применилось к SCP-127!</color>",
                5);

            player.EnableEffect<CustomPlayerEffects.MovementBoost>(20, 1);
        }

        public void OnRemove(Player player)
        {
            player.DisableEffect<CustomPlayerEffects.MovementBoost>();
        }
    }
}