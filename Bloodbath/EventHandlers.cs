using Exiled.API.Features;
using Exiled.Events.EventArgs;

namespace Bloodbath
{
    public class EventHandlers
    {
        private readonly Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        public void OnRoundStart()
        {
            if (!plugin.IsEnabled)
                return;

            plugin.IsRunning = true;
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

        public void OnPlayerDeath(DiedEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;
            
            int counter = 0;
            foreach (Player player in Player.List)
                if (player.Role == RoleType.ClassD)
                    counter++;

            switch (counter)
            {
                case 1:
                    plugin.Methods.EndRound();
                    break;
                default:
                    if (!string.IsNullOrEmpty(plugin.Config.RemainingBroadcast) &&
                        plugin.Config.RemainingBroadcastDur > 0)
                    {
                        Map.ClearBroadcasts();
                        Map.Broadcast(plugin.Config.RemainingBroadcastDur, plugin.Config.RemainingBroadcast.Replace("%count", $"{counter}"));
                    }

                    break;
            }
        }

        public void OnPlayerJoin(JoinedEventArgs ev)
        {
            if (plugin.IsRunning || plugin.IsEnabled && !Round.IsStarted)
                ev.Player.Broadcast(5, $"Welcome to the <color=red>{GetType().Namespace}</color>!");
        }
    }
}