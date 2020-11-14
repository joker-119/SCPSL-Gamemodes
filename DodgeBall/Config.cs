using System.ComponentModel;
using Exiled.API.Interfaces;

namespace DodgeBall
{
    public class Config : IConfig
    {
        [Description("Whether or not this plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;

        public float NewDodgeDelay { get; set; } = 5.5f;
    }
}