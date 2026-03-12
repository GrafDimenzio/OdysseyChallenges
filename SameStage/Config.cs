using DSMOOFramework.Config;

namespace SameStage;

[Config(Name = "same_stage")]
public class Config : IConfig
{
    public bool Enabled { get; set; } = true;
}