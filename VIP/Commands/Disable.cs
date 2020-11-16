using System;
using System.Linq;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace VIP.Commands
{
    public class Disable : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(Plugin.Singleton.PermissionString))
            {
                response = "You are not permitted to run this command.";
                return false;
            }

            Plugin.Singleton.Methods.DisableGamemode(arguments.Array != null && arguments.Array.Any(a => a == "-f" || a == "force"));
            response = "The gamemode has been disabled.";
            return true;
        }

        public string Command { get; } = "disable";
        public string[] Aliases { get; } = { "d" };
        public string Description { get; } = "Disables the gamemode.";
    }
}