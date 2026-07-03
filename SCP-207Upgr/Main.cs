using System;
using LabApi.Loader.Features.Plugins;
using SCP_207Upgr.Features.SCP127;

namespace SCP_207Upgr
{
    internal class Main : Plugin<Config>
    {
        public override string Name => "SCP-207 Upgrade";
        public override string Description => "SCP-207 Upgrade";
        public override string Author => "mrSashaman";
        public override Version Version => new(1, 0, 0);
        public override Version RequiredApiVersion => new(1, 1, 7);
        public static Main Instance { get; private set; }

        public UpgradeManager UpgradeManager { get; private set; }
        private CustomItemManager _customItemManager;
        public CustomItemManager CustomItemManager { get; private set; }
        private Scp127Controller _scp127;
        private CustomItemController _customItems;

        public override void Enable()
        {
            Instance = this;

            UpgradeManager = new UpgradeManager();
            CustomItemManager = new CustomItemManager();

            _scp127 = new Scp127Controller(UpgradeManager);
            _customItems = new CustomItemController(
                UpgradeManager,
                CustomItemManager);

            _scp127.Register();
            _customItems.Register();
        }

        public override void Disable()
        {
            _scp127.Unregister();
            _customItems.Unregister();
        }
    }
}