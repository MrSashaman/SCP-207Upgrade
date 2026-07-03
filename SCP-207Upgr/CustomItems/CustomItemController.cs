using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Handlers;
using LabApi.Features.Wrappers;
using SCP_207Upgr.Features.SCP127;

namespace SCP_207Upgr
{
    internal class CustomItemController
    {
        private readonly UpgradeManager _upgradeManager;
        private readonly CustomItemManager _customItemManager;
        public CustomItemController(
            UpgradeManager upgradeManager,
            CustomItemManager customItemManager)
        {
            _upgradeManager = upgradeManager;
            _customItemManager = customItemManager;
        }

        public void Register()
        {
            PlayerEvents.UsingItem += OnUsingItem;
        }

        public void Unregister()
        {
            PlayerEvents.UsingItem -= OnUsingItem;
        }

        private void OnUsingItem(PlayerUsingItemEventArgs ev)
        {
            if (!_customItemManager.TryGetUpgrade(ev.Item, out var upgrade))
                return;

            ev.IsAllowed = false;

            Item scp127 = null;

            foreach (Item item in ev.Player.Items)
            {
                if (item.Type == ItemType.GunSCP127)
                {
                    scp127 = item;
                    break;
                }
            }

            if (scp127 == null)
            {
                ev.Player.SendHint(
                    "<color=red>У вас нет SCP-127 в инвентаре.</color>",
                    5f);

                return;
            }

            
            if (upgrade is KamikazeUpgrade kamikaze)
            {
                   kamikaze.Owner = ev.Player;

                    _upgradeManager.AddUpgrade(scp127, upgrade);         
            }


            if (!_upgradeManager.AddUpgrade(scp127, upgrade))
            {
                ev.Player.SendHint(
                    "<color=red>Такой модификатор уже установлен.</color>",
                    5f);

                return;
            }

            _customItemManager.Remove(ev.Item);
            ev.Player.RemoveItem(ev.Item);

            ev.Player.SendHint(
                "<color=green>Модификатор успешно установлен.</color>",
                5f);
        }
    }
}