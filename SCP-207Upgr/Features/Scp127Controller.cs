using System;
using System.Collections.Generic;
using System.Text;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Handlers;
using LabApi.Features.Wrappers;
using MEC;
using SCP_207Upgr.Features.SCP127;

namespace SCP_207Upgr
{
    internal class Scp127Controller
    {
        private readonly UpgradeManager _upgradeManager;

        private readonly Dictionary<Player, CoroutineHandle> _weaponCoroutines = new();
        private readonly Random _random = new();

        public Scp127Controller(UpgradeManager upgradeManager)
        {
            _upgradeManager = upgradeManager;
        }

        public void Register()
        {
            PlayerEvents.ChangedItem += OnChangedItem;   
            PlayerEvents.DroppingItem += OnDroppingItem;
            PlayerEvents.Death += OnDied;
            PlayerEvents.PickingUpItem += OnPickingUpItem;
        }

        public void Unregister()
        {
            PlayerEvents.ChangedItem -= OnChangedItem;
            PlayerEvents.DroppingItem -= OnDroppingItem;
            PlayerEvents.Death -= OnDied;
            PlayerEvents.PickingUpItem -= OnPickingUpItem;
            foreach (var coroutine in _weaponCoroutines.Values)
                Timing.KillCoroutines(coroutine);


            _weaponCoroutines.Clear();
        }




        
        private void OnChangedItem(PlayerChangedItemEventArgs ev)
        {
            StopWeapon(ev.Player);

            Item weapon = ev.Player.CurrentItem;

            if (weapon == null)
                return;

            if (weapon.Type != ItemType.GunSCP127)
                return;

            if (_upgradeManager.HasUpgrades(weapon))
            {
                foreach (var upgrade in _upgradeManager.GetUpgrades(weapon))
                    upgrade.OnEquip(ev.Player);
            }

            _weaponCoroutines[ev.Player] =
                Timing.RunCoroutine(WeaponCoroutine(ev.Player, weapon));
        }




        private void OnPickingUpItem(PlayerPickingUpItemEventArgs ev)
        {
            ushort serial = ev.Pickup.Serial;
            if (!_upgradeManager.TryGet(serial, out var data))
                return;

            if (data.Upgrades.Count == 0)
                return;

            foreach (var upgrade in data.Upgrades)
            {
                if (upgrade is not KamikazeUpgrade kamikaze)
                    continue;

                if (kamikaze.Owner != null &&
                    kamikaze.Owner == ev.Player)
                    return;

                ev.Player.Kill(
                    "Вы попытались подобрать SCP-127, модифицированный улучшением «Камикадзе».");

                return;
            }
        }





        private void OnDroppingItem(PlayerDroppingItemEventArgs ev)
        {
            if (ev.Item.Type != ItemType.GunSCP127)
                return;

            if (_upgradeManager.HasUpgrades(ev.Item))
            {
                foreach (var upgrade in _upgradeManager.GetUpgrades(ev.Item))
                {
                    upgrade.OnDrop(ev.Player);
                    upgrade.OnUnequip(ev.Player);
                }
            }

            StopWeapon(ev.Player);
        }

        private void OnDied(PlayerDeathEventArgs ev)
        {
            StopWeapon(ev.Player);

            foreach (var weapon in _upgradeManager.AllWeapons)
            {
                foreach (var upgrade in weapon.Upgrades)
                {
                    if (upgrade is KamikazeUpgrade kamikaze &&
                        kamikaze.Owner == ev.Player)
                    {
                        kamikaze.Owner = null;
                    }
                }
            }
        }



        private void StopWeapon(Player player)
        {
            if (player.CurrentItem?.Type == ItemType.GunSCP127 &&
                _upgradeManager.HasUpgrades(player.CurrentItem))
            {
                foreach (var upgrade in _upgradeManager.GetUpgrades(player.CurrentItem))
                    upgrade.OnUnequip(player);
            }

            if (_weaponCoroutines.TryGetValue(player, out var coroutine))
            {
                Timing.KillCoroutines(coroutine);
                _weaponCoroutines.Remove(player);
            }
        }
    
        private IEnumerator<float> WeaponCoroutine(Player player, Item weapon)
        {
            float nextPhrase = 10f;

            while (player != null &&
                player.IsAlive &&
                player.CurrentItem != null &&
                player.CurrentItem.Serial == weapon.Serial)
            {
                if (_upgradeManager.HasUpgrades(weapon))
                {
                    foreach (var upgrade in _upgradeManager.GetUpgrades(weapon))
                        upgrade.OnTick(player);
                }

                nextPhrase -= 1f;

                if (nextPhrase <= 0f)
                {
                    player.SendHint(
                        Scp127Phrases.List[_random.Next(Scp127Phrases.List.Count)],
                        4f);

                    nextPhrase = (float)(_random.NextDouble() * 30f + 30f);

                    yield return Timing.WaitForSeconds(4f);

                    continue;
                }

                StringBuilder sb = new();

                sb.AppendLine("<size=28><b>SCP-127</b></size>");
                sb.AppendLine("<color=#666666>────────────────</color>");

                if (_upgradeManager.HasUpgrades(weapon))
                {
                    foreach (var upgrade in _upgradeManager.GetUpgrades(weapon))
                    {
                        sb.AppendLine();
                        sb.AppendLine($"<color=#57ff7e>• {upgrade.Name}</color>");


                    }
                }
                else
                {
                    sb.AppendLine();
                    sb.AppendLine("<color=#888888>Без модификаций</color>");
                }

                player.SendHint(sb.ToString(), 1.2f);

                yield return Timing.WaitForSeconds(1f);
            }

            _weaponCoroutines.Remove(player);
        }
    }
}