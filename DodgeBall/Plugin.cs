using System;
using Exiled.API.Features;
using MEC;

namespace DodgeBall
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "galaxy119";
        public override string Name { get; } = "DodgeBall";
        public override string Prefix { get; } = "gamemode_DodgeBall";
        public override Version Version { get; } = new Version(1, 0, 1);
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 15);

        public static Plugin Singleton;

        public Methods Methods { get; private set; }
        public EventHandlers EventHandlers { get; private set; }
        public ItemType Type { get; set; }
        public CoroutineHandle Coroutine { get; set; }

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