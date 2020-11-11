using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;

namespace HideNSeek
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
            _plugin.Methods.SetupPlayers(_plugin.ScpRole);

            if (_plugin.Config.DisableLights)
                Timing.RunCoroutine(_plugin.Methods.Blackout(), "hidenseekblackout");
        }
        
        public void OnPlayerJoin(JoinedEventArgs ev)
        {
            if (_plugin.IsRunning || _plugin.IsEnabled && !Round.IsStarted)
                ev.Player.Broadcast(5, $"Welcome to the <color=red>{GetType().Namespace}</color>!");
        }

        public void OnRoundEnd(RoundEndedEventArgs ev)
        {
            if (!_plugin.IsRunning)
                return;

            if (_plugin.ShouldDisableNextRound)
                _plugin.IsEnabled = false;

            _plugin.IsRunning = false;
            Timing.KillCoroutines("hidenseekblackout");
        }

        public void OnDeath(DiedEventArgs ev)
        {
            if (!_plugin.IsRunning)
                return;
            
            int humanCount = 0;
            
            foreach (Player player in Player.List)
                if (player.IsAlive && player.ReferenceHub.characterClassManager.IsHuman())
                    humanCount++;

            if (humanCount < 2)
                _plugin.Methods.EndRound();
        }
    }
}