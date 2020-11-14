using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Loader;
using MEC;

namespace GamemodeManager
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "Galaxy119";
        public override string Name { get; } = "Gamemode Manager";
        public override string Prefix { get; } = "gamemode_manager";
        public override Version Version { get; } = new Version(1,1,1);
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 12);
        public bool ShouldDisablePlugins { get; set; }

        public static Plugin Singleton;

        public Methods Methods;
        public QueueHandler.QueueHandler QueueHandler;
        
        public Dictionary<IPlugin<IConfig>, List<ICommand>> LoadedGamemodes = new Dictionary<IPlugin<IConfig>, List<ICommand>>();
        public Dictionary<IPlugin<IConfig>, ICommand> GamemodeEnableCommands = new Dictionary<IPlugin<IConfig>, ICommand>();
        public bool PluginsDisabled = false;

        public override void OnEnabled()
        {
            Singleton = this;
            Methods = new Methods(this);
            QueueHandler = new QueueHandler.QueueHandler(this);

            Timing.CallDelayed(3f, () => Config.ParseDisabledPlugins());
            
            if (!Directory.Exists(Config.GamemodeDirectory))
            {
                Log.Warn($"Gamemode directory: {Config.GamemodeDirectory} doesn't exist!");
                return;
            }

            Timing.CallDelayed(3f, () => Methods.LoadGamemodes());
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            QueueHandler.Destroy();
            
            foreach (KeyValuePair<IPlugin<IConfig>, List<ICommand>> plugin in LoadedGamemodes)
            {
                plugin.Key.OnDisabled();
                Log.Info($"Disabled {plugin.Key.Name}.");
            }

            base.OnDisabled();
        }
    }
}