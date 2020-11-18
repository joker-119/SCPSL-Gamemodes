using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;

namespace Juggernaut
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
            Timing.CallDelayed(1f, () => plugin.Methods.SetupPlayers());
        }

        public void OnRoundEnd(RoundEndedEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;
            
            plugin.Juggernaut = null;
            plugin.IsRunning = false;
            if (plugin.ShouldDisableNextRound)
                plugin.IsEnabled = false;
        }
        
        public void OnPlayerJoin(JoinedEventArgs ev)
        {
            if (plugin.IsRunning || plugin.IsEnabled && !Round.IsStarted)
                ev.Player.Broadcast(5, $"Welcome to the <color=green>{GetType().Namespace}</color>!");
        }

        public void OnThrowingGrenade(ThrowingGrenadeEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;
            
            if (ev.Player == plugin.Juggernaut && plugin.Config.InfiniteJuggGrenades)
                Timing.CallDelayed(1.5f,
                    () => ev.Player.AddItem(ev.GrenadeManager.availableGrenades[(int) ev.Type].inventoryID));
        }
    }
}