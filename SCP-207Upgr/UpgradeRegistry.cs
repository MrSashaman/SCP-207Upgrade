using System;
using System.Collections.Generic;
using SCP_207Upgr.Features.SCP127;

namespace SCP_207Upgr
{
    internal static class UpgradeRegistry
    {
        private static readonly Dictionary<string, Func<IScp127Upgrade>> _upgrades =
            new(StringComparer.OrdinalIgnoreCase)
            {
                { "speed", () => new SpeedUpgrade() },
                { "1", () => new SpeedUpgrade() },

                { "regen", () => new RegenerationUpgrade() },
                { "2", () => new RegenerationUpgrade() },

                 { "Kamikaze", () => new KamikazeUpgrade() },
                 { "3", () => new KamikazeUpgrade() },
            };

        public static bool TryCreate(string id, out IScp127Upgrade upgrade)
        {
            if (_upgrades.TryGetValue(id, out var ctor))
            {
                upgrade = ctor();
                return true;
            }

            upgrade = null;
            return false;
        }

        public static IEnumerable<string> Ids => _upgrades.Keys;
    }
}