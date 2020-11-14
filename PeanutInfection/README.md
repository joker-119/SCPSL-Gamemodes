PeanutInfection Gamemode
======
## Description
Everyone on the server (save a few randomly selected to be SCP-173s) are spawned as Class-D. The Peanuts are tasked with killing all of the Class-D, while the Class-D attempt to fight back any way they can. Whenever an SCP-173 kills a player, that player will turn into another, new SCP-173.

### Features
Optional integration with the GamemodeManager.

### Config Settings
Config option | Config Type | Default Value | Description
:---: | :---: | :---: | :------
is_enabled | bool | true | Whether or not this gamemode is availible on the server.
smart_scp_selection | bool | true | Whether or not the plugin should select the number of SCP-173's based on the server's current player count.
max_starting_scp_count | int | 3 | The maximum number of SCP-173s that can spawn at the start of the round. If smart_scp_selection is set to false, it will always spawn this many SCP-173's.

### Commands
Command | Optional Arguments | | Description
:---: | :---: | :---: | :------
**Aliases** | **peanutinfection** | **pinfect**
(alias) enable | force, -f | ~~ | Enables the gamemode, starting on the next round start. If 'force' or '-f' is used, the gamemode will start instantly.
(alias) disable | force, -f | ~~ | Disables the gamemode after the current round. If 'force' or '-f' is used, the gamemode will end immediately, and it will attempt to start a new, standard round as best it can. (Decon and nuke timer/status will not be reset.)
