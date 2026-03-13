using System.Numerics;
using DSMOOFramework.Plugins;
using DSMOOServer.API.Enum;
using DSMOOServer.API.Events;
using DSMOOServer.API.Events.Args;
using DSMOOServer.API.Player;
using DSMOOServer.API.Stage;
using DSMOOServer.Logic;

namespace PlayerSwap;

[Plugin(
    Name = "Player Swap",
    Author = "Dimenzio",
    Description = "Swaps players position",
    Version = "1.0.0",
    Repository = "https://github.com/GrafDimenzio/OdysseyChallenges"
    )]
public class PlayerSwap(EventManager eventManager, StageManager stageManager, PlayerManager playerManager) : Plugin<Config>
{
    private readonly Random _random = new();
    
    public override void Initialize()
    {
        Task.Run(Run);
        eventManager.OnPlayerAction.Subscribe(OnPlayerAction);
        eventManager.OnPlayerCollectMoon.Subscribe(OnMoonCollect);
        Logger.Info("PlayerSwap initialized");
    }
    
    private void OnMoonCollect(PlayerCollectMoonEventArgs args)
    {
        if (!Config.Enabled)
            return;
        
        if(Config.SwapOnMoonCollect)
            SwapAllPlayers();
    }

    private void OnPlayerAction(PlayerActionEventArgs args)
    {
        if (!Config.Enabled)
            return;

        if (args.Action == PlayerAction.Bonk && Config.SwapOnBonk
            || args.Action == PlayerAction.Damage && Config.SwapOnDamage)
            SwapAllPlayers();
    }
    
    public void SwapAllPlayers()
    {
        var positions = new Dictionary<IPlayer, Vector3>();
        var stages = new Dictionary<IPlayer, string>();
        var playersToSwap = new List<IPlayer>();
        foreach (var player in playerManager.RealPlayers)
        {
            if (string.IsNullOrWhiteSpace(player.Stage))
                continue;
            
            positions[player] = player.Position;
            stages[player] = player.Stage;
            playersToSwap.Add(player);
        }

        if (playersToSwap.Count <= 1)
            return;

        var shuffledPlayers = ShufflePlayers(playersToSwap);
        
        for (var i = 0; i < shuffledPlayers.Count; i++)
        {
            var player = playersToSwap[i];
            var swapTo = shuffledPlayers[i];
            player.ChangeStage(stages[swapTo], stageManager.GetNearestWarp(stages[swapTo], positions[swapTo]) ?? "");
        }
    }

    private List<IPlayer> ShufflePlayers(List<IPlayer> players)
    {
        if (Config.SwapRandom)
        {
            List<IPlayer> shuffledList;
            bool isShuffled;
            do
            {
                shuffledList = players.OrderBy(_ => _random.Next()).ToList();
                isShuffled = true;
                if (Config.AllowSwapToYourself)
                    break;
                
                for (var i = 0; i < shuffledList.Count; i++)
                {
                    if(shuffledList[i] == players[i])
                        isShuffled = false;
                }
            }
            while(!isShuffled && !Config.AllowSwapToYourself);
            return shuffledList;
        }

        var result = players.ToList();
        var first = result[0];
        result.RemoveAt(0);
        result.Add(first);
        return result;
    }
    
    private async Task Run()
    {
        while (true)
        {
            if (!Config.Enabled || !Config.SwapOnTime)
            {
                await Task.Delay(5000);
                continue;
            }

            SwapAllPlayers();
            
            await Task.Delay(_random.Next(Config.MinTime, Config.MaxTime) * 1000);
        }
    }
}