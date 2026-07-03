using System.Collections.Generic;
using LabApi.Features.Wrappers;

namespace SCP_207Upgr.Features.SCP127
{
    internal class UpgradeManager
    {
        private readonly Dictionary<ushort, Scp127Data> _weapons = new();
        public IEnumerable<Scp127Data> AllWeapons => _weapons.Values;
        public Scp127Data Get(Item item)
        {
            if (!_weapons.TryGetValue(item.Serial, out var data))
            {
                data = new Scp127Data();     
                _weapons.Add(item.Serial, data);
            }

            return data;
        }

        public bool AddUpgrade(Item item, IScp127Upgrade upgrade)
        {
            var data = Get(item);

            if (data.Upgrades.Count >= Config.MaxUpgrades)
                return false;
                
            foreach (var existing in data.Upgrades)
            {
                if (existing.GetType() == upgrade.GetType())
                    return false;
            }

            data.Upgrades.Add(upgrade);
            return true;
        }

        public bool HasUpgrades(Item item)
        {
            return Get(item).Upgrades.Count > 0;
        }


        public bool TryGet(ushort serial, out Scp127Data data)
        {
            return _weapons.TryGetValue(serial, out data);
        }
        public IReadOnlyList<IScp127Upgrade> GetUpgrades(Item item)
        {
            return Get(item).Upgrades;
        }

        public void Remove(Item item)
        {
            _weapons.Remove(item.Serial);
        }
    }
}