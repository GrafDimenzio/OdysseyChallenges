using DSMOOFramework.Logger;
using DSMOOFramework.Plugins;
using DSMOOServer.API.Enum;
using DSMOOServer.API.Events;
using DSMOOServer.API.Events.Args;
using DSMOOServer.API.Player;
using DSMOOServer.API.Stage;
using DSMOOServer.Logic;

namespace NoBonk;

[Plugin(
    Name = "NoBonk",
    Author = "Dimenzio",
    Version = "1.0.0",
    Description = "Resets the Stage upon bonking",
    Repository = "https://github.com/GrafDimenzio/OdysseyChallenges"
    )]
public class NoBonk(EventManager eventManager, StageManager stageManager, PlayerManager playerManager, ILogger logger) : Plugin<Config>
{
    public override void Initialize()
    {
        logger.Info("NoBonk initialized");
        eventManager.OnPlayerAction.Subscribe(OnPlayerAction);
    }

    private void OnPlayerAction(PlayerActionEventArgs args)
    {
        if (!Config.Enabled)
            return;
        
        if (args.Action != PlayerAction.Bonk)
            return;

        var players = new List<IPlayer>() { args.Player };
        if (!Config.ResetOnlyBonkingPlayer)
        {
            foreach (var player in playerManager.RealPlayers)
            {
                if (player == args.Player)
                    continue;
                if (Config.ResetOnlyPlayersOnSameStage && player.Stage != args.Player.Stage)
                    continue;
                
                players.Add(player);
            }
        }

        foreach (var player in players)
        {
            var stage = Config.ResetAllToBonkingPlayer ? args.Player.Stage : player.Stage;
            if (Config.ResetToKingdom)
                stage = stageManager.GetKingdomFromStage(stage) ?? stage;
            player.ChangeStage(stage);
        }
    }
}