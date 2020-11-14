using Exiled.API.Interfaces;

namespace Outbreak
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool AllDeathsMakeZombies { get; set; } = true;
        public bool AlphasBreakLockedDoors { get; set; } = true;
        public int InfectionChance { get; set; } = 80;
        public int MaxAlphaCount { get; set; } = 3;
        public int AlphaZombieHealth { get; set; } = 3000;
    }
}