using LabApi.Features.Wrappers;

namespace SCP_207Upgr.Features.SCP127.Upgrades
{
    internal interface IScp127Upgrade
    {
        string Name { get; }

        void OnUnlock(Player player);

        void OnApply(Player player);

        void OnRemove(Player player);
    }
}