Dodgeball Gamemode
======
## Description
Everyone on the server will spawn as a Class-D inside of a small room. Every few seconds, a number of SCP-018's, or grenades, will spawn in the room in random locations moving in random directions. The last Class-D alive is the winner.

### Features
Optional integration with the GamemodeManager.

### Config Settings
Config option | Config Type | Default Value | Description
:---: | :---: | :---: | :------
is_enabled | bool | true | Whether or not this gamemode will be usable on the server.
new_dodge_delay | float | 5.5 | How long between each set of Balls/grenades are 'thrown'.

### Commands
Command | Optional Arguments | | Description
:---: | :---: | :---: | :------
**Aliases** | **dodgeball**
(alias) enable | force, -f, grenade | ~~ | Enables the gamemode, starting on the next round start. If 'force' or '-f' is used, the gamemode will start instantly. Using the "grenade" optional argument will cause the balls to instead be Frag Grenades.
(alias) disable | force, -f | ~~ | Disables the gamemode after the current round. If 'force' or '-f' is used, the gamemode will end immediately, and it will attempt to start a new, standard round as best it can. (Decon and nuke timer/status will not be reset.)
