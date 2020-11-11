using Exiled.API.Interfaces;

namespace Outbreak
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; }
        public bool AllDeathsMakeZombies { get; set; } = true;
        public bool AlphasBreakLockedDoors { get; set; } = true;
        public int InfectionChance { get; set; } = 80;
        public int MaxAlphaCount { get; set; } = 3;
        public int AlphaZombieHealth { get; set; } = 3000;
        public float WaitTime { get; set; } = 60f;
        public string EndRoundBroadcast { get; set; } = "%user is the winner!";
        public ushort EndRoundBroadcastDur { get; set; } = 15;
    }
}