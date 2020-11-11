using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using Exiled.API.Interfaces;
using Exiled.Permissions.Extensions;

namespace GamemodeManager.Commands
{
    public class Disable : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(Plugin.Singleton.Config.RequiredPermissions))
            {
                response = "You are not permitted to run this command.";
                return false;
            }
            
            string[] args = arguments.Array;

            if (args == null || args.Length < 3)
            {
                response = "You must specify a gamemode to run this command on.";
                return false;
            }

            IPlugin<IConfig> gamemodePlugin = null;
            ICommand enableCommand = null;
            foreach (IPlugin<IConfig> plugin in Plugin.Singleton.LoadedGamemodes.Keys)
                if (string.Equals(plugin.Name, args[2], StringComparison.CurrentCultureIgnoreCase))
                {
                    gamemodePlugin = plugin;
                    if (Plugin.Singleton.GamemodeEnableCommands.ContainsKey(plugin))
                        enableCommand = Plugin.Singleton.GamemodeEnableCommands[plugin];
                    break;
                }

            bool forced = args.Any(a => a == "-f" || a == "force");
            response = gamemodePlugin == null
                ?
                $"The specified gamemode {args[2]} is not installed. Use 'gamemode help list' to get a list of valid gamemodes."
                : enableCommand == null
                    ? $"The specified gamemode {args[2]} does not have a disable command implemented."
                    : string.Empty;  
            return enableCommand == null || Plugin.Singleton.Methods.DisableGamemode(gamemodePlugin, out response, forced, sender);
        }

        public string Command { get; } = "disable";
        public string[] Aliases { get; } = { "d" };
        public string Description { get; } = "Disables a specified gamemode.";
    }
}