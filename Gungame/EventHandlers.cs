using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;

namespace Gungame
{
    public class EventHandlers
    {
        private readonly Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        public void OnRoundStart()
        {
            if (!plugin.IsEnabled)
                return;

            Timing.CallDelayed(1f, () => plugin.Methods.SetupPlayers());
        }

        public void OnRoundEnd(RoundEndedEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;

            plugin.IsRunning = false;
            if (plugin.ShouldDisableNextRound)
                plugin.IsEnabled = false;
        }

        public void OnHurting(HurtingEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;
            
            if (ev.Attacker == ev.Target && ev.DamageType != DamageTypes.Falldown)
                ev.Amount = 0f;
        }

        public void OnDied(DiedEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;
            
            if (ev.Killer != ev.Target)
                plugin.Methods.UpgradeItem(ev.Killer, ev.Killer.CurrentItem.id);
            Timing.CallDelayed(plugin.Config.RespawnTime, () => plugin.Methods.SpawnPlayer(ev.Target));
        }

        public void OnThrowingGrenade(ThrowingGrenadeEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;

            Timing.CallDelayed(0.25f,
                () => ev.Player.AddItem(ev.Type == GrenadeType.FragGrenade ? ItemType.GrenadeFrag : ItemType.SCP018));
        }

        public void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;

            ev.IsAllowed = false;
            ev.Pickup.Delete();
        }
    }
}