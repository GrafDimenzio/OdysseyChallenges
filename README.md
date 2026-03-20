# OdysseyChallenges

**OdysseyChallenges** is a collection of multiple simple plugins for [DSMOO](https://github.com/GrafDimenzio/DSMOO). You can download the latest releases [here](https://github.com/GrafDimenzio/OdysseyItems/releases/latest).

---

## Installation

1. Download one or more plugin `.dll` files:  
   - `NoBonk.dll`  
   - `PlayerSwap.dll`  
   - `RandomTeleports.dll`  
   - `SameStage.dll`  
2. Place the `.dll` files in your DSMOO plugin directory.  
3. After installation, default configuration files will be automatically generated for each plugin upon starting DSMOO  

---

## NoBonk

The **NoBonk** plugin resets all players whenever a player bonks.

### Configuration

| Config Option | Description |
|---------------|-------------|
| `Enabled` | If set to `false`, the plugin will do nothing. |
| `ResetOnlyBonkingPlayer` | Only the player who bonked will be reset. |
| `ResetAllToBonkingPlayer` | All players will be teleported to the same stage as the player who bonked. |
| `ResetOnlyPlayersOnSameStage` | Only players on the same stage as the bonk will be reset. (Requires `ResetOnlyBonkingPlayer` to be disabled.) |
| `ResetToKingdom` | Teleports players to the kingdom stage, leaving any subareas. |

---

## PlayerSwap

The **PlayerSwap** plugin swaps all connected players with each other.

### Configuration

| Config Option | Description |
|---------------|-------------|
| `Enabled` | If set to `false`, the plugin will do nothing. |
| `SwapRandom` | Players swap randomly. If disabled, players always swap with the same partners. |
| `AllowSwapToYourself` | Players may swap to their own position. |
| `SwapOnBonk` | Players swap when someone bonks. |
| `SwapOnDamage` | Players swap when a player takes damage. |
| `SwapOnMoonCollect` | Players swap when a moon is collected. |
| `SwapOnTime` | Players swap at regular time intervals. |
| `MinTime` | Minimum interval (in seconds) for `SwapOnTime`. |
| `MaxTime` | Maximum interval (in seconds) for `SwapOnTime`. |

---

## RandomTeleports

The **RandomTeleports** plugin teleports players to a random warp location.

### Configuration

| Config Option | Description |
|---------------|-------------|
| `Enabled` | If set to `false`, the plugin will do nothing. |
| `TeleportOnTime` | Players teleport on a regular interval. |
| `TeleportOnBonk` | Player teleports when they bonk. |
| `TeleportOnDamager` | Player teleports when taking damage. |
| `TeleportOnMoonCollect` | Player teleports when collecting a moon. |
| `MinTime` | Minimum time interval (in seconds) for timed teleportation. |
| `MaxTime` | Maximum time interval (in seconds) for timed teleportation. |
| `TeleportToSubareas` | Allows teleportation to subareas. |
| `IncreasedKingdomChance` | If `TeleportToSubareas` is enabled, this increases the likelihood of teleporting to the kingdom instead of a subarea. Higher values increase the chance. |
| `IgnoresStages` | Players cannot be teleported if they are currently in these stages. |

---

## SameStage

The **SameStage** plugin forces all players to be on the same stage. When one player enters a new stage, all other players are moved there automatically.

### Configuration

| Config Option | Description |
|---------------|-------------|
| `Enabled` | If set to `false`, the plugin will do nothing. |
