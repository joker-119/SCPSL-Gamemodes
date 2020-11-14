using System.ComponentModel;
using Exiled.API.Interfaces;

namespace Gungame
{
    public class Config : IConfig
    {
        [Description("Whether or not this plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;

        public float UpgradeDelay { get; set; } = 1.5f;
        public float RespawnTime { get; set; } = 5f;
    }
}