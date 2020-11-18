using System;
using Exiled.API.Features;

namespace Juggernaut
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "galaxy119";
        public override string Name { get; } = "Juggernaut";
        public override string Prefix { get; } = "gamemode_juggernaut";
        public override Version Version { get; } = new Version(1,0,1);
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 15);
        public static Plugin Singleton;

        public Methods Methods;
        public EventHandlers EventHandlers;
        public Random Rng = new Random();
        public bool IsEnabled = false;
        public bool IsRunning = false;
        public bool ShouldDisableNextRound = false;
        public string PermissionString = "gamemode.staff";

        internal Player Juggernaut;

        public override void OnEnabled()
        {
            Singleton = this;
            Methods = new Methods(this);
            EventHandlers = new EventHandlers(this);

            try
            {
                Config.ParseInventory();
            }
            catch (Exception e)
            {
                Log.Error($"Unhandled exception parsing juggernaut inventory: {e}");
            }

            Methods.RegisterEvents();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
        }
    }
}