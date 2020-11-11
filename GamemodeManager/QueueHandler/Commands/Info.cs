using System;
using System.Collections.Generic;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace GamemodeManager.QueueHandlerCommands
{
    public class Help : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(Plugin.Singleton.Config.RequiredPermissions))
            {
                response = "You are not permitted to run this command.";
                return false;
            }
            
            string msg = string.Empty;
            foreach (Dictionary<Type, ICommand> collection in Plugin.Singleton.Commands.Values)
                foreach (ICommand command in collection.Values)
                    if (command.Command == "add" || command.Command == "remove" || command.Command == "update" || command.Command == "list" && !msg.Contains($"{command.Description}"))
                        msg += $"{command.Command} - {command.Description}\n";
            
            response = msg;
            
            return true;
        }

        public string Command { get; } = "help";
        public string[] Aliases { get; } = {"h"};
        public string Description { get; } = "Gets a list of valid queue handler commands and their usage.";
    }
}