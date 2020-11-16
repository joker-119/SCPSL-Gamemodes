using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;

namespace DodgeBall
{
    public class EventHandlers
    {
        private readonly Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        public void OnRoundStart()
        {
            if (!plugin.IsEnabled)
                return;

            Timing.CallDelayed(1f, () => plugin.Methods.SetupPlayers());
        }

        public void OnRoundEnd(RoundEndedEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;

            plugin.IsRunning = false;
            
            if (plugin.ShouldDisableNextRound)
                plugin.IsEnabled = false;
            
            if (plugin.Coroutine.IsValid)
                Timing.KillCoroutines(plugin.Coroutine);
        }
    }
}