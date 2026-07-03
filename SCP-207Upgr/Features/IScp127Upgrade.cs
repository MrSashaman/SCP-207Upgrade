using LabApi.Features.Wrappers;

namespace SCP_207Upgr
{
    internal interface IScp127Upgrade
    {
        string Name { get; }

        string Description { get; }

        void OnEquip(Player player);

        void OnUnequip(Player player);

        void OnShoot(Player player);

        void OnKill(Player attacker, Player victim);

        void OnDrop(Player player);

        void OnTick(Player player);
    }
}