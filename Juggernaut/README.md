Juggernaut Gamemode
======
## Description
All players on the server (save a single randomly selected one to be the Juggernaut) will spawn as MTF Commanders on the surface. The Juggernaut will spawn as a Chaos Insurgency in SCP-939's spawn. The MTF will need to enter the facility and kill the Juggernaut. If they succeed, they win, if the Juggernaut is able to terminate all MTF Commanders before they respawn more, the Juggernaut wins.

### Features
Optional integration with the GamemodeManager.
Configurable Health for the Juggernaut.
Configurable Inventory for the Juggernaut.

### Config Settings
Config option | Config Type | Default Value | Description
:---: | :---: | :---: | :------
is_enabled | bool | true | Whether or not this gamemode is availible on the server.
infinite_jugg_grenades | bool | true | Whether or not the Juggernaut should have infinite grenades. How this works: Whenever the Juggernaut throws a grenade, it will respawn another replacement of it in their inventory. If they drop a grenade normally, instead of throwing, it does not replace it.
commanders_get_micro | bool | true | Whether or not all MTF Commanders should receive a Micro-HID when they spawn.
juggernaut_health | int | 10000 | The amount of health the Juggernaut should start the round with.
juggernaut_inventory | ItemType-list | empty | The inventory to assign to the Juggernaut when they spawn. With an empty list, they will recieve whatever the default Chaos Insurgency inventory is on your server. If you provide **ANY** items in this list, it will **COMPLETELY OVERRIDE** their inventory, with this list. Please keep that in mind. This special inventory is given on a longer delay from when they spawn than usual, and thus should not be interfered with by other plugins that change a role's default inventory.

### Commands
Command | Optional Arguments | | Description
:---: | :---: | :---: | :------
**Aliases** | **juggernaut** | **jugg**
(alias) enable | force, -f | ~~ | Enables the gamemode, starting on the next round start. If 'force' or '-f' is used, the gamemode will start instantly.
(alias) disable | force, -f | ~~ | Disables the gamemode after the current round. If 'force' or '-f' is used, the gamemode will end immediately, and it will attempt to start a new, standard round as best it can. (Decon and nuke timer/status will not be reset.)
