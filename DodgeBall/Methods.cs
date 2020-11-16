using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Grenades;
using MEC;
using Mirror;
using UnityEngine;
using Server = Exiled.Events.Handlers.Server;

namespace DodgeBall
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
            Round.IsLocked = true;
            Vector3 spawnPos = RoleType.Scp173.GetRandomSpawnPoint();
            foreach (Player player in Player.List)
            {
                player.Role = RoleType.ClassD;
                Timing.CallDelayed(0.5f, () => player.Position = spawnPos);
            }

            plugin.IsRunning = true;

            plugin.Coroutine = Timing.RunCoroutine(DodgeballLoop());
        }

        IEnumerator<float> DodgeballLoop()
        {
            yield return Timing.WaitForSeconds(10f);

            for (;;)
            {
                int count = 0;
                Player winner = null;
                foreach (Player player in Player.List)
                    if (player.IsAlive)
                    {
                        count++;
                        winner = player;
                    }

                if (count <= 1)
                {
                    EndRound(winner);
                    yield break;
                }
                
                foreach (Player player in Player.List)
                    if (plugin.Rng.Next(100) >= 50)
                        SpawnGrenadeOnPlayer(player);

                yield return Timing.WaitForSeconds(plugin.Config.NewDodgeDelay);
            }
        }

        private void SpawnGrenadeOnPlayer(Player player)
        {
            Vector3 spawnrand = new Vector3(UnityEngine.Random.Range(0f, 2f), UnityEngine.Random.Range(0f, 2f), UnityEngine.Random.Range(0f, 2f));
            GrenadeManager gm = player.ReferenceHub.GetComponent<GrenadeManager>();
            GrenadeSettings grenade = gm.availableGrenades.FirstOrDefault(g => g.inventoryID == plugin.Type);
            Grenade component = Object.Instantiate(grenade.grenadeInstance).GetComponent<Grenade>();
            component.InitData(gm, spawnrand, Vector3.zero);
            NetworkServer.Spawn(component.gameObject);
        }

        public void EndRound(Player winner)
        {
            Round.IsLocked = false;
            Map.Broadcast(10, $"{winner.Nickname} has claimed victory!");
        }

        public void EnableGamemode(ItemType type, bool force = false)
        {
            plugin.IsEnabled = true;
            plugin.ShouldDisableNextRound = true;
            plugin.Type = type;
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
                Round.IsLocked = false;
            }
        }
    }
}