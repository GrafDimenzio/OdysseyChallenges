using DSMOOFramework.Plugins;
using DSMOOServer.API.Events;
using DSMOOServer.API.Events.Args;
using DSMOOServer.API.Stage;
using DSMOOServer.Logic;

namespace SameStage;

[Plugin(
    Name = "SameStage",
    Author = "Dimenzio",
    Description = "Teleports every player on the same Stage",
    Version =  "1.0.0",
    Repository = "https://github.com/GrafDimenzio/OdysseyChallenges"
    )]
public class SameStage(EventManager eventManager, PlayerManager playerManager, StageManager stageManager) : Plugin<Config>
{
    public override void Initialize()
    {
        Logger.Info("Same Stage initialized");
        eventManager.OnPlayerChangeStage.Subscribe(OnChangeStage);
    }

    private void OnChangeStage(PlayerChangeStageEventArgs args)
    {
        if (args.SendBack || !Config.Enabled)
            return;
        
        foreach (var player in playerManager.RealPlayers)
        {
            if (player == args.Player)
                continue;
            
            if (player.Stage != args.NewStage)
                player.ChangeStage(args.NewStage, stageManager.GetConnection(player.Stage, args.NewStage));
        }
    }
}