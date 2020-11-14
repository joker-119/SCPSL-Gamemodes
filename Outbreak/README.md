Outbreak Gamemode
======
## Description
Most players will spawn normally, as Class-D, Scientists, Guards, etc. However, all SCP's on the server will spawn as "Alpha Zombies". These zombies (by default) will spawn with a much larger than normal health pool, and be able to break down locked doors. During the round, anyone who takes damage from an SCP-049-2 will have a (by default 80%) chance to be infected with SCP-008. While infected, they will slowly lose health until they heal, or die. If a player dies while infected, they will turn into a normal SCP-049-2, and be tasked with helping the alphas spread the infection and kill everyone.

### Features
Optional integration with the GamemodeManager.
Highly Configurable.

### Config Settings
Config option | Config Type | Default Value | Description
:---: | :---: | :---: | :------
is_enabled | bool | true | Whether or not this gamemode is availible on the server.
all_deaths_make_zombies | bool | true | When set to true, all humans who die, to any cause, even if not infected, will turn into an SCP-049-2.
alphas_break_locked_doors | bool | true | Whether or not Alpha Zombies are able to break locked doors they cannot normally open.
infection_chance | int | 80 | The % chance a Human will become infected, when hit by an SCP-049-2.
max_alpha_count | int | 3 | The maximum number of Alpha Zombies allowed to spawn at the start of the round.
alpha_zombie_health | int | 3000 | The amount of health Alpha Zombies will spawn with.

### Commands
Command | Optional Arguments | | Description
:---: | :---: | :---: | :------
**Aliases** | **outbreak**
(alias) enable | force, -f | ~~ | Enables the gamemode, starting on the next round start. If 'force' or '-f' is used, the gamemode will start instantly.
(alias) disable | force, -f | ~~ | Disables the gamemode after the current round. If 'force' or '-f' is used, the gamemode will end immediately, and it will attempt to start a new, standard round as best it can. (Decon and nuke timer/status will not be reset.)
