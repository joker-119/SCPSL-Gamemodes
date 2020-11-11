using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using MEC;

namespace Outbreak
{
    public class Methods
    {
        private readonly Plugin _plugin;
        public Methods(Plugin plugin) => _plugin = plugin;

        public void SetupPlayers()
        {
            Timing.CallDelayed(2f, () => SpawnAlphas());
        }

        void SpawnAlphas()
        {
            int counter = 0;
            foreach (Player player in Player.List)
                if (player.Team == Team.SCP)
                {
                    counter++;
                    if (counter >= _plugin.Config.MaxAlphaCount)
                    {
                        player.Role = RoleType.ClassD;
                        continue;
                    }

                    player.SetRole(RoleType.Scp0492, true);
                    Timing.CallDelayed(0.5f, () => player.Health = _plugin.Config.AlphaZombieHealth);
                }
        }
        
        internal void EventRegistration(bool disable = false)
        {
            switch (disable)
            {
                case true:
                    Exiled.Events.Handlers.Player.InteractingDoor -= _plugin.EventHandlers.OnDoorInteraction;
                    Exiled.Events.Handlers.Server.RoundStarted -= _plugin.EventHandlers.OnRoundStart;
                    Exiled.Events.Handlers.Server.RoundEnded -= _plugin.EventHandlers.OnRoundEnd;
                    Exiled.Events.Handlers.Player.Hurting -= _plugin.EventHandlers.OnPlayerHurt;
                    Exiled.Events.Handlers.Player.Joined -= _plugin.EventHandlers.OnPlayerJoin;
                    Exiled.Events.Handlers.Player.Died -= _plugin.EventHandlers.OnPlayerDeath;
                    break;
                case false:
                    Exiled.Events.Handlers.Player.InteractingDoor += _plugin.EventHandlers.OnDoorInteraction;
                    Exiled.Events.Handlers.Server.RoundStarted += _plugin.EventHandlers.OnRoundStart;
                    Exiled.Events.Handlers.Server.RoundEnded += _plugin.EventHandlers.OnRoundEnd;
                    Exiled.Events.Handlers.Player.Hurting += _plugin.EventHandlers.OnPlayerHurt;
                    Exiled.Events.Handlers.Player.Joined += _plugin.EventHandlers.OnPlayerJoin;
                    Exiled.Events.Handlers.Player.Died += _plugin.EventHandlers.OnPlayerDeath;
                    break;
            }
        }

        public void EnableGamemode(bool force = false)
        {
            if (!force)
                _plugin.IsEnabled = true;
            else
            {
                _plugin.IsEnabled = true;
                SetupPlayers();
            }

            _plugin.ShouldDisableNextRound = true;
        }

        public void DisableGamemode(bool force = false)
        {
            if (!force)
                _plugin.ShouldDisableNextRound = true;
            else
            {
                List<RoleType> scpRoles = new List<RoleType>
                {
                    RoleType.Scp049,
                    RoleType.Scp079,
                    RoleType.Scp096,
                    RoleType.Scp106,
                    RoleType.Scp173,
                    RoleType.Scp93953,
                    RoleType.Scp93989
                };
                
                foreach (Player player in Player.List)
                {
                    if (player.Role == RoleType.Scp173)
                    {
                        player.Role = scpRoles[_plugin.Rng.Next(scpRoles.Count)];
                    }
                    else
                    {
                        int r = _plugin.Rng.Next(6);
                        switch (r)
                        {
                            case 6:
                                player.Role = RoleType.FacilityGuard;
                                break;
                            case 5:
                            case 4:    
                                player.Role = RoleType.Scientist;
                                break;
                            default:
                                player.Role = RoleType.ClassD;
                                break;
                        }
                    }
                }
            }
        }

        public void EndRound()
        {
            Player winner = null;
            
            foreach (Player player in Player.List)
            {
                switch (player.Role)
                {
                    case RoleType.Scp0492:
                        player.Kill();
                        break;
                    case RoleType.ClassD:
                        winner = player;
                        break;
                }
            }

            if (string.IsNullOrEmpty(_plugin.Config.EndRoundBroadcast) || _plugin.Config.EndRoundBroadcastDur <= 0) 
                return;
            Map.ClearBroadcasts();
            Map.Broadcast(_plugin.Config.EndRoundBroadcastDur, _plugin.Config.EndRoundBroadcast.Replace("%user", winner == null ? "no one :o" : winner.Nickname));
        }
    }
}