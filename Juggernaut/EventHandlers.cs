using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;

namespace Juggernaut
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
            
            _plugin.Juggernaut = null;
            _plugin.IsRunning = false;
            if (_plugin.ShouldDisableNextRound)
                _plugin.IsEnabled = false;
        }
        
        public void OnPlayerJoin(JoinedEventArgs ev)
        {
            if (_plugin.IsRunning || _plugin.IsEnabled && !Round.IsStarted)
                ev.Player.Broadcast(5, $"Welcome to the <color=green>{GetType().Namespace}</color>!");
        }

        public void OnThrowingGrenade(ThrowingGrenadeEventArgs ev)
        {
            if (!_plugin.IsRunning)
                return;
            
            if (ev.Player == _plugin.Juggernaut && _plugin.Config.InfiniteJuggGrenades)
                Timing.CallDelayed(1.5f,
                    () => ev.Player.AddItem(ev.GrenadeManager.availableGrenades[(int) ev.Type].inventoryID));
        }
    }
}