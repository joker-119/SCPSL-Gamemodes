using System;
using CommandSystem;
using GamemodeManager.QueueHandlerCommands;

namespace GamemodeManager.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Gamemode : ParentCommand
    {
        public Gamemode() => LoadGeneratedCommands();
        public sealed override void LoadGeneratedCommands()
        {
            RegisterCommand(new List());
            RegisterCommand(new Enable());
            RegisterCommand(new Disable());
            RegisterCommand(new Queue());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender,
            out string response)
        {
            response = "Please specify a valid subcommand. Use 'gamemode help' for more info.";
            return false;
        }

        public override string Command { get; } = "gamemode";
        public override string[] Aliases { get; } = { "gmm" };
        public override string Description { get; } = string.Empty;
    }
}