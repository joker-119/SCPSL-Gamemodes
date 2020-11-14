PeanutRun Gamemode
======
## Description
Everyone on the server is spawned as an SCP-173 in the normal spawn location. All doors in the facility are locked open, and the Peanuts must run for the exit while the Alpha Warhead counts down to their death. Anyone who makes it to the surface and survives the nuke, wins.

### Features
Optional integration with the GamemodeManager.

### Config Settings
Config option | Config Type | Default Value | Description
:---: | :---: | :---: | :------
is_enabled | bool | true | Whether or not this gamemode is availible on the server.


### Commands
Command | Optional Arguments | | Description
:---: | :---: | :---: | :------
**Aliases** | **peanutrun** | **prun**
(alias) enable | force, -f | ~~ | Enables the gamemode, starting on the next round start. If 'force' or '-f' is used, the gamemode will start instantly.
(alias) disable | force, -f | ~~ | Disables the gamemode after the current round. If 'force' or '-f' is used, the gamemode will end immediately, and it will attempt to start a new, standard round as best it can. (Decon and nuke timer/status will not be reset.)
