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
- You can set `Percent` value to `x` and set `PhaseMode` to one of `Lobby` or `Level` in configuration to always get up
  to `x`% HP.
- You can set `ExactValue` to `x` and set `PhaseMode` to one of `Lobby` or `Level` in configuration to always get healed
  for `x` HP.
- You can set `HealthPackMode` to `All` or other combinations and set `PhaseMode` to one of `Lobby` or `Level` in
  configuration to simulate the health pack bug.
- You can set `HealRequirementMode` to `Survive` then to heal player will need to survive the previous level.

## Installation

1. Install [BepInEx](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.23.2)
2. Place the `FullHealth.dll` file in the `BepInEx/plugins` folder.
3. Configuration will be generated and can be changed after the first launch of the game at:
   `BepInEx/config/FullHealth.cfg`

## Configuration

| Variable            | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           | Default |
|---------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|---------|
| Enabled             | Indicates whether the mod is enabled.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 | true    |
| Percent             | Percentage of maximum health to which the player will be healed (Min: 0, Max: 100).<br>Attention! You must set the `PhaseMode` parameter to only one of `Level` or `Lobby`, not both. Otherwise, the healing will be done twice.                                                                                                                                                                                                                                                                                                                      | 100     |
| WorkMode            | Configures the work mode.<br>Warning: You are using `All` value for this parameter at your own risk.<br>It may stop working after future game updates.<br>If another lobby players do not agree with this parameter, do not use it.<br>Also, the `Percent` parameter may not work correctly with this parameter, since the host or other clients may also perform the heal.<br>Host - works only on host.<br>Client - works only on client.<br>All - works on host and client.                                                                        | Host    |
| HealMode            | Configures the heal mode.<br>Self - heal self.<br>Others - heal others.<br>All - heal all.                                                                                                                                                                                                                                                                                                                                                                                                                                                            | All     |
| PhaseMode           | Configures in which phases of the game health restoration will be triggered.<br>Level - when spawning in the level.<br>Shop - when spawning in the shop. Attention! Healing in the shop does not affect the game.<br>Lobby - when spawning in the truck (not menu) before starting the next level.<br>Arena - when spawning in the arena.<br>All - in all phases.                                                                                                                                                                                     | All     |
| ExactValue          | Sets the exact value to heal.<br>Attention! You must set the `PhaseMode` parameter to only one of `Level` or `Lobby`, not both. Otherwise, the healing will be done twice.<br>If the value is `0`, then it is disabled.<br>It has priority over the `Percent` parameter.<br>For example, if you set the value to `15`, you will always receive 15 HP.                                                                                                                                                                                                 | 0       |
| HealthPackMode      | Sets healing value options to the same as in health packs, one of which will be applied randomly.<br>Attention! You must set the `PhaseMode` parameter to only one of `Level` or `Lobby`, not both. Otherwise, the healing will be done twice.<br>If the value is `None`, then it is disabled.<br>It has priority over the `Percent` and `ExactValue` parameters.<br>For example, if you set the value to `Medium, Large`, you will receive a random healing of 50HP or 100HP.<br>Small - heal 25 HP.<br>Medium - heal 50 HP.<br>Large - heal 100 HP. | None    |
| HealRequirementMode | Sets requirements for performing heal.<br>None - no requirements.<br>Survive - the player must return to the truck at the end of the round and survive.                                                                                                                                                                                                                                                                                                                                                                                               | None    |

## Additionally

- If you find some bug, or you have a suggestion for improvement, please create
  an [issue](https://github.com/yvsimakov/R.E.P.O_FullHealth/issues).