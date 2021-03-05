using System.ComponentModel;
using Exiled.API.Interfaces;

namespace Blackout
{
    public class Config : IConfig
    {
        [Description("Whether or not this gamemode is availible on the server.")]
        public bool IsEnabled { get; set; } = true;
        [Description("The maximum number of SCP's allowed to spawn at the start of the round.")]
        public int MaxScpCount { get; set; } = 5;
        [Description("Whether or not the plugin should select the number of SCP's based on the server's current player count.")]
        public bool SmartScpSelection { get; set; } = true;
        [Description("The SCP's that should spawn at the start of the round.")]
        public RoleType ScpToSpawn { get; set; } = RoleType.Scp173;
    }
}
