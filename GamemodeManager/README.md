GamemodeManager
======
## Description
A tool to permit optional, unified control, and queue system, for gamemode plugins. This tool does not requires any gamemodes to be installed, nor do any gamemodes require it to be installed. The GMM acts like a middle-man between EXILED and the gamemode plugins during loading. This permits it to create a list of supported gamemodes during load-time, without needing to know which ones are present, or which ones are *possible* to be present, beforehand. This tool will work with any gamemode plugin constructed with the GMM's existance in mind.

### Features
Integration with any gamemode plugin constructed with the GMM in mind, even future ones created by other plugin authors, without any need for those plugins to require GMM as a dependency.
A simple but powerful command-based gamemode queue system. (File-based queue coming soon, to a theatre near you!)

### Config Settings
Config option | Config Type | Default Value | Description
:---: | :---: | :---: | :------
is_enabled | bool | true | Whether or not this gamemode will be usable on the server.
debug | bool | false | Whether or not to display DEBUG messages in the console.
required_permissions | string | gamemodes.staff | The EXILED Permission string required by users to run GMM based commands.
gamemode_directory | string | (.config or %AppData%)/EXILED/Plugins/Gamemodes | The folder GMM will look for gamemode plugins in.

### Commands
Command | Optional Arguments | | Description
:---: | :---: | :---: | :------
**Aliases** | **gamemode**
gamemode enable (name) | OptionalArgs | ~~ | Enabled the specified gamemode. If any Optional Arguments are used, they will be passed into the gamemodes Enable command. (Refer to the gamemode's documentation for optional arguments, if any.)
gamemode disable (name) | OptionalArgs | ~~ | Disables the specified gamemode. If any Optional Arguments are used, they will be passed into the gamemodes Disable command. (Refer to the gamemode's documentation for optional arguments, if any.)
gamemode list | ~~ | ~~ | Gets a list of all valid gamemode plugins that are installed in the gamemode_directory folder, that GMM can manage.
gamemode queue help | ~~ | ~~ | Prints a list of valid gamemode queue subcommands, and their descriptions.
gamemode queue list | ~~ | ~~ | Prints a list of all currently queued gamemodes, and their index number.
gamemode queue add (name) | NumberOfRounds | OptionalArgs | Adds the specified gamemode to the end of the existing queue (if any). OptionalArgs are passed into the plugin's Enable command when it's that plugin's turn to run. If a NumberOfRounds (a whole number only) is defined, it will be added to the queue that many times.
gamemode queue remove (index/name) | ~~ | ~~ | Index: Removes a single entry from the gamemode queue list, removing only the item at the specified index number. Name: Removes all entries for the specified gamemode from the queue list.

### How to use (For server owners):
1. Install this as a normal plugin on your server.
2. Create a folder in the gamemode_directory (by default, this will be .config/EXILED/Plugins/Gamemodes on Linux, or %AppData%\EXILED\Plugins\Gamemodes on Windows).
3. Place whatever gamemode plugins you want GMM to be able to manage, in this newly created Gamemodes folder.
4. User GMM commands to enable/disable/queue gamemodes, instead of the normal gamemode plugin commands. Optional arguments on the gamemode enable, gamemode disable and gamemode queue add commands are passed through to the plugin. IE: dodgeball enable grenade would change to; gamemode enable dodgeball grenade, or gamemode queue add dodgeball grenade, and the optional argument will still work as intended.

### How to make a Gamemode Plugin that GMM can manage (FOR DEVELOPERS ONLY):
1. Create the plugin as a standard EXILED Framework plugin. **DO NOT ADD GMM AS A REFERENCE FOR YOUR PLUGIN, IT IS NOT NECESSARY!**
2. Make sure your plugin implements a ParentCommand, for continuity, this command should be the same as your gamemode's name. 
3. Register 2 RemoteAdminCommandHandler & GameConsoleCommandHandler commands for your plugin, "enable" and "disable".
4. Add a public bool (field, not a property), called "IsEnabled" in your plugin's : Plugin<Config> class, and make sure this gets set to true when the gamemode is enabled, and false when it is disabled.

That's it. If those things are implemented, GMM can enable/disable and manage your gamemode plugin in it's queue. How you handle those commands is entierly up to you, but I would recommend following the existing gamemode plugins here as a template, as it will help with debugging any issues that may arise from using your gamemode plugin with GMM.
