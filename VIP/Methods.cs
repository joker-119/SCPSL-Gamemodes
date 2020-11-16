using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using MEC;
using UnityEngine;

namespace VIP
{
    public class Methods
    {
        private readonly Plugin plugin;
        public Methods(Plugin plugin) => this.plugin = plugin;

        internal void RegisterEvents(bool disable = false)
        {
            switch (disable)
            {
                case true:
                    Exiled.Events.Handlers.Server.RoundStarted -= plugin.EventHandlers.OnRoundStart;
                    Exiled.Events.Handlers.Server.RoundEnded -= plugin.EventHandlers.OnRoundEnd;
                    Exiled.Events.Handlers.Player.PickingUpItem -= plugin.EventHandlers.OnPickingUpItem;
                    Exiled.Events.Handlers.Player.Dying -= plugin.EventHandlers.OnDying;
                    Exiled.Events.Handlers.Player.Escaping -= plugin.EventHandlers.OnEscaping;
                    break;
                case false:
                    Exiled.Events.Handlers.Server.RoundStarted += plugin.EventHandlers.OnRoundStart;
                    Exiled.Events.Handlers.Server.RoundEnded += plugin.EventHandlers.OnRoundEnd;
                    Exiled.Events.Handlers.Player.PickingUpItem += plugin.EventHandlers.OnPickingUpItem;
                    Exiled.Events.Handlers.Player.Dying += plugin.EventHandlers.OnDying;
                    Exiled.Events.Handlers.Player.Escaping += plugin.EventHandlers.OnEscaping;
                    break;
            }
        }

        public void SetupPlayers()
        {
            Round.IsLocked = true;
            plugin.IsRunning = true;

            plugin.Guards = new List<Player>();
            List<Player> players = Player.List.ToList();
            int r = plugin.Rng.Next(players.Count);

            plugin.Vip = players[r];
            players.Remove(players[r]);

            int spawns = (int)Math.Ceiling((players.Count + 1) * (plugin.Config.PercentOfGuards / 100));
            int commanders = 1;
            int lieutenants = Mathf.Clamp(spawns - commanders, 0, 3); ;

            Timing.CallDelayed(0.1f, () =>
            {
                for (int i = 0; i < spawns; i++)
                {
                    r = plugin.Rng.Next(players.Count);
                    Player p = players[r];

                    plugin.Guards.Add(p);

                    if (commanders > 0)
                    {
                        p.SetRole(RoleType.NtfCommander);
                        commanders--;
                    }
                    else if (lieutenants > 0)
                    {
                        p.SetRole(RoleType.NtfLieutenant);
                        lieutenants--;
                    }
                    else p.SetRole(RoleType.NtfCadet);
                    Timing.CallDelayed(0.5f, () =>
                    {
                        p.Broadcast(5, plugin.Config.GuardBroadcast);
                        p.Position = RoleType.ClassD.GetRandomSpawnPoint();
                    });

                    players.Remove(players[r]);
                }
                
                plugin.Vip.Role = RoleType.Scientist;
                Timing.CallDelayed(0.5f, () =>
                {
                    plugin.Vip.Broadcast(5, plugin.Config.VipBroadcast);
                    plugin.Vip.Position = RoleType.ClassD.GetRandomSpawnPoint();
                    plugin.Vip.Health = plugin.Config.VipStartingHealth;
                    plugin.Vip.ReferenceHub.playerStats.artificialHpDecay = plugin.Config.VipArmorDecay ? plugin.Config.VipArmorDecayRate : 0;
                    plugin.Vip.AdrenalineHealth = plugin.Config.VipStartingArmor;
                });
                
                foreach (Player p in players)
                {
                    p.Broadcast(5, plugin.Config.AttackerBroadcast);
                    p.SetRole(RoleType.ChaosInsurgency);
                }
                
                plugin.Coroutine = Timing.RunCoroutine(RoundTimer());
            });
        }

        public void EnableGamemode(bool force = false)
        {
            if (!force)
                plugin.IsEnabled = true;
            else
            {
                plugin.IsEnabled = true;
                SetupPlayers();
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

                plugin.IsEnabled = false;
                plugin.IsRunning = false;
                Round.IsLocked = false;
            }
        }

        public IEnumerator<float> RoundTimer()
		{
            yield return Timing.WaitForSeconds(plugin.Config.MaxDuration);
            
            RoundSummary.escaped_ds = 1;
            foreach (Player p in Player.List)
                if (plugin.Guards.Contains(p) || p == plugin.Vip) 
                    p.Kill();
            
            Map.Broadcast(10, "The VIP was unable to escape. Attackers have won!");
            Round.IsLocked = false;
        }
    }
}
