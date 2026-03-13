using DSMOOFramework.Config;

namespace PlayerSwap;

[Config(Name = "player_swap")]
public class Config : IConfig
{
    public bool Enabled { get; set; } = true;
    public bool SwapRandom { get; set; } = true;
    public bool AllowSwapToYourself { get; set; } = false;
}