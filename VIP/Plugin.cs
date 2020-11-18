using System;
using System.Collections.Generic;
using Exiled.API.Features;
using MEC;

namespace VIP
{
    public class Plugin : Plugin<Config>
    { 
        public override string Author { get; } = "Steven4547466";
        public override string Name { get; } = "VIP";
        public override string Prefix { get; } = "gamemode_vip";
        public override Version Version { get; } = new Version(1, 0, 1);
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 15);

        public static Plugin Singleton;

        public Methods Methods;
        public EventHandlers EventHandlers;
        public bool IsEnabled = false;
        public bool IsRunning = false;
        public bool ShouldDisableNextRound = false;
        public Random Rng = new Random();
        public string PermissionString = "gamemodes.staff";

        internal Player Vip;
        internal List<Player> Guards;
        internal CoroutineHandle Coroutine;

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
