using DSMOOFramework.Config;

namespace RandomTeleports;

[Config(Name = "random_teleports")]
public class Config : IConfig
{
    public bool Enabled { get; set; } = true;
    public bool TeleportOnTime { get; set; } = true;
    public bool TeleportOnBonk { get; set; } = true;
    public bool TeleportOnDamage { get; set; } = true;
    public bool TeleportOnMoonCollect { get; set; } = true;
    public int MinTime { get; set; } = 30;
    public int MaxTime { get; set; } = 90;
    public bool TeleportToSubarea { get; set; } = true;
    public int IncreasedKingdomChance { get; set; } = 10;

    public string[] IgnoredStages { get; set; } =
        ["MoonWorldBasementStage", "MoonWorldKoopa1Stage", "MoonWorldKoopa2Stage", "DemoEndingStage"];
}