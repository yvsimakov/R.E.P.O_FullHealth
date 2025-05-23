# Change Log

## [1.8.0] - 2025-04-23

### Added

- New variables for expressions.

### Updated

- **Breaking change:** The library for parsing expressions has been replaced
  from [MathParser.org-mXparser](https://mathparser.org/) to [Expressive](https://github.com/bijington/expressive/).

## [1.7.1] - 2025-04-23

### Fixed

- Configuration description and README.md.

## [1.7.0] - 2025-04-22

### Added

- `ByMaxHealthPercentage` configuration parameter. Healing is performed based on a percentage of maximum health.
- `Expression` configuration parameter. Allows you to calculate the heal value using your expression. Uses
  the [MathParser.org-mXparser](https://mathparser.org/) library for calculations.

### Fixed

- `Percent` configuration parameter renamed to `ToMaxHealthPercentage` for clarity. It is responsible for healing up to
  a certain percentage of maximum health.

## [1.6.0] - 2025-04-20

### Added

- The ability to enable the requirement to return to the truck and survive at the end of the round to receive
  healing in the next round.
  The `HealRequirementMode` configuration parameter must be set to `Survive` for this.
- `Arena` variant added to the `PhaseMode` configuration parameter.
- `Others` variant added to the `HealMode` configuration parameter.
- `Client` variant added to the `WorkMode` configuration parameter.

### Fixed

- `HostAndClient` variant replaced by `All` variant in the `WorkMode` configuration parameter.

## [1.5.2] - 2025-04-11

### Fixed

- Fixed support for Vortex Mod Manager.

## [1.5.1] - 2025-04-04

### Fixed

- Readme

## [1.5.0] - 2025-04-04

### Added

- Parameter `ExactValue` - sets the exact value to heal.
- Parameter `HealthPackMode` - sets healing value options to the same as in health packs, one of which will be applied
  randomly.

## [1.4.0] - 2025-04-02

### Added

- Parameter `PhaseMode` - configures the phases in which the mod works (Level, Shop, Lobby, All).

### Fixed

- Description of configuration parameters and readme

## [1.3.0] - 2025-03-31

### Added

- Parameter `WorkMode` - configure work mode (Host - works only on host, HostAndClient - works on host and client).
- Parameter `HealMode` - configure heal mode (All - heal all, Self - self-heal).

## [1.2.0] - 2025-03-31

### Fixed

- Functionality in multiplayer, now if you are the host, other players will also be healed.

## [1.1.0] - 2025-03-30

### Added

- `Enabled` configuration parameter which indicates whether the mod is enabled.
- `Percent` configuration parameter which set percentage of maximum health to which the player will be healed.

### Fixed

- Functionality of mod for single player and multiplayer.

## [1.0.0] - 2025-03-30

### Added

- The first version of the mod 1.0.0 has been added.