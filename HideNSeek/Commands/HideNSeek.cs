using System;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace HideNSeek.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public sealed class HideNSeek : ParentCommand
    {
        public HideNSeek() => LoadGeneratedCommands();
        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new Enable());
            RegisterCommand(new Disable());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response =
                "Please use a valid sub-command.\nEnable - enables the gamemode.\nDisable - disables the gamemode.";
            return false;
        }

        public override string Command { get; } = "hidenseek";
        public override string[] Aliases { get; } = {"hide", "seek", "hide&seek"};
        public override string Description { get; } = string.Empty;
    }
}