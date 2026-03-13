using DSMOOFramework.Config;

namespace PlayerSwap;

[Config(Name = "player_swap")]
public class Config : IConfig
{
    public bool Enabled { get; set; } = true;
    public bool SwapRandom { get; set; } = true;
    public bool AllowSwapToYourself { get; set; } = false;
    public bool SwapOnBonk  { get; set; } = true;
    public bool SwapOnDamage { get; set; } = true;
    public bool SwapOnDeath  { get; set; } = true;
    public bool SwapOnMoonCollect { get; set; } = true;
    public bool SwapOnTime { get; set; } = true;
    public int MinTime { get; set; } = 60;
    public int MaxTime { get; set; } = 90;
}