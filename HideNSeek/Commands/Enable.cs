using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;

namespace HideNSeek.Commands
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

            RoleType role = RoleType.Scp93953;
            if (arguments.Array != null && arguments.Array.Length > 2)
                try
                {
                    role = (RoleType) Enum.Parse(typeof(RoleType), arguments.Array[2], true);
                }
                catch (Exception)
                {
                    Log.Warn($"Enable Command: Unable to parse {arguments.Array[2]}, not a valid role. Using default role.");
                }
            
            Plugin.Singleton.Methods.EnableGamemode(role, arguments.Array != null && arguments.Array.Any(a => a == "-f" || a == "force"));
            response = "The gamemode has been enabled.";
            return true;
        }

        public string Command { get; } = "enable";
        public string[] Aliases { get; } = { "e" };
        public string Description { get; } = "Enables the gamemode.";
    }
}