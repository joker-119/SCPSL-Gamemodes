using System;
using Exiled.API.Features;

namespace PeanutInfection
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "galaxy119";
        public override string Name { get; } = "PeanutInfection";
        public override string Prefix { get; } = "gamemode_peanut_infection";
        public override Version Version { get; } = new Version(1,0,1);
        public override Version RequiredExiledVersion { get; } = new Version(2,1,15);

        public static Plugin Singleton;
        
        public Methods Methods;
        public EventHandlers EventHandlers;
        public Random Rng = new Random();
        public bool IsEnabled = false;
        public bool IsRunning = false;
        public bool ShouldDisableNextRound = false;
        public string PermissionString = "gamemodes.staff";
        public int EscapeCounter = 0;
        public bool IsEnded = false;

        public override void OnEnabled()
        {
            Singleton = this;
            Methods = new Methods(this);
            EventHandlers = new EventHandlers(this);
            
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