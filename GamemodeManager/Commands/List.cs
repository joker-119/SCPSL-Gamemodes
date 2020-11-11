using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Interfaces;

namespace GamemodeManager.Commands
{
    public class List : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string[] args = arguments.Array;
            if (args == null || args.Length < 1)
            {
                response =
                    "This command will help give a list of installed gamemodes and their plugins. Use 'gamemode help list' to list all installed gamemodes, or 'gamemode help NAME' to get a list of all commands for a specific gamemode.";
                return false;
            }

            string msg = string.Empty;

            if (Plugin.Singleton.LoadedGamemodes.Count > 0)
                foreach (IPlugin<IConfig> plugin in Plugin.Singleton.LoadedGamemodes.Keys)
                    msg += $"{plugin.Name}\n";

            if (string.IsNullOrEmpty(msg))
            {
                response = "There are no currently installed gamemodes.";
                return false;
            }

            response = msg;
            return true;
        }

        public string Command { get; } = "list";
        public string[] Aliases { get; } = { "l" };
        public string Description { get; } = "Gets a list of all gamemode commands with descriptions.";
    }
}