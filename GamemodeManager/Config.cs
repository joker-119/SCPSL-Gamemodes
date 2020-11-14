using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Exiled.API.Features;
using Exiled.API.Interfaces;

namespace GamemodeManager
{
    public class Config : IConfig
    {
        [Description("Whether or not events are usable on this server.")]
        public bool IsEnabled { get; set; } = true;
        
        [Description("Whether or not to display debug messages.")]
        public bool Debug { get; set; }

        [Description("The permission string required to enable/disable events.")]
        public string RequiredPermissions { get; set; } = "gamemodes.staff";

        [Description("Where to look for gamemode plugins.")]
        public string GamemodeDirectory { get; set; } = Path.Combine(Paths.Plugins, "Gamemodes");
        
        [Description("The list of plugins to disable before running a gamemode round.")]
        public List<string> DisabledPlugins { get; set; } = new List<string>();
        
        public List<IPlugin<IConfig>> DisabledPluginsList = new List<IPlugin<IConfig>>();

        internal void ParseDisabledPlugins()
        {
            foreach (IPlugin<IConfig> plugin in Exiled.Loader.Loader.Plugins)
                if (DisabledPlugins.Contains(plugin.Name))
                {
                    if (plugin.Name == "Gamemode Manager")
                    {
                        Log.Warn($"You cannot set GMM to be disabled when a gamemode is active.");
                        continue;
                    }
                    
                    Log.Debug($"Adding {plugin.Name} - {plugin.Version} ({plugin.Author} to disabled plugins list.", Debug);
                    DisabledPluginsList.Add(plugin);
                }
        }
    }
}