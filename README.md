# Full Health

A mod for the game R.E.P.O that restores full health to players at the beginning of each level.

## Description

Your health will be restored every time you spawn in a level, shop or lobby.  
No more buying health packs or using a bug to restore health.  
There are now more options to buy health packs to restore health when injured during the game, rather than to restore
health after the previous level.

## Showcase

| Without mod                                                                                                           | With mod                                                                                                        |
|-----------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------|
| ![WithoutMod](https://raw.githubusercontent.com/yvsimakov/R.E.P.O_FullHealth/refs/heads/main/showcase/WithoutMod.gif) | ![WithMod](https://raw.githubusercontent.com/yvsimakov/R.E.P.O_FullHealth/refs/heads/main/showcase/WithMod.gif) |

## Features

- Automatically heal all players or yourself at the start of various phases of the game.
- You can also activate heal for all players or yourself even if you are a client in the configuration.
- Works in multiplayer and in single player.
- All features can be changed in the configuration.

## Installation

1. Install [BepInEx](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.23.2)
2. Place the `FullHealth.dll` file in the `BepInEx/plugins` folder.
3. Configuration will be generated and can be changed after the first launch of the game at:
   `BepInEx/config/FullHealth.cfg`

## Configuration

| Variable  | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                    | Default |
|-----------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|---------|
| Enabled   | Indicates whether the mod is enabled                                                                                                                                                                                                                                                                                                                                                                                                                           | true    |
| Percent   | Percentage of maximum health to which the player will be healed (Min: 0, Max: 100)                                                                                                                                                                                                                                                                                                                                                                             | 100     |
| WorkMode  | Configures the work mode.<br>Warning: You are using 'HostAndClient' value for this parameter at your own risk.<br>It may stop working after future game updates.<br>If another lobby players do not agree with this parameter, do not use it.<br>Also, the 'Percent' parameter may not work correctly with this parameter, since the host or other clients may also perform the heal.<br>(Host - works only on host, HostAndClient - works on host and client) | Host    |
| HealMode  | Configures the heal mode.<br> (All - heal all, Self - heal self)                                                                                                                                                                                                                                                                                                                                                                                               | All     |
| PhaseMode | Configures in which phases of the game health restoration will be triggered.<br>Level - when spawning in the level.<br>Shop - when spawning in the shop. Attention! Healing in the shop does not affect the game.<br>Lobby - when spawning in the truck (not menu) before starting the next level.<br>All - in all phases.                                                                                                                                     | All     |

## Additionally

- If you find some bug, or you have a suggestion for improvement, please create
  an [issue](https://github.com/yvsimakov/R.E.P.O_FullHealth/issues).