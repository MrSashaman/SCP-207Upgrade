using CustomPlayerEffects;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using SCP_207Upgr;

internal class KamikazeUpgrade : IScp127Upgrade
{
    public string Name => "Камикадзе";

    public string Description =>
        "Любой, кто поднимет SCP-127, погибнет.";

    public Player Owner { get; set; }

    public void OnEquip(Player player)
    {
        Logger.Debug("Камикадзе");
    }

    public void OnUnequip(Player player)
    {
    }

    public void OnTick(Player player)
    {
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
    public void OnApply(Player player)
    {
            throw new System.NotImplementedException();
    }

    public void OnRemove(Player player)
    {
            throw new System.NotImplementedException();
    }

}