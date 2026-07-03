using CustomPlayerEffects;
using LabApi.Features.Wrappers;

namespace SCP_207Upgr
{
    internal class SpeedUpgrade : IScp127Upgrade
    {
        public string Name => "Ускоренный метаболизм";

        public string Description =>
            "Пока SCP-127 находится в руках, скорость увеличена.";

        public void OnEquip(Player player)
        {
            player.EnableEffect<MovementBoost>(50, 9999f);
        }

        public void OnUnequip(Player player)
        {
            player.DisableEffect<MovementBoost>();
        }

        public void OnShoot(Player player)
        {
        }

        public void OnKill(Player attacker, Player victim)
        {
        }

        public void OnDrop(Player player)
        {
        }

        public void OnTick(Player player)
        {
        }

        public void OnUnlock(Player player)
        {
            throw new System.NotImplementedException();
        }

        public void OnApply(Player player)
        {
            throw new System.NotImplementedException();
        }

        public void OnRemove(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}