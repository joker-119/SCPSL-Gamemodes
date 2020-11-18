using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;

namespace VIP
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

            plugin.Vip = null;
            plugin.Guards = null;
            plugin.IsRunning = false;
            if (plugin.Coroutine.IsValid) 
                Timing.KillCoroutines(plugin.Coroutine);
            if (plugin.ShouldDisableNextRound)
                plugin.IsEnabled = false;
        }

        public void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;

            if (plugin.Config.DisableItemPickup) 
                ev.IsAllowed = false;
            else if(plugin.Config.DisableItemPickupVip)
            {
                if (ev.Player == plugin.Vip) 
                    ev.IsAllowed = false;
            }
        }

        public void OnDying(DyingEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;

            if (ev.Target == plugin.Vip)
            {
                foreach (Player p in Player.List)
                {
                    if (plugin.Guards.Contains(p) || p == plugin.Vip) 
                        p.Kill();
                }
                RoundSummary.escaped_ds = 1;
                Map.Broadcast(10, "The VIP was killed. Attackers have won!");
                Round.IsLocked = false;
            }

            if (plugin.Config.GuardsRespawn && plugin.Guards.Contains(ev.Target))
            {
                RoleType role = ev.Target.Role;
                Timing.CallDelayed(plugin.Config.GuardRespawnDelay, () =>
                {
                    ev.Target.SetRole(role);
                    Timing.CallDelayed(0.2f, () =>
                    {
                        ev.Target.Position = !Map.IsLCZDecontaminated ? RoleType.ClassD.GetRandomSpawnPoint() : RoleType.Scp106.GetRandomSpawnPoint();
                    });
                });
            }
            else if (plugin.Config.AttackersRespawn && !plugin.Guards.Contains(ev.Target) && ev.Target != plugin.Vip)
            {
                RoleType role = ev.Target.Role;
                Timing.CallDelayed(plugin.Config.AttackerRespawnDelay, () =>
                {
                    ev.Target.SetRole(role);
                });
            }
        }

        public void OnEscaping(EscapingEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;

            if (ev.Player == plugin.Vip)
            {
                foreach (Player p in Player.List)
                {
                    if (!plugin.Guards.Contains(p) && p != plugin.Vip) 
                        p.Kill();
                }
                Map.Broadcast(10, "The VIP has escaped. The guards and VIP have won!");
                Round.IsLocked = false;
            }
        }
    }
}
