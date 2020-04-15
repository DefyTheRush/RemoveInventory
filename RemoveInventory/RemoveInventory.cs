using System;
using EXILED;
namespace RemoveInventory
{
    public class RemoveInventory : EXILED.Plugin
    {
        bool IsPluginEnabled;
        public RemoveInventoryHandler RIHandle;
        public override string getName => "RemoveInventory - By DefyTheRush";

        public void ReloadConfig()
        {
            IsPluginEnabled = Config.GetBool("removeinventory_enable");
            if (!IsPluginEnabled)
                Log.Info("Plugin disabled!");
        }

        public override void OnDisable()
        {
            Log.Info("Disabling \"RemoveInventory\"!");
            Events.RemoteAdminCommandEvent -= RIHandle.RunOnRACommandSent;
            RIHandle = null;
        }

        public override void OnEnable()
        {
            ReloadConfig();
            if (!IsPluginEnabled)
                return;

            Log.Info("Starting up \"RemoveInventory\"! (Created by DefyTheRush)");
            RIHandle = new RemoveInventoryHandler();
            Events.RemoteAdminCommandEvent += RIHandle.RunOnRACommandSent;

        }

        public override void OnReload() { }
    }
}
