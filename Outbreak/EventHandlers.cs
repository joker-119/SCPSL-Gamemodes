using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using Outbreak.Components;

namespace Outbreak
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

        public void OnPlayerHurt(HurtingEventArgs ev)
        {
            if (!_plugin.IsRunning)
                return;
            
            if (ev.Attacker.Role == RoleType.Scp0492 && _plugin.Rng.Next(100) <= _plugin.Config.InfectionChance)
                ev.Target.GameObject.AddComponent<Infection>().Attacker = ev.Attacker;
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
                    if (ev.Target.Role != RoleType.Scp0492 && _plugin.Config.AllDeathsMakeZombies)
                        Timing.CallDelayed(0.15f, () => ev.Target.Role = RoleType.Scp0492);
                    break;
            }
        }

        public void OnDoorInteraction(InteractingDoorEventArgs ev)
        {
            if (!_plugin.IsRunning)
                return;
            
            if (ev.Door.locked && ev.Player.Role == RoleType.Scp0492 && _plugin.AlphaZombies.Contains(ev.Player) &&
                _plugin.Config.AlphasBreakLockedDoors)
            {
                if (ev.Door.doorType == Door.DoorTypes.HeavyGate)
                    ev.Door.PryGate();
                else
                    ev.Door.DestroyDoor(true);
            }
        }
        
        public void OnPlayerJoin(JoinedEventArgs ev)
        {
            if (_plugin.IsRunning || _plugin.IsEnabled && !Round.IsStarted)
                ev.Player.Broadcast(5, $"Welcome to the <color=orange>{GetType().Namespace}</color>!");
        }
    }
}