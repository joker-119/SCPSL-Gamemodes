using Exiled.API.Interfaces;

namespace PeanutInfection
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool SmartScpSelection { get; set; } = true;
        public int MaxStartingScpCount { get; set; } = 3;
    }
}