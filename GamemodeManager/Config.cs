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
        public string GamemodeDirectory { get; set; } = Path.Combine(Paths.Plugins, "GamemodePlugins");
    }
}