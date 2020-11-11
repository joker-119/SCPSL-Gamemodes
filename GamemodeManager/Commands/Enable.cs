using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Interfaces;
using Exiled.Permissions.Extensions;

namespace GamemodeManager.Commands
{
    public class Enable : ICommand
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
                    ? $"The specified gamemode {args[2]} does not have an enable command implemented."
                    : forced && Plugin.Singleton.QueueHandler.IsAnyGamemodeActive
                        ? "You cannot forcefully start a gamemode while another is in progress."
                        : string.Empty;
            string extra = string.Empty;
            if (args.Length > 3)
                foreach (string s in args)
                    if (s != args[0] && s != args[1] && s != args[2])
                        extra += $"{s} ";
            extra = extra.Trim();
            return enableCommand == null || Plugin.Singleton.Methods.EnableGamemode(gamemodePlugin, out response, forced, sender, extra);
        }

        public string Command { get; } = "enable";
        public string[] Aliases { get; } = { "e" };
        public string Description { get; } = "Enables a specific gamemode";
    }
}