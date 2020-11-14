using System.Collections.Generic;
using Exiled.API.Features;
using MEC;
using Server = Exiled.Events.Handlers.Server;

namespace Gungame
{
    public class Methods
    {
        private readonly Plugin plugin;
        public Methods(Plugin plugin) => this.plugin = plugin;
        public List<Player> RecentlyUpgraded = new List<Player>();

        internal void RegisterEvents(bool disable = false)
        {
            switch (disable)
            {
                case true:
                    Exiled.Events.Handlers.Player.ThrowingGrenade -= plugin.EventHandlers.OnThrowingGrenade;
                    Exiled.Events.Handlers.Player.Hurting -= plugin.EventHandlers.OnHurting;
                    Exiled.Events.Handlers.Player.Died -= plugin.EventHandlers.OnDied;
                    Server.RoundStarted -= plugin.EventHandlers.OnRoundStart;
                    Server.RoundEnded -= plugin.EventHandlers.OnRoundEnd;
                    break;
                case false:
                    Exiled.Events.Handlers.Player.ThrowingGrenade += plugin.EventHandlers.OnThrowingGrenade;
                    Exiled.Events.Handlers.Player.Hurting += plugin.EventHandlers.OnHurting;
                    Exiled.Events.Handlers.Player.Died += plugin.EventHandlers.OnDied;
                    Server.RoundStarted += plugin.EventHandlers.OnRoundStart;
                    Server.RoundEnded += plugin.EventHandlers.OnRoundEnd;
                    break;
            }
        }

        public void SetupPlayers()
        {
            Round.IsLocked = true;
            plugin.FriendlyFire = Exiled.API.Features.Server.FriendlyFire;
            Exiled.API.Features.Server.FriendlyFire = true;
            foreach (Player player in Player.List)
            {
                player.Role = RoleType.ClassD;
                Timing.CallDelayed(0.5f, () => player.ResetInventory(new List<ItemType> { ItemType.SCP018 }));
                player.Ammo[0] = 300;
                player.Ammo[1] = 300;
                player.Ammo[2] = 300;
            }

            plugin.IsRunning = true;
        }

        public void UpgradeItem(Player player, ItemType curWep)
        {
            if (RecentlyUpgraded.Contains(player))
                return;
            
            switch (curWep)
            {
                case ItemType.SCP018:
                    player.ResetInventory(new List<ItemType> { ItemType.GunCOM15 });
                    player.CurrentItem = new Inventory.SyncItemInfo
                    {
                        durability = float.MaxValue,
                        id = ItemType.GunCOM15,
                    };
                    break;
                case ItemType.GunCOM15:
                    player.ResetInventory(new List<ItemType> { ItemType.GunUSP });
                    player.CurrentItem = new Inventory.SyncItemInfo
                    {
                        durability = float.MaxValue,
                        id = ItemType.GunCOM15,
                    };
                    break;
                case ItemType.GunUSP:
                    player.ResetInventory(new List<ItemType> { ItemType.GunMP7 });
                    player.CurrentItem = new Inventory.SyncItemInfo
                    {
                        durability = float.MaxValue,
                        id = ItemType.GunMP7,
                    };
                    break;
                case ItemType.GunMP7:
                    player.ResetInventory(new List<ItemType> { ItemType.GunLogicer });
                    player.CurrentItem = new Inventory.SyncItemInfo
                    {
                        durability = float.MaxValue,
                        id = ItemType.GunLogicer,
                    };
                    break;
                case ItemType.GunLogicer:
                    player.ResetInventory(new List<ItemType> { ItemType.GunProject90 });
                    player.CurrentItem = new Inventory.SyncItemInfo
                    {
                        durability = float.MaxValue,
                        id = ItemType.GunProject90,
                    };
                    break;
                case ItemType.GunProject90:
                    player.ResetInventory(new List<ItemType> { ItemType.GunE11SR });
                    player.CurrentItem = new Inventory.SyncItemInfo
                    {
                        durability = float.MaxValue,
                        id = ItemType.GunE11SR,
                    };
                    break;
                case ItemType.GunE11SR:
                    player.ResetInventory(new List<ItemType> { ItemType.GrenadeFrag });
                    player.CurrentItem = new Inventory.SyncItemInfo
                    {
                        durability = float.MaxValue,
                        id = ItemType.GrenadeFrag,
                    };
                    break;
                case ItemType.GrenadeFrag:
                    EndRound(player);
                    break;
            }

            player.Ammo[0] = 300;
            player.Ammo[1] = 300;
            player.Ammo[2] = 300;
            RecentlyUpgraded.Add(player);
            Timing.CallDelayed(1.5f, () => RecentlyUpgraded.Remove(player));
        }

        public void EndRound(Player winner)
        {
            foreach (Player player in Player.List)
                if (player != winner)
                    player.Kill();
            
            Round.IsLocked = false;
            Exiled.API.Features.Server.FriendlyFire = plugin.FriendlyFire;
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
                Round.IsLocked = false;
                Exiled.API.Features.Server.FriendlyFire = plugin.FriendlyFire;
            }
        }
    }
}