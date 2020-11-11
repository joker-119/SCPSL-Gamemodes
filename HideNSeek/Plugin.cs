using System;
using Exiled.API.Features;

namespace HideNSeek
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "Galaxy119";
        public override string Name { get; } = "HideNSeek";
        public override string Prefix { get; } = "gamemode_hidenseek";
        public override Version Version { get; } = new Version(1,0,0);
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 13);

        public static Plugin Singleton;

        public Methods Methods;
        public EventHandlers EventHandlers;
        public Random Rng = new Random();
        public bool IsEnabled = false;
        public bool IsRunning = false;
        public bool ShouldDisableNextRound = false;
        public string PermissionString = "gamemodes.staff";
        public RoleType ScpRole = RoleType.Scp93953;

        public override void OnEnabled()
        {
            Singleton = this;
            Methods = new Methods(this);
            EventHandlers = new EventHandlers(this);

            Config.ParseTeslaIgnore();
            Methods.RegisterEvents();
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Methods.RegisterEvents(true);

            Methods = null;
            EventHandlers = null;
            base.OnDisabled();
        }
    }
}