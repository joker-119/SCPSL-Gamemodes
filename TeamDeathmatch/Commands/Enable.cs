using System;
using System.Linq;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace TeamDeathmatch.Commands
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


            float timer = 0f;
            if (arguments.Array != null && arguments.Array.Length > 3) 
                float.TryParse(arguments.Array[2], out timer);

            Plugin.Singleton.Methods.EnableGamemode(timer, arguments.Array != null && arguments.Array.Any(a => a == "-f" || a == "force"));
            response = "The gamemode has been enabled.";
            return true;
        }

        public string Command { get; } = "enable";
        public string[] Aliases { get; } = { "e" };
        public string Description { get; } = "Enables the gamemode.";
    }
}