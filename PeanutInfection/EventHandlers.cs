using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;

namespace PeanutInfection
{
    public class EventHandlers
    {
        private readonly Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        public void OnPlayedDied(DiedEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;
            
            if (ev.Killer.Role == RoleType.Scp173)
                Timing.CallDelayed(1f, () =>
                {
                    ev.Target.SetRole(RoleType.Scp173, true);
                    ev.Target.Position = ev.Killer.Position;
                });

            int counter = 0;
            foreach (Player player in Player.List)
                if (player.Role == RoleType.ClassD)
                    counter++;
            
            if (counter == 0)
                plugin.Methods.EndRound();
        }

        public void OnPlayerEscape(EscapingEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;
            
            if (ev.Player.Role == RoleType.ClassD)
                plugin.EscapeCounter++;
        }

        public void OnEndingRound(EndingRoundEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;
            
            if (!plugin.IsEnded)
            {
                ev.IsAllowed = false;
                ev.IsRoundEnded = false;
                
                return;
            }

            ev.LeadingTeam = plugin.EscapeCounter > 0 ? LeadingTeam.ChaosInsurgency : LeadingTeam.Anomalies;
            ev.IsRoundEnded = true;
        }

        public void OnRoundStart()
        {
            if (!plugin.IsEnabled)
                return;
            
            plugin.Methods.SetupPlayers();
        }

        public void OnRoundEnd(RoundEndedEventArgs ev)
        {
            if (!plugin.IsRunning)
                return;

            plugin.IsRunning = false;
            if (plugin.ShouldDisableNextRound)
                plugin.IsEnabled = false;
        }
    }
}