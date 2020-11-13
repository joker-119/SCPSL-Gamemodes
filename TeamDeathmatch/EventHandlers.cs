using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;

namespace TeamDeathmatch
{
    public class EventHandlers
    {
        private readonly Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        public void OnRoundStart()
        {
            if (!plugin.IsEnabled)
                return;

            plugin.Methods.SetupPlayers();
        }

        public void OnRoundEnd(RoundEndedEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;

            plugin.IsRunning = false;
            if (plugin.ShouldDisableNextRound)
                plugin.IsEnabled = false;
        }
    }
}