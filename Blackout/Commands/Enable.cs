using System;
using System.Linq;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace Blackout.Commands
{
    public class Enable : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(Plugin.Singleton.PermissionString))
            {
                response = "You are not permitted to run this command.";
                return false;
            }
            
            Plugin.Singleton.Methods.EnableGamemode(arguments.Array != null && arguments.Array.Any(a => a == "-f" || a == "force"));
            response = "The gamemode has been enabled.";
            return true;
        }

        public string Command { get; } = "enable";
        public string[] Aliases { get; } = { "e" };
        public string Description { get; } = "Enables the gamemode.";
    }
}