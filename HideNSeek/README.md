HidenSeek Gamemode
======
## Description
All players (save a few randomly selected ones choosen as SCP's) will spawn as Class-D in the SCP-096 spawn room. They will need to find hiding places within Heavy-Containment Zone, as the SCP's attempt to find and kill all of them. The last surviving Class-D is the winner of the round.

### Features
Optional integration with the GamemodeManager.
Selection between multiple SCP types. (more to be implemented later)
**NYI** Automated (configurable) blackouts during the round.
All Items on the map are removed.
All doors inside of HCZ are unlocked.
All checkpoints leading out of HCZ are locked. (This does mean people can take the elevators down to the LCZ checkpoints, but they cannot actually enter LCZ).

### Config Settings
Config option | Config Type | Default Value | Description
:---: | :---: | :---: | :------
is_enabled | bool | true | Whether or not this gamemode is availible on the server.
debug | bool | false | Whether or not to display DEBUG messages in the console.
disable_lights | bool | false | Whether or not to cause a facility blackout during the gamemode.
max_939_count | int | 3 | How many SCP-939's that are allowed to spawn, at most, during the round, if SCP-939 is selected.
max_049_count | int | 1 | How many SCP-049's that are allowed to spawn, at most, during the round, if SCP-049 is selected. **SCP-049 will be able to make zombies out of anyone he kills, as such, it is recommended to keep this number low.**
end_round_broadcast | string | %user is the winner! | The broadcast message displayed to players when the round ends.
end_round_broadcast_dur | ushort | 15 | The duration of the end_round_broadcast. Set this to -1 to disable this broadcast.
tesla_ignore | string-list | allscp | What roles should NOT trigger tesla gates when they approach them. Tesla gates can still kill these roles, but will not automatically trigger when only they are nearby. Valid role identifiers are: Any role name, "allscp" for all SCP roles, "allhuman" for all human roles.

### Commands
Command | Optional Arguments | | Description
:---: | :---: | :---: | :------
**Aliases** | **hidenseek**
(alias) enable | force, -f, RoleType | ~~ | Enables the gamemode, starting on the next round start. If 'force' or '-f' is used, the gamemode will start instantly. When invoked with a RoleType argument, such as "Scp049" or "Scp173", this will be the role given to all SCP's for the round. Currently, any SCP role can be given here, but only 939 and 049 have max_role_count config options to limit how many their are. Use with caution.
(alias) disable | force, -f | ~~ | Disables the gamemode after the current round. If 'force' or '-f' is used, the gamemode will end immediately, and it will attempt to start a new, standard round as best it can. (Decon and nuke timer/status will not be reset.)
