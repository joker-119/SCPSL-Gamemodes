using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;
using MEC;
using Outbreak.Components;

namespace Outbreak
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
            
            plugin.IsRunning = false;
            if (plugin.ShouldDisableNextRound)
                plugin.IsEnabled = false;
        }

        public void OnPlayerHurt(HurtingEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;
            
            if (ev.Attacker.Role == RoleType.Scp0492 && plugin.Rng.Next(100) <= plugin.Config.InfectionChance)
                ev.Target.GameObject.AddComponent<Infection>().attacker = ev.Attacker;
        }

        public void OnPlayerDeath(DiedEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;
            
            if (ev.Target.Role != RoleType.Scp0492 && plugin.Config.AllDeathsMakeZombies)
                Timing.CallDelayed(0.15f, () => ev.Target.Role = RoleType.Scp0492);
        }

        public void OnDoorInteraction(InteractingDoorEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;
            
            if (ev.Door.ActiveLocks > 0 && ev.Player.Role == RoleType.Scp0492 && plugin.AlphaZombies.Contains(ev.Player) &&
                plugin.Config.AlphasBreakLockedDoors)
            {
                if (ev.Door.TryGetComponent(out PryableDoor component))
                    component.TryPryGate();
                else if (ev.Door is IDamageableDoor damage)
                    damage.ServerDamage(ushort.MaxValue, DoorDamageType.ServerCommand);
            }
        }
        
        public void OnPlayerJoin(JoinedEventArgs ev)
        {
            if (plugin.IsRunning || plugin.IsEnabled && !Round.IsStarted)
                ev.Player.Broadcast(5, $"Welcome to the <color=orange>{GetType().Namespace}</color>!");
        }
    }
}