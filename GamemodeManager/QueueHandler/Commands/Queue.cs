using System;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace GamemodeManager.QueueHandlerCommands
{
    public sealed class Queue : ParentCommand
    {
        public Queue() => LoadGeneratedCommands();
        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new Help());
            RegisterCommand(new Add());
            RegisterCommand(new Remove());
            RegisterCommand(new List());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(Plugin.Singleton.Config.RequiredPermissions))
            {
                response = "You are not permitted to run this command.";
                return false;
            }

            response = "Please execute a valid subcommand. use `queue help` for a list.";
            return false;
        }

        public override string Command { get; } = "queue";
        public override string[] Aliases { get; } = { "q" };
        public override string Description { get; } = string.Empty;
    }
}