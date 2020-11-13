using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Server = Exiled.Events.Handlers.Server;

namespace TeamDeathmatch
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
                    Server.RoundStarted -= plugin.EventHandlers.OnRoundStart;
                    Server.RoundEnded -= plugin.EventHandlers.OnRoundEnd;
                    break;
                case false:
                    Server.RoundStarted += plugin.EventHandlers.OnRoundStart;
                    Server.RoundEnded += plugin.EventHandlers.OnRoundEnd;
                    break;
            }
        }

        public void SetupPlayers()
        {
            List<Player> players = Player.List.ToList();
            for (int i = 0; i < players.Count / 2; i++)
            {
                players[i].Role = RoleType.ChaosInsurgency;
                players[i].AddItem(ItemType.GunE11SR);
                players.Remove(players[i]);
            }

            foreach (Player player in players)
            {
                player.Role = RoleType.NtfCommander;
                player.AddItem(ItemType.GunLogicer);
                player.AddItem(ItemType.Painkillers);
            }

            foreach (Player player in Player.List)
            {
                player.Ammo[0] = 200;
                player.Ammo[1] = 200;
                player.Ammo[2] = 200;
                
                foreach (ItemType type in plugin.Config.AdditionalItemList)
                    player.AddItem(type);
            }

            SetupMap();
            plugin.IsRunning = true;
        }

        void SetupMap()
        {
            Warhead.Detonate();
            foreach (Door door in Map.Doors)
                if (door.DoorName == "SURFACE_GATE")
                    door.NetworkisOpen = true;
        }

        public void EnableGamemode(bool force = false)
        {
            plugin.IsEnabled = true;
            plugin.ShouldDisableNextRound = true;
            if (force)
                SetupPlayers();
        }

        public void DisableGamemode(bool force = false)
        {
            plugin.ShouldDisableNextRound = true;

            if (force)
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
            }
        }
    }
}