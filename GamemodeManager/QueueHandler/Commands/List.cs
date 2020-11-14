using System;
using CommandSystem;
using Exiled.API.Interfaces;
using Exiled.Permissions.Extensions;

namespace GamemodeManager.QueueHandlerCommands
{
    public class List : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(Plugin.Singleton.Config.RequiredPermissions))
            {
                response = "You are not permitted to run this command.";
                return false;
            }

            if (Plugin.Singleton.QueueHandler.Queue.Count == 0)
            {
                response = "There are no currently queued gamemodes.";
                return true;
            }

            string msg = string.Empty;

            int counter = 1;
            foreach (Tuple<IPlugin<IConfig>,string> plugin in Plugin.Singleton.QueueHandler.Queue)
            {
                msg += $"{counter}: {plugin.Item1.Name}\n";
                counter++;
            }

            response = msg;
            return true;
        }

        public string Command { get; } = "list";
        public string[] Aliases { get; } = {"l"};

        public string Description { get; } =
            "Lists all currently queued plugins, along with how many rounds they have remaining.";
    }
}