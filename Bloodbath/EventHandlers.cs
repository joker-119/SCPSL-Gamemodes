using Exiled.API.Features;
using Exiled.Events.EventArgs;

namespace Bloodbath
{
    public class EventHandlers
    {
        private readonly Plugin _plugin;
        public EventHandlers(Plugin plugin) => _plugin = plugin;

        public void OnRoundStart()
        {
            if (!_plugin.IsEnabled)
                return;

            _plugin.IsRunning = true;
            _plugin.Methods.SetupPlayers();
        }

        public void OnRoundEnd(RoundEndedEventArgs ev)
        {
            if (!_plugin.IsRunning)
                return;
            
            _plugin.IsRunning = false;
            if (_plugin.ShouldDisableNextRound)
                _plugin.IsEnabled = false;
        }

        public void OnPlayerDeath(DiedEventArgs ev)
        {
            if (!_plugin.IsRunning)
                return;
            
            int counter = 0;
            foreach (Player player in Player.List)
                if (player.Role == RoleType.ClassD)
                    counter++;

            switch (counter)
            {
                case 1:
                    _plugin.Methods.EndRound();
                    break;
                default:
                    if (!string.IsNullOrEmpty(_plugin.Config.RemainingBroadcast) &&
                        _plugin.Config.RemainingBroadcastDur > 0)
                    {
                        Map.ClearBroadcasts();
                        Map.Broadcast(_plugin.Config.RemainingBroadcastDur, _plugin.Config.RemainingBroadcast.Replace("%count", $"{counter}"));
                    }

                    break;
            }
        }

        public void OnPlayerJoin(JoinedEventArgs ev)
        {
            if (_plugin.IsRunning || _plugin.IsEnabled && !Round.IsStarted)
                ev.Player.Broadcast(5, $"Welcome to the <color=red>{GetType().Namespace}</color>!");
        }
    }
}