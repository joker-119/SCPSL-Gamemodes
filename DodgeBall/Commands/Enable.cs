using System;
using System.Linq;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace DodgeBall.Commands
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

            ItemType type = ItemType.SCP018;
            if (arguments.Array != null && arguments.Array.Length > 2)
            {
                if (arguments.Array.Any(a => a == "grenade"))
                    type = ItemType.GrenadeFrag;
            }
            
            Plugin.Singleton.Methods.EnableGamemode(type, arguments.Array != null && arguments.Array.Any(a => a == "-f" || a == "force"));
            response = "The gamemode has been enabled.";
            return true;
        }

        public string Command { get; } = "enable";
        public string[] Aliases { get; } = { "e" };
        public string Description { get; } = "Enables the gamemode.";
    }
}