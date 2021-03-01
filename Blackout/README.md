Outbreak Gamemode
======
## Description
Most players will spawn normally, as Class-D, Scientists, Guards, etc. However, 1-5 SCP's will spawn of your choice. All the lights are out and everyone gets a free flashlight if they spawn!

### Features
Optional integration with the GamemodeManager.

### Config Settings
Config option | Config Type | Default Value | Description
:---: | :---: | :---: | :------
is_enabled | bool | true | Whether or not this gamemode is availible on the server.
smart_scp_selection | bool | true | Whether or not the plugin should select the number of SCP's based on the server's current player count.
max_scp_count | int | 5 | The maximum number of SCP's allowed to spawn at the start of the round.
scp_to_spawn | RoleType | Scp173 | The SCP's that should spawn at the start of the round.

### Commands
Command | Optional Arguments | | Description
:---: | :---: | :---: | :------
**Aliases** | **blackout**
(alias) enable | force, -f | ~~ | Enables the gamemode, starting on the next round start. If 'force' or '-f' is used, the gamemode will start instantly.
(alias) disable | force, -f | ~~ | Disables the gamemode after the current round. If 'force' or '-f' is used, the gamemode will end immediately, and it will attempt to start a new, standard round as best it can. (Decon and nuke timer/status will not be reset.)
