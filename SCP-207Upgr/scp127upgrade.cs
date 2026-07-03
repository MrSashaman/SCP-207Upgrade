using System;
using System.Linq;
using CommandSystem;
using LabApi.Features.Wrappers;
using RemoteAdmin;
using SCP_207Upgr.Features.SCP127.Upgrades;

namespace SCP_207Upgr.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class GiveScp127UpgradeCommand : ICommand
    {
        public string Command => "scp127upgrade";

        public string[] Aliases => Array.Empty<string>();

        public string Description => "Выдать модификатор SCP-127.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is not PlayerCommandSender playerSender)
            {
                response = "Команду может использовать только игрок.";
                return false;
            }

            Player player = Player.Get(playerSender);

            if (arguments.Count == 0)
            {
                response =
                    "Использование:\n" +
                    "scp127upgrade <id>\n\n" +
                    "Доступные улучшения:\n" +
                    string.Join("\n", UpgradeRegistry.Ids.OrderBy(x => x));

                return false;
            }

            string id = arguments.At(0);

            if (!UpgradeRegistry.TryCreate(id, out var upgrade))
            {
                response =
                    $"Неизвестное улучшение '{id}'.\n\n" +
                    "Доступные:\n" +
                    string.Join("\n", UpgradeRegistry.Ids.OrderBy(x => x));

                return false;
            }

            Main.Instance.CustomItemManager.GiveUpgradeItem(player, upgrade);

            response = $"<color=green>Выдан модификатор '{upgrade.Name}'.</color>";
            return true;
        }
    }
}