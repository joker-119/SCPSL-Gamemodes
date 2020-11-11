using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Interfaces;
using Exiled.Permissions.Extensions;

namespace GamemodeManager.QueueHandlerCommands
{
    public class Remove : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(Plugin.Singleton.Config.RequiredPermissions))
            {
                response = "You are not permitted to run this command.";
                return false;
            }

            if (arguments.Array == null || arguments.Array.Length < 4)
            {
                response = "You must specify what gamemode to remove from the queue.";
                return false;
            }

            if (int.TryParse(arguments.Array[3], out int index))
            {
                if (Plugin.Singleton.QueueHandler.Queue.Count < index)
                {
                    response = "Invalid index given. Use list to get a list of the current queue.";
                    return false;
                }
                
                index -= 1;
                string name = Plugin.Singleton.QueueHandler.Queue[index].Item1.Name;
                Plugin.Singleton.QueueHandler.RemoveQueueItem(index);

                response = $"{index + 1}: {name} was removed from the queue.";
                return false;
            }

            IPlugin<IConfig> plugin =
                Plugin.Singleton.QueueHandler.Queue.FirstOrDefault(p => string.Equals(p.Item1.Name, arguments.Array[3], StringComparison.CurrentCultureIgnoreCase))?.Item1;
            if (plugin == null)
            {
                response = "That gamemode was not found in the queue.";
                return false;
            }

            Plugin.Singleton.QueueHandler.RemoveQueueItem(plugin);
            response = $"All copies of {plugin.Name} have been removed from the queue.";
            return false;
        }

        public string Command { get; } = "remove";
        public string[] Aliases { get; } = {"r"};
        public string Description { get; } = "Removes a specified gamemode from the queue.";
    }
}