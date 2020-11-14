Gungame Gamemode
======
## Description
**NOTE: It is recommended that you disable the base-game friendly fire auto-ban, and/or any plugins you may have related to that function, before using this gamemode.**
All players are spawn in random rooms in Light-Containment Zone, as Class-D with a single SCP-018. Their objective is to kill other players with their SCP-018, thus granting them a COM-15. Killing people with each gun time will further upgrade their weapon along the following scale:
SCP-018 -> COM15 -> USP -> MP7 -> Logicer -> P90 -> E-11 -> Frag Grenade
The first player who gets a kill with a Frag Grenade is the winner.

### Features
Optional integration with the GamemodeManager.

Friendly Fire is returned back it's old setting once the round has completed.
Players are prohibited from upgrading their weapon "too quickly" to prevent multi-kills with SCP-018 from giving them a big advantage.
Players are prohibited from picking up items during the round, to prevent picking up a weapon they have not yet earned and "jumping the ladder".
Players are given infinite SCP-018 & Frag Grenade throws when they are on those weapon tiers.
Players are given infinite ammo for all weapons.
Players are unable to injure themselves with SCP-018 or Frag Grenades during the round.
When a player is killed during the round, they will respawn a short time later, but will start over with an SCP-018 again.
### Config Settings
Config option | Config Type | Default Value | Description
:---: | :---: | :---: | :------
is_enabled | bool | true | Whether or not this gamemode is availible on the server.
upgrade_delay | float | 1.5 | How long a player must wait after receiving an upgrade, before they can receive another. Set this to 0 to disable the delay. NOTE: Setting this value too low, can result in SCP-018 multi-kills cause a player to receive multiple upgrades at once, causing an unfair advantage.
respawn_time | float | 5 | How long it will take for a player who dies during the round to respawn.
### Commands
Command | Optional Arguments | | Description
:---: | :---: | :---: | :------
**Aliases** | **gungame**
(alias) enable | force, -f | ~~ | Enables the gamemode, starting on the next round start. If 'force' or '-f' is used, the gamemode will start instantly.
(alias) disable | force, -f | ~~ | Disables the gamemode after the current round. If 'force' or '-f' is used, the gamemode will end immediately, and it will attempt to start a new, standard round as best it can. (Decon and nuke timer/status will not be reset.)
