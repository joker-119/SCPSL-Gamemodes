using System;
using Exiled.API.Features;

namespace Bloodbath
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "Galaxy119";
        public override string Name { get; } = "Bloodbath";
        public override string Prefix { get; } = "gamemode_bloodbath";
        public override Version Version { get; } = new Version(1,0,0);
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 12);

        public static Plugin Singleton;
        
        public Methods Methods;
        public EventHandlers EventHandlers;
        public bool IsEnabled = false;
        public bool IsRunning = false;
        public bool ShouldDisableNextRound = false;
        public Random Rng = new Random();
        public string PermissionString = "gamemodes.staff";
        
        public override void OnEnabled()
        {
            Singleton = this;
            EventHandlers = new EventHandlers(this);
            Methods = new Methods(this);
            
            Methods.EventRegistration();
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Methods.EventRegistration(true);
            EventHandlers = null;
            Methods = null;
            
            base.OnDisabled();
        }
    }
}