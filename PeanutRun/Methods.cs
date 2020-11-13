using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using MEC;
using Map = Exiled.Events.Handlers.Map;
using Server = Exiled.Events.Handlers.Server;

namespace PeanutRun
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
                    Exiled.Events.Handlers.Warhead.Detonated -= plugin.EventHandlers.OnDetonated;
                    Server.RoundStarted -= plugin.EventHandlers.OnRoundStart;
                    Server.RoundEnded -= plugin.EventHandlers.OnRoundEnd;
                    break;
                case false:
                    Exiled.Events.Handlers.Warhead.Detonated += plugin.EventHandlers.OnDetonated;
                    Server.RoundStarted += plugin.EventHandlers.OnRoundStart;
                    Server.RoundEnded += plugin.EventHandlers.OnRoundEnd;
                    break;
            }
        }

        public void SetupPlayers()
        {
            foreach (Player player in Player.List)
                player.Role = RoleType.Scp173;

            Timing.CallDelayed(1f, Warhead.Start);
            Round.IsLocked = true;
            plugin.IsRunning = true;
        }

        public void EndRound()
        {
            Round.IsLocked = false;
        }

        public void EnableGamemode(bool force = false)
        {
            plugin.IsEnabled = true;
            plugin.ShouldDisableNextRound = true;
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