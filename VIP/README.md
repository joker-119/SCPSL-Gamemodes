VIP Gamemode
======
## Description
There is one VIP (Scientist). Guards (MTF) must escort the VIP to escape while Attackers (Chaos Insurgency) attempt to kill the VIP. The VIP has no respawns and increased health and armor.

### Features
Optional integration with the GamemodeManager.

### Config Settings
Config option | Config Type | Default Value | Description
:---: | :---: | :---: | :------
is_enabled | bool | true | Whether or not this gamemode will be usable on the server.
max_duration | float | 600 | The maximum duration of the round in seconds.
disable_item_pickup | bool | false | Whether or not to disable item pickup for everyone.
disable_item_pickup_vip | bool | true | Whether or not to disable item pickup for the VIP. Disregarded if no one can pickup items.
vip_broadcast | string | You are the VIP. Avoid Chaos Insurgency and escape. | The broadcast sent to the VIP.
guard_broadcast | string | You are a guard. Kill Chaos Insurgency, escort the VIP to escape. | The broadcast sent to the guards.
attacker_broadcast | string | You are an attacker. Kill the VIP using any means. | The broadcast sent to the attackers.
vip_starting_health | int | 300 | The VIP's starting health.
vip_starting_armor | int | 250 | The VIP's starting armor.
vip_armor_decay | bool | false | Whether or not the VIP's armor decays.
vip_armor_decay_rate | float | 0.75 | The decay rate of armor in armor/sec. Disregarded if the armor doesn't decay.
percent_of_guards | float | 30 | The percontage of players that are guards.
guards_respawn | bool | true | Whether or not guards respawn on death.
guard_respawn_delay | float | 10 | The time it takes for guards to respawn. Disregarded if guard respawn is disabled.
attackers_respawn | bool | true | Whether or not attackers respawn on death.
attacker_respawn_delay | float | 5 | The time it takes for attackers to respawn. Disregarded if attacker respawn is disabled.

### Commands
Command | Optional Arguments | | Description
:---: | :---: | :---: | :------
**Aliases** | **vip**
(alias) enable | force, -f | ~~ | Enables the gamemode, starting on the next round start. If 'force' or '-f' is used, the gamemode will start instantly.
(alias) disable | force, -f | ~~ | Disables the gamemode after the current round. If 'force' or '-f' is used, the gamemode will end immediately, and it will attempt to start a new, standard round as best it can. (Decon and nuke timer/status will not be reset.)