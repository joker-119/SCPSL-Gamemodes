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

            plugin.IsRunning = true;
            plugin.Methods.SetupPlayers();
        }

        public void OnRoundEnd(RoundEndedEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;

            plugin.VIP = null;
            plugin.Guards = null;
            plugin.IsRunning = false;
            if (plugin.Coroutine != null) 
                Timing.KillCoroutines(plugin.Coroutine);
            if (plugin.ShouldDisableNextRound)
                plugin.IsEnabled = false;
        }

        public void OnPickingUpItem(PickingUpItemEventArgs ev)
		{
            if (plugin.Config.DisableItemPickup) 
                ev.IsAllowed = false;
            else if(plugin.Config.DisableItemPickupVip)
			{
                if (ev.Player == plugin.VIP) 
                    ev.IsAllowed = false;
			}
		}

        public void OnDying(DyingEventArgs ev)
		{
            if (ev.Target == plugin.VIP)
			{
                foreach (Player p in Player.List)
                {
                    if (plugin.Guards.Contains(p) || p == plugin.VIP) 
                        p.Kill();
                }
                Map.Broadcast(10, "The VIP was killed. Attackers have won!");
            }

            if (plugin.Config.GuardsRespawn && plugin.Guards.Contains(ev.Target))
			{
                RoleType role = ev.Target.Role;
                Timing.CallDelayed(plugin.Config.GuardRespawnDelay, () =>
                {
                    ev.Target.SetRole(role);
                    ev.Target.Position = RoleType.ClassD.GetRandomSpawnPoint();
                });
			}else if (plugin.Config.AttackersRespawn && !plugin.Guards.Contains(ev.Target) && ev.Target != plugin.VIP)
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
            if (ev.Player == plugin.VIP)
			{
                foreach (Player p in Player.List)
                {
                    if (!plugin.Guards.Contains(p) && p != plugin.VIP) 
                        p.Kill();
                }
                Map.Broadcast(10, "The VIP has escaped. The guards and VIP have won!");
            }
		}
    }
}
