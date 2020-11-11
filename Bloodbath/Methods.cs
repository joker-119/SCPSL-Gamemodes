using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using MEC;
using UnityEngine;

namespace Bloodbath
{
    public class Methods
    {
        private readonly Plugin _plugin;
        public Methods(Plugin plugin) => this._plugin = plugin;
        
        private List<Player> _scp173S = new List<Player>();
        private Vector3 _spawnPoint = Vector3.zero;

        private int GetScpCount => (_plugin.Config.SmartScpSelection ? Player.List.Count() / 6 : _plugin.Config.MaxScpCount) < 1 ? 1 : _plugin.Config.SmartScpSelection ? Player.List.Count() / 6 : _plugin.Config.MaxScpCount;
        private Vector3 GetSpawnPoint
        {
            get
            {
                if (_spawnPoint == Vector3.zero)
                    _spawnPoint = RoleType.Tutorial.GetRandomSpawnPoint();
                return _spawnPoint;
            }
        }

        public void SetupPlayers()
        {
            List<Player> players = Player.List.ToList();
            for (int i = 0; i < GetScpCount; i++)
            {
                int r = _plugin.Rng.Next(players.Count);
                Timing.RunCoroutine(MakeScp(players[r]));
                players.Remove(players[r]);
            }

            foreach (Player player in players)
                Timing.RunCoroutine(MakeHuman(player));
        }
        
        public void EndRound()
        {
            foreach (Player player in _scp173S) 
                player?.Kill();

            Player winner = Player.List.FirstOrDefault(p => p.Role == RoleType.ClassD);


            if (string.IsNullOrEmpty(_plugin.Config.EndRoundBroadcast) || _plugin.Config.EndRoundBroadcastDur <= 0) 
                return;
            Map.ClearBroadcasts();
            Map.Broadcast(_plugin.Config.EndRoundBroadcastDur, _plugin.Config.EndRoundBroadcast.Replace("%user", winner == null ? "no one :o" : winner.Nickname));
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

        IEnumerator<float> MakeHuman(Player player)
        {
            player.Role = RoleType.ClassD;
            yield return Timing.WaitForSeconds(0.5f);

            player.Position = GetSpawnPoint;
        }

        IEnumerator<float> MakeScp(Player player)
        {
            player.Role = RoleType.Scp173;
            player.Health = 173;
            player.IsGodModeEnabled = true;
            yield return Timing.WaitForSeconds(10f);

            player.Position = GetSpawnPoint;
        }
        
        internal void EventRegistration(bool disable = false)
        {
            switch (disable)
            {
                case true:
                    Exiled.Events.Handlers.Server.RoundStarted -= _plugin.EventHandlers.OnRoundStart;
                    Exiled.Events.Handlers.Server.RoundEnded -= _plugin.EventHandlers.OnRoundEnd;
                    Exiled.Events.Handlers.Player.Joined -= _plugin.EventHandlers.OnPlayerJoin;
                    Exiled.Events.Handlers.Player.Died -= _plugin.EventHandlers.OnPlayerDeath;
                    break;
                case false:
                    Exiled.Events.Handlers.Server.RoundStarted += _plugin.EventHandlers.OnRoundStart;
                    Exiled.Events.Handlers.Server.RoundEnded += _plugin.EventHandlers.OnRoundEnd;
                    Exiled.Events.Handlers.Player.Joined += _plugin.EventHandlers.OnPlayerJoin;
                    Exiled.Events.Handlers.Player.Died += _plugin.EventHandlers.OnPlayerDeath;
                    break;
            }
        }
    }
}