using System.Collections.Generic;
using LabApi.Features.Wrappers;
using SCP_207Upgr.Features.SCP127.Upgrades;

namespace SCP_207Upgr
{
    internal class CustomItemManager
    {
        private readonly Dictionary<ushort, IScp127Upgrade> _items = new();

        public void Register(Item item, IScp127Upgrade upgrade)
        {
            _items[item.Serial] = upgrade;
        }

        public bool TryGetUpgrade(Item item, out IScp127Upgrade upgrade)
        {
            return _items.TryGetValue(item.Serial, out upgrade);
        }

        public void Remove(Item item)
        {
            _items.Remove(item.Serial);
        }

        public void GiveUpgradeItem(Player player, IScp127Upgrade upgrade)
        {
            Item item = player.AddItem(ItemType.Medkit);

            Register(item, upgrade);
        }
    }
}