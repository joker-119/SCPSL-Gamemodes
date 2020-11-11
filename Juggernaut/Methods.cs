using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using MEC;

namespace Juggernaut
{
    public class Methods
    {
        private readonly Plugin _plugin;
        public Methods(Plugin plugin) => _plugin = plugin;

        internal void RegisterEvents(bool disable = false)
        {
            switch (disable)
            {
                case true:
                    Exiled.Events.Handlers.Player.ThrowingGrenade -= _plugin.EventHandlers.OnThrowingGrenade;
                    Exiled.Events.Handlers.Server.RoundStarted -= _plugin.EventHandlers.OnRoundStart;
                    Exiled.Events.Handlers.Server.RoundEnded -= _plugin.EventHandlers.OnRoundEnd;
                    Exiled.Events.Handlers.Player.Joined -= _plugin.EventHandlers.OnPlayerJoin;
                    break;
                case false:
                    Exiled.Events.Handlers.Player.ThrowingGrenade += _plugin.EventHandlers.OnThrowingGrenade;
                    Exiled.Events.Handlers.Server.RoundStarted += _plugin.EventHandlers.OnRoundStart;
                    Exiled.Events.Handlers.Server.RoundEnded += _plugin.EventHandlers.OnRoundEnd;
                    Exiled.Events.Handlers.Player.Joined += _plugin.EventHandlers.OnPlayerJoin;
                    break;
            }
        }
        
        public void SetupPlayers()
        {
            int r = _plugin.Rng.Next(Player.Dictionary.Count);

            _plugin.Juggernaut = Player.List.ElementAt(r);
            
            foreach (Player player in Player.List)
                if (player != _plugin.Juggernaut)
                {
                    player.Role = RoleType.NtfCommander;
                    if (_plugin.Config.CommandersGetMicro)
                        Timing.CallDelayed(0.5f, () => player.AddItem(ItemType.MicroHID));
                }

            _plugin.Juggernaut.Role = RoleType.ChaosInsurgency;
            Timing.CallDelayed(0.75f, () =>
            {
                _plugin.Juggernaut.Position = RoleType.Scp93953.GetRandomSpawnPoint();
                _plugin.Juggernaut.Health = _plugin.Config.JuggernautHealth;
                if (_plugin.Config.JuggernautInv.Count > 0)
                    _plugin.Juggernaut.ResetInventory(_plugin.Config.JuggernautInv);
            });
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
    }
}