using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;

namespace PeanutInfection
{
    public class Methods
    {
        private readonly Plugin plugin;
        public Methods(Plugin plugin) => this.plugin = plugin;
        
        private int GetScpCount => (plugin.Config.SmartScpSelection ? Player.List.Count() / 6 : plugin.Config.MaxStartingScpCount) < 1 ? 1 : plugin.Config.SmartScpSelection ? Player.List.Count() / 6 : plugin.Config.MaxStartingScpCount;

        internal void RegisterEvents(bool disable = false)
        {
            switch (disable)
            {
                case true:
                    Exiled.Events.Handlers.Server.RoundStarted -= plugin.EventHandlers.OnRoundStart;
                    Exiled.Events.Handlers.Server.EndingRound -= plugin.EventHandlers.OnEndingRound;
                    Exiled.Events.Handlers.Player.Escaping -= plugin.EventHandlers.OnPlayerEscape;
                    Exiled.Events.Handlers.Server.RoundEnded -= plugin.EventHandlers.OnRoundEnd;
                    Exiled.Events.Handlers.Player.Died -= plugin.EventHandlers.OnPlayedDied;
                    break;
                case false:
                    Exiled.Events.Handlers.Server.RoundStarted += plugin.EventHandlers.OnRoundStart;
                    Exiled.Events.Handlers.Server.EndingRound += plugin.EventHandlers.OnEndingRound;
                    Exiled.Events.Handlers.Player.Escaping += plugin.EventHandlers.OnPlayerEscape;
                    Exiled.Events.Handlers.Server.RoundEnded += plugin.EventHandlers.OnRoundEnd;
                    Exiled.Events.Handlers.Player.Died += plugin.EventHandlers.OnPlayedDied;
                    break;
            }
        }

        public void SetupPlayers()
        {
            List<Player> players = Player.List.ToList();
            for (int i = 0; i < GetScpCount; i++)
            {
                int r = plugin.Rng.Next(players.Count);
                players[r].Role = RoleType.Scp173;
                players.Remove(players[r]);
            }

            foreach (Player player in players) 
                player.Role = RoleType.ClassD;

            plugin.IsRunning = true;
        }

        public void EnableGamemode(bool force = false)
        {
            plugin.IsEnabled = true;
            plugin.ShouldDisableNextRound = true;
            plugin.EscapeCounter = 0;
            plugin.IsEnded = false;
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