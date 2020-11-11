using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CommandSystem;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Events.EventArgs;
using Exiled.Loader;
using YamlDotNet.Core;
using Player = Exiled.Events.Handlers.Player;

namespace GamemodeManager
{
    public class Methods
    {
        private readonly Plugin _plugin;
        public Methods(Plugin plugin) => _plugin = plugin;

        public bool IsGamemodeEnabled(IPlugin<IConfig> plugin) => (bool) plugin.GetInstanceField("IsEnabled");

        public void SetGamemodeRoundBool(IPlugin<IConfig> plugin, bool value) => plugin.SetInstanceField("ShouldDisableNextRound", value);

        public bool EnableGamemode(IPlugin<IConfig> plugin, out string response, bool force = false, ICommandSender sender = null, string extraArgs = "")
        {
            try
            {
                Log.Debug($"{plugin.Name} enable command run.", _plugin.Config.Debug);
                _plugin.GamemodeEnableCommands[plugin]
                    .Execute(new ArraySegment<string>(new[] {"enable", extraArgs, force ? "force" : string.Empty}), sender ?? Server.Host.Sender, out response);
                Log.Debug($"{plugin.Name} Enable: {response}", _plugin.Config.Debug);
            }
            catch (Exception e)
            {
                Log.Error($"{e}");
                response = "Internal server error.";
                return false;
            }

            return true;
        }

        public bool DisableGamemode(IPlugin<IConfig> plugin, out string response, bool force = false, ICommandSender sender = null)
        {
            try
            {
                Log.Debug($"{plugin.Name} disable command run.", _plugin.Config.Debug);
                _plugin.GamemodeEnableCommands[plugin]
                    .Execute(new ArraySegment<string>(new[] {"disable", force ? "force" : string.Empty}),
                        sender ?? Server.Host.Sender, out response);
                Log.Debug($"{plugin.Name} Disable: {response}", _plugin.Config.Debug);
            }
            catch (Exception e)
            {
                Log.Error($"{e}");
                response = "Internal server error.";
                return false;
            }

            return true;
        }

        public void LoadGamemodes()
        {
            Log.Info("Loading gamemode plugins...");
            
            foreach (string gamemode in Directory.GetFiles(_plugin.Config.GamemodeDirectory))
            {
                if (!gamemode.EndsWith(".dll"))
                    continue;
                
                IPlugin<IConfig> gamemodePlugin = Loader.CreatePlugin(Loader.LoadAssembly(gamemode));
                if (!_plugin.LoadedGamemodes.ContainsKey(gamemodePlugin))
                    _plugin.LoadedGamemodes.Add(gamemodePlugin, new List<ICommand>());
                else
                    Log.Warn($"LOADED TWO OF THE SAME GAMEMODE: {gamemodePlugin.Name}! THIS WILL CAUSE ISSUES UNLESS YOU KNOW WHAT YOU ARE DOING AND THIS IS INTENTIONAL!");
                
                gamemodePlugin.OnEnabled();
                gamemodePlugin.OnRegisteringCommands();
                
                foreach (ICommand command in gamemodePlugin.Commands.Values.SelectMany(dict => dict.Values))
                {
                    if (_plugin.LoadedGamemodes[gamemodePlugin].Contains(command))
                        _plugin.LoadedGamemodes[gamemodePlugin].Add(command);
                    if (!_plugin.GamemodeEnableCommands.ContainsKey(gamemodePlugin))
                        _plugin.GamemodeEnableCommands.Add(gamemodePlugin, command);
                }
                
                Log.Info($"Loaded {gamemodePlugin.Name}.");
            }

            ReloadGamemodeConfigs();
        }

        public bool ReloadGamemodeConfigs() => SaveGamemodeConfigs(LoadGamemodeConfigs(ReadGamemodeConfig()));
        
        public bool SaveGamemodeConfigs(Dictionary<string, IConfig> configs)
        {
            try
            {
                if (configs == null || configs.Count == 0)
                    return false;

                return SaveGamemodeConfigs(ConfigManager.Serializer.Serialize(configs));
            }
            catch (YamlException yamlException)
            {
                Log.Error($"An error has occurred while serializing configs: {yamlException}");

                return false;
            }
        }
        
        public bool SaveGamemodeConfigs(string configs)
        {
            try
            {
                File.WriteAllText(Path.Combine(_plugin.Config.GamemodeDirectory, "configs.yml"), configs ?? string.Empty);

                return true;
            }
            catch (Exception exception)
            {
                Log.Error($"An error has occurred while saving configs to {Paths.Config} path: {exception}");

                return false;
            }
        }
        
        public string ReadGamemodeConfig()
        {
            try
            {
                if (File.Exists(Path.Combine(_plugin.Config.GamemodeDirectory, "configs.yml")))
                    return File.ReadAllText(Path.Combine(_plugin.Config.GamemodeDirectory, "configs.yml"));
            }
            catch (Exception exception)
            {
                Log.Error($"An error has occurred while reading configs from {Paths.Config} path: {exception}");
            }

            return string.Empty;
        }

        public Dictionary<string, IConfig> LoadGamemodeConfigs(string rawConfigs)
        {
            try
            {
                Log.Info("Loading plugin configs...");

                rawConfigs = Regex.Replace(rawConfigs, @"\ !.*", string.Empty).Replace("!Dictionary[string,IConfig]", string.Empty);

                Dictionary<string, object> rawDeserializedConfigs = ConfigManager.Deserializer.Deserialize<Dictionary<string, object>>(rawConfigs) ?? new Dictionary<string, object>();
                Dictionary<string, IConfig> deserializedConfigs = new Dictionary<string, IConfig>();

                foreach (IPlugin<IConfig> plugin in Plugin.Singleton.LoadedGamemodes.Keys)
                {
                    if (!rawDeserializedConfigs.TryGetValue(plugin.Prefix, out object rawDeserializedConfig))
                    {
                        Log.Warn($"{plugin.Name} doesn't have default configs, generating...");

                        deserializedConfigs.Add(plugin.Prefix, plugin.Config);
                    }
                    else
                    {
                        try
                        {
                            deserializedConfigs.Add(plugin.Prefix, (IConfig)ConfigManager.Deserializer.Deserialize(ConfigManager.Serializer.Serialize(rawDeserializedConfig), plugin.Config.GetType()));

                            plugin.Config.CopyProperties(deserializedConfigs[plugin.Prefix]);
                        }
                        catch (YamlException yamlException)
                        {
                            Log.Error($"{plugin.Name} configs could not be loaded, some of them are in a wrong format, default configs will be loaded instead! {yamlException}");

                            deserializedConfigs.Add(plugin.Prefix, plugin.Config);
                        }
                    }
                }

                Log.Info("Plugin configs loaded successfully!");

                return deserializedConfigs;
            }
            catch (Exception exception)
            {
                Log.Error($"An error has occurred while loading configs! {exception}");

                return null;
            }
        }
    }
}