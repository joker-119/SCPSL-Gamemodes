using Exiled.API.Interfaces;

namespace Blackout
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public int MaxScpCount { get; set; } = 5;
        public bool SmartScpSelection { get; set; } = true;

        public RoleType ScpToSpawn { get; set; } = RoleType.Scp173;
    }
}