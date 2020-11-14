TeamDeathmatch Gamemode
======
## Description
Half of the server will spawn as Chaos Insurgency, and half of the server as MTF Commanders. They must fight eachother on the surface. Who will win, the MTF with their higher health, or the Chaos, with their natural high ground above their spawn? 

### Features
Optional integration with the GamemodeManager.
Players are given similar inventories regardless of their team.
Configurable extra items to give each player when they spawn.
Players will have a large amount of ammo for all weapons.

### Config Settings
Config option | Config Type | Default Value | Description
:---: | :---: | :---: | :------
is_enabled | bool | true | Whether or not this gamemode is availible on the server.
additional_items | ItemType-list | empty | The list of additional items to give players when the rounds starts.

### Commands
Command | Optional Arguments | | Description
:---: | :---: | :---: | :------
**Aliases** | **teamdeathmatch** | **tdm**
(alias) enable | force, -f, TimeInSeconds | ~~ | Enables the gamemode, starting on the next round start. If 'force' or '-f' is used, the gamemode will start instantly. If a time (in seconds) is given here, a timer will start when the round does. When it expires, the round ends, the winning team is determined by which team has more people alive when the timer has ended.
(alias) disable | force, -f | ~~ | Disables the gamemode after the current round. If 'force' or '-f' is used, the gamemode will end immediately, and it will attempt to start a new, standard round as best it can. (Decon and nuke timer/status will not be reset.)
