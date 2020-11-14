Bloodbath Gamemode
======
## Description
The entire server, save a few players selected at random to become SCP-173's, are spawned as Class-D in the Tutorial tower. After a few seconds, the SCP-173's will be teleported there to join and kill them. The SCP-173's will be given god mode, and low health. The last surviving Class-D is the winner.

### Features
Optional Integration with the GamemodeManager.

### Config Settings
Config option | Config Type | Default Value | Description
:---: | :---: | :---: | :------
is_enabled | bool | true | Whether or not this gamemode will be usable on the server.
smart_scp_selection | bool | true | Whether the plugin should select the number of SCP's to spawn based on current player count (true), or force the count to always be the same (false).
max_scp_count | int | 3 | The maximum number of SCP's that can be spawned for the gamemode round. When smart_scp_selection is set to false, it will always spawn this many SCPs.
remaining_broadcast | string | $count Class-D remaining! | The broadcast text sent to all players when a Class-D is eliminated.
remaining_broadcast_dur | ushort | 5 | How long to display the remaining_broadcast message for. Set this to -1 to disable this broadcast.
end_round_broadcast | string | %user is the winner! | The broadcast to send when only a single Class-D remains, and is the winner.
end_round_broadcast_dur | ushort | 15 | How long to display the end_round_broadcast. Set this to -1 to disable this broadcast.

### Commands
Command | Optional Arguments | | Description
:---: | :---: | :---: | :------
**Aliases** | **bloodbath**
(alias) enable | force, -f | ~~ | Enables the gamemode, starting on the next round start. If 'force' or '-f' is used, the gamemode will start instantly.
(alias) disable | force, -f | ~~ | Disables the gamemode after the current round. If 'force' or '-f' is used, the gamemode will end immediately, and it will attempt to start a new, standard round as best it can. (Decon and nuke timer/status will not be reset.)
