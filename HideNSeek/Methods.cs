using System.Collections.Generic;
using Exiled.API.Features;
using MEC;
using UnityEngine;

namespace HideNSeek
{
    public class Methods
    {
        private readonly Plugin plugin;
        public Methods(Plugin plugin) => this.plugin = plugin;

        public void SetupPlayers(RoleType scpRole)
        {
            Timing.CallDelayed(2f, () => SetScps(scpRole));
        }

        public IEnumerator<float> Blackout()
        {
            for (;;)
            {
                Map.TurnOffAllLights(10);

                yield return Timing.WaitForSeconds(9.99f);
            }
        }

        void SetScps(RoleType scpRole)
        {
            int counter = 0;
            foreach (Player player in Player.List)
            {
                if (player.Team == Team.SCP)
                {
                    switch (scpRole)
                    {
                        case RoleType.Scp049:
                            if (counter >= plugin.Config.Max049Count)
                                Timing.RunCoroutine(SetHuman(player));
                            continue;
                        case RoleType.Scp93953:
                        case RoleType.Scp93989:
                            if (counter >= plugin.Config.Max939Count)
                                Timing.RunCoroutine(SetHuman(player));
                            continue;
                    }

                    player.Role = scpRole;
                    counter++;
                }
                else
                    Timing.RunCoroutine(SetHuman(player));
            }
            
            SetupMap();
        }

        IEnumerator<float> SetHuman(Player player)
        {
            player.Role = RoleType.ClassD;
            
            yield return Timing.WaitForSeconds(0.5f);
            
            player.Position = RoleType.Scp096.GetRandomSpawnPoint();
        }

        public void SetupMap()
        {
            foreach (Door door in Map.Doors)
                if (door.DoorName.ToLower().Contains("gate") || door.DoorName.ToLower().Contains("checkpoint"))
                    door.Networklocked = true;
            foreach (Pickup pickup in Object.FindObjectsOfType<Pickup>())
                pickup.Delete();
        }
        public void EnableGamemode(RoleType scpRole, bool force = false)
        {
            if (!force)
                plugin.IsEnabled = true;
            else
            {
                plugin.IsEnabled = true;
                SetupPlayers(scpRole);
            }

            plugin.ShouldDisableNextRound = true;
        }
        
        public void DisableGamemode(bool force = false)
        {
            if (!force)
                plugin.ShouldDisableNextRound = true;
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
                        player.Role = scpRoles[plugin.Rng.Next(scpRoles.Count)];
                    }
                    else
                    {
                        int r = plugin.Rng.Next(6);
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

            if (string.IsNullOrEmpty(plugin.Config.EndRoundBroadcast) || plugin.Config.EndRoundBroadcastDur <= 0) 
                return;
            Map.ClearBroadcasts();
            Map.Broadcast(plugin.Config.EndRoundBroadcastDur, plugin.Config.EndRoundBroadcast.Replace("%user", winner == null ? "no one :o" : winner.Nickname));
        }
        
        public void RegisterEvents(bool disable = false)
        {
            switch (disable)
            {
                case true:
                    Exiled.Events.Handlers.Server.RoundStarted -= plugin.EventHandlers.OnRoundStart;
                    Exiled.Events.Handlers.Server.RoundEnded -= plugin.EventHandlers.OnRoundEnd;
                    Exiled.Events.Handlers.Player.Joined -= plugin.EventHandlers.OnPlayerJoin;
                    Exiled.Events.Handlers.Player.Died -= plugin.EventHandlers.OnDeath;
                    break;
                case false:
                    Exiled.Events.Handlers.Server.RoundStarted += plugin.EventHandlers.OnRoundStart;
                    Exiled.Events.Handlers.Server.RoundEnded += plugin.EventHandlers.OnRoundEnd;
                    Exiled.Events.Handlers.Player.Joined += plugin.EventHandlers.OnPlayerJoin;
                    Exiled.Events.Handlers.Player.Died += plugin.EventHandlers.OnDeath;
                    break;
            }
        }
    }
}