using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Interfaces;
using Exiled.Permissions.Extensions;

namespace GamemodeManager.QueueHandlerCommands
{
    public class Add : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(Plugin.Singleton.Config.RequiredPermissions))
            {
                response = "You are not permitted to run this command.";
                return false;
            }

            string[] args = arguments.Array;

            if (args == null || args.Length < 4)
            {
                response = "You must specify a gamemode.";
                return false;
            }

            bool allPlugins = args[3] == "all" || args[3] == "*";

            IPlugin<IConfig> plugin = null;
            
            if (!allPlugins)
            {
                foreach (IPlugin<IConfig> loadedPlugin in Plugin.Singleton.LoadedGamemodes.Keys)
                {
                    if (string.Equals(loadedPlugin.Name, args[3], StringComparison.CurrentCultureIgnoreCase))
                    {
                        plugin = loadedPlugin;
                        break;
                    }
                }

                if (plugin == null)
                {
                    response = $"The specified gamemode {args[0]} is not installed.";
                    return false;
                }
            }

            int roundCount = 1;
            foreach (string s in args)
                int.TryParse(s, out roundCount);

            string extra = string.Empty;
            if (args.Length > 4)
                foreach (string s in args)
                    if (s != args[0] && s != args[1] && s != args[2] && s != args[3])
                        extra += $"{s} ";
            extra = extra.Trim();
            
            if (allPlugins)
                foreach (IPlugin<IConfig> p in Plugin.Singleton.LoadedGamemodes.Keys)
                    Plugin.Singleton.QueueHandler.AddToQueue(p, roundCount, extra);
            else
                Plugin.Singleton.QueueHandler.AddToQueue(plugin, roundCount, extra);

            response = $"{(allPlugins ? "All plugins have" : $"{plugin.Name} has")} been added to the queue for {roundCount} rounds.";
            
            return true;
        }
 
        public string Command { get; } = "add";
        public string[] Aliases { get; } = {"a"};

        public string Description { get; } =
            "Adds a specified gamemode to the queue with the indicated number of rounds (defaults 1 if no round count specified.";
    }
}