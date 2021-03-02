using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;
using MEC;

namespace Blackout
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
            Timing.CallDelayed(0.5f, () => plugin.Methods.SetupPlayers());

            Map.TurnOffAllLights(float.MaxValue);
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (!plugin.IsRunning) return
            Timing.CallDelayed(1.0f, () => ev.Player.AddItem(ItemType.Flashlight));
        }

        public void OnRoundEnd(RoundEndedEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;
            
            plugin.IsRunning = false;
            if (plugin.ShouldDisableNextRound)
                plugin.IsEnabled = false;
        }
        
        public void OnPlayerJoin(JoinedEventArgs ev)
        {
            if (plugin.IsRunning || plugin.IsEnabled && !Round.IsStarted)
                ev.Player.Broadcast(5, $"Welcome to the <color=orange>{GetType().Namespace}</color>!");
        }
    }
}
