using DSMOOFramework.Logger;
using DSMOOFramework.Plugins;
using DSMOOServer.API.Enum;
using DSMOOServer.API.Events;
using DSMOOServer.API.Events.Args;
using DSMOOServer.API.Player;
using DSMOOServer.API.Stage;
using DSMOOServer.Logic;

namespace RandomTeleports;

[Plugin(
    Name = "RandomTeleports",
    Author = "Dimenzio",
    Description = "Teleports players randomly",
    Version = "1.0.0",
    Repository = "https://github.com/GrafDimenzio/OdysseyChallenges"
)]
public class RandomTeleports(
    EventManager eventManager,
    PlayerManager playerManager,
    StageManager stageManager,
    ILogger logger) : Plugin<Config>
{
    private readonly Random _random = new();

    public override void Initialize()
    {
        Task.Run(Run);
        eventManager.OnPlayerAction.Subscribe(OnPlayerAction);
        eventManager.OnPlayerCollectMoon.Subscribe(OnMoonCollect);
        logger.Info("RandomTeleports initialized");
    }

    private void OnMoonCollect(PlayerCollectMoonEventArgs args)
    {
        if (!Config.Enabled)
            return;

        if (Config.TeleportOnMoonCollect)
            TeleportPlayer(args.Player);
    }

    private void OnPlayerAction(PlayerActionEventArgs args)
    {
        if (!Config.Enabled)
            return;

        if ((args.Action == PlayerAction.Bonk && Config.TeleportOnBonk)
            || (args.Action == PlayerAction.Damage && Config.TeleportOnDamage))
            TeleportPlayer(args.Player);
    }

    private async Task Run()
    {
        while (true)
        {
            if (!Config.Enabled || !Config.TeleportOnTime)
            {
                await Task.Delay(5000);
                continue;
            }

            foreach (var player in playerManager.RealPlayers)
            {
                //This happens for player in the starting screen
                if (string.IsNullOrWhiteSpace(player.Stage))
                    continue;
                if (Config.IgnoredStages.Contains(player.Stage))
                    continue;
                TeleportPlayer(player);
            }

            await Task.Delay(_random.Next(Config.MinTime, Config.MaxTime) * 1000);
        }
    }

    private void TeleportPlayer(IPlayer player)
    {
        try
        {
            var possibleStages = new List<string> { player.Stage };
            if (Config.TeleportToSubarea)
            {
                var kingdom = stageManager.GetKingdomFromStage(player.Stage) ?? player.Stage;
                var stageInfo = stageManager.GetStageInfo(kingdom);
                foreach (var warp in stageInfo.Warps)
                {
                    if (warp.Name == "Come" || warp.Name == "Go")
                        continue;
                    possibleStages.Add(warp.ConnectedStage);
                }

                possibleStages = possibleStages.Distinct().ToList();
                for (var i = 0; i < Config.IncreasedKingdomChance; i++)
                    possibleStages.Add(player.Stage);
            }

            var stage = possibleStages[_random.Next(possibleStages.Count)];
            var warps = stageManager.GetStageInfo(stage)?.Warps ?? [];
            var warpId = warps.Length == 0 ? "" : warps[_random.Next(warps.Length)].Name;
            player.ChangeStage(stage, warpId);
        }
        catch (Exception e)
        {
            logger.Error("Error while teleporting players to random location", e);
        }
    }
}