using DSMOOFramework.Config;

namespace NoBonk;

[Config(Name = "no_bonk")]
public class Config : IConfig
{
    public bool Enabled { get; set; } = true;
    public bool ResetOnlyBonkingPlayer { get; set; } = false;
    public bool ResetAllToBonkingPlayer { get; set; } = true;
    public bool ResetOnlyPlayersOnSameStage { get; set; } = false;
    public bool ResetToKingdom { get; set; } = false;
}