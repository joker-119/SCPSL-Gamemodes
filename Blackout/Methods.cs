using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;

namespace Blackout
{
    public class Methods
    {
        private readonly Plugin plugin;
        public Methods(Plugin plugin) => this.plugin = plugin;

        private int GetScpCount => (plugin.Config.SmartScpSelection ? Player.List.Count() / 6 : plugin.Config.MaxScpCount) < 1 ? 1 : plugin.Config.SmartScpSelection ? Player.List.Count() / 6 : plugin.Config.MaxScpCount;

        public void SetupPlayers()
        {
            Timing.CallDelayed(1f, () => SpawnScps());
        }

        void SpawnScps()
        {
           List<Player> players = Player.List.ToList();
           for (int i = 0; i < GetScpCount; i++)
           {
                int r = plugin.Rng.Next(players.Count);
                players[r].Role = plugin.Config.ScpToSpawn;
                players.Remove(players[r]);
           }

           foreach (Player player in Player.Get(Side.Scp))
               if (player.Role != plugin.Config.ScpToSpawn)
                   player.Role = RoleType.ClassD;

           foreach (Player player in Player.List)
           {
               bool flag = false;
               foreach (Inventory.SyncItemInfo item in player.Items)
                   if (item.id == ItemType.Flashlight)
                   {
                       flag = true;
                       break;
                   }
               
               if (!flag)
                   player.AddItem(ItemType.Flashlight);
           }
        }
        
        internal void EventRegistration(bool disable = false)
        {
            switch (disable)
            {
                case true:
                    Exiled.Events.Handlers.Server.RoundStarted -= plugin.EventHandlers.OnRoundStart;
                    Exiled.Events.Handlers.Server.RoundEnded -= plugin.EventHandlers.OnRoundEnd;
                    Exiled.Events.Handlers.Player.ChangingRole -= plugin.EventHandlers.OnChangingRole;
                    break;
                case false:
                    Exiled.Events.Handlers.Server.RoundStarted += plugin.EventHandlers.OnRoundStart;
                    Exiled.Events.Handlers.Server.RoundEnded += plugin.EventHandlers.OnRoundEnd;
                    Exiled.Events.Handlers.Player.ChangingRole += plugin.EventHandlers.OnChangingRole;
                    break;
            }
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
            }
        }
    }
}