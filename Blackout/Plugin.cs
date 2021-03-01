using System;
using System.Collections.Generic;
using Exiled.API.Features;

namespace Blackout
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "Jorrit";
        public override string Name { get; } = "Blackout";
        public override string Prefix { get; } = "gamemode_outbreak";
        public override Version Version { get; } = new Version(1,1,1);
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 29);
        public static Plugin Singleton;

        public Methods Methods;
        public EventHandlers EventHandlers;
        public Random Rng = new Random();
        public bool IsEnabled = false;
        public bool IsRunning = false;
        public bool ShouldDisableNextRound = false;
        public string PermissionString = "gamemodes.staff";

        public override void OnEnabled()
        {
            Singleton = this;
            Methods = new Methods(this);
            EventHandlers = new EventHandlers(this);
            
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