using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;

namespace HideNSeek
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
            Timing.CallDelayed(1f, () => plugin.Methods.SetupPlayers(plugin.ScpRole));

            if (plugin.Config.DisableLights)
                Timing.RunCoroutine(plugin.Methods.Blackout(), "hidenseekblackout");
        }
        
        public void OnPlayerJoin(JoinedEventArgs ev)
        {
            if (plugin.IsRunning || plugin.IsEnabled && !Round.IsStarted)
                ev.Player.Broadcast(5, $"Welcome to the <color=red>{GetType().Namespace}</color>!");
        }

        public void OnRoundEnd(RoundEndedEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;

            if (plugin.ShouldDisableNextRound)
                plugin.IsEnabled = false;

            plugin.IsRunning = false;
            Timing.KillCoroutines("hidenseekblackout");
        }

        public void OnDeath(DiedEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;
            
            int humanCount = 0;
            
            foreach (Player player in Player.List)
                if (player.IsAlive && player.ReferenceHub.characterClassManager.IsHuman())
                    humanCount++;

            if (humanCount < 2)
                plugin.Methods.EndRound();
        }
    }
}