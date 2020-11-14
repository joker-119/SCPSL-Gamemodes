using Exiled.API.Interfaces;

namespace Bloodbath
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool SmartScpSelection { get; set; } = true;
        public int MaxScpCount { get; set; } = 3;

        public string RemainingBroadcast { get; set; } = "%count Class-D remaining!";
        public ushort RemainingBroadcastDur { get; set; } = 5;
        
        public string EndRoundBroadcast { get; set; } = "%user is the winner!";
        public ushort EndRoundBroadcastDur { get; set; } = 15;
    }
}